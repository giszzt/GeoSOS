using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Accord.Neuro;
using Accord.Neuro.Learning;
using System.Windows.Forms;
using FrameWork.Gui;
using GIS.FrameWork;
using Accord.Math;
using GIS.AddIns.Ca.CommonClass;

namespace GIS.AddIns.Ca.CaClass
{
    class AnnCa
    {

        #region delegate

        public delegate void UpdateConsoleDelegate(string line); // 向console中打印消息的委托
        public UpdateConsoleDelegate updateConsoleEvent;
        public delegate void UpdateImageDelegate(double[] buffer, int width, int height, LandUseClassificationInfo landUseInfo); // 绘制模拟图像委托
        public UpdateImageDelegate updateImageEvent;

        #endregion

        #region inner class

        private class Cell
        {
            public int row { get; set; }
            public int col { get; set; }
        }

        #endregion

        #region fields

        double alpha = 1;
        double threshold = 0.7;
        int numOfSample = 3000;
        int timesOfTrain = 1000;

        ActivationNetwork network = null;
        LandUseClassificationInfo landInfo = null;

        string beginLayernName = "";
        string endLayerName = "";
        List<string> driveLayerNames = null;

        double[][] driveBufferList = null;
        double[] beginBuffer = null;
        double[] endBuffer = null;

        int width = 0;
        int height = 0;

        #endregion


        #region properties

        public int TimesOfTrain
        {
            get
            {
                return this.timesOfTrain;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("训练次数要求是正整数");
                }
                else
                {
                    this.timesOfTrain = value;
                }
                
            }
        }

        /// <summary>
        /// 采样数目
        /// </summary>
        public int NumOfSamples
        {

            get
            {
                return this.numOfSample;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("样本数要是正整数");
                }
                else
                {
                    this.numOfSample = value;
                }

            }
        }

        /// <summary>
        /// 转换的阈值
        /// </summary>
        public double Threshold
        {
            get
            {
                return this.threshold;
            }
            set
            {
                this.threshold = value;
            }
        }

        /// <summary>
        /// 随机因子中的系数
        /// </summary>
        public double Alpha
        {
            get
            {
                return this.alpha;
            }
            set
            {

                this.alpha = value;
            }
        }

        public double[] BeginBuffer
        {
            get
            {
                return this.beginBuffer;
            }
        }

        public int Width
        {
            get
            {
                return this.width;
            }
        }

        public int Height
        {
            get
            {
                return this.height;
            }
        }

        #endregion


        #region private methods
        /// <summary>
        /// 和GetData功能相同，但是使用GDAL直接从文件中读取数据。
        /// </summary>
        /// <param name="fileName">文件名字</param>
        /// <param name="width">ref用于返回数据的宽度</param>
        /// <param name="height">ref用于返回数据的高度</param>
        /// <returns>一维数据数组,按行优先</returns>
        private double[] GdalGetData(string fileName, ref int width, ref int height)
        {
            OSGeo.GDAL.Dataset dataset = OSGeo.GDAL.Gdal.Open(fileName, OSGeo.GDAL.Access.GA_ReadOnly);
            width = dataset.RasterXSize;
            height = dataset.RasterYSize;
            double[] imageBuffer = new double[width * height];
            OSGeo.GDAL.Band b = dataset.GetRasterBand(1);
            b.ReadRaster(0, 0, width, height, imageBuffer, width, height, 0, 0);
            return imageBuffer;
        }

        /// <summary>
        /// 载入图层名对应的数据
        /// </summary>
        /// <param name="inputColumnNames"></param>
        /// <param name="outputColumnName"></param>
        private void LoadData()
        {
            // ----------------todo 处理null的问题 

            // --- 开发时gdal读数据 --

            //string prefix = @"E:\test\10_21_ann\ca_ann_test\img\";
            //string extname = ".tif";
            string prefix = @"";
            string extname = "";            
            this.driveBufferList = (from fileName in this.driveLayerNames
                                    select GdalGetData(prefix + fileName + extname, ref width, ref height)).ToArray<double[]>();
            this.beginBuffer = GdalGetData(prefix + this.beginLayernName + extname, ref width, ref height);
            this.endBuffer = GdalGetData(prefix + this.endLayerName + extname, ref width, ref height);
            

            // ---

            // --- 生产时

            // this.driveBufferList = (from fileName in inputColumnNames
            //                         select GetData(fileName, ref width, ref height)).ToArray<double[]>();
            // this.outputBuffer = GetData(outputColumnName, ref width, ref height);

            // ---



        }

        /// <summary>
        /// 采样,按照土地类型等比例采样，土地类型改变何不变各采样一半
        /// </summary>
        /// <param name="count">样本数目</param>
        /// <returns></returns>
        private List<Cell> getSamples(int count)
        {
            List<Cell> samples = new List<Cell>();
            long totalNumOfCells = 0;


            // dimension 1 for different landuse dimension 2 for change or no-change
            List<Cell>[][] landUseChnageType = new List<Cell>[this.landInfo.NumOfLandUseTypes][];
            for (int i = 0; i < this.landInfo.NumOfLandUseTypes; i++)
            {
                landUseChnageType[i] = new List<Cell>[2];
                landUseChnageType[i][0] = new List<Cell>(); // same
                landUseChnageType[i][1] = new List<Cell>();  // change
            }

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int pos = row * width + col;
                    if (this.beginBuffer[pos]  > 0  && this.endBuffer[pos] > 0) // 非空值
                    {
                        for (int i = 0; i < this.landInfo.NumOfLandUseTypes; i++)
                        {
                            if (this.landInfo.AllTypes[i].LandUseTypeValue == beginBuffer[pos])
                            {
                                if (beginBuffer[pos] == endBuffer[pos])
                                {
                                    landUseChnageType[i][0].Add(new Cell() { row = row, col = col }); // 不变
                                }
                                else
                                {
                                    landUseChnageType[i][1].Add(new Cell() { row = row, col = col }); // 变

                                }
                                totalNumOfCells++;
                            }
                        }
                    }

                }
            }


            Random rnd = new Random();
            for (int i = 0; i < this.landInfo.NumOfLandUseTypes; i++)
            {
                int num = (int)((float)(landUseChnageType[i][0].Count + landUseChnageType[i][1].Count) / (float)totalNumOfCells * count);
                for (int j = 0; j < num; j++)
                {
                    if (j % 2 == 0)
                    {
                        if (landUseChnageType[i][0].Count != 0)
                        {
                            int idx = rnd.Next(landUseChnageType[i][0].Count);
                            Cell cell = landUseChnageType[i][0][idx];
                            samples.Add(cell);
                            landUseChnageType[i][0].RemoveAt(idx);

                        }
                    }
                    else
                    {
                        if (landUseChnageType[i][1].Count != 0)
                        {
                            int idx = rnd.Next(landUseChnageType[i][1].Count);
                            Cell cell = landUseChnageType[i][1][idx];
                            samples.Add(cell);
                            landUseChnageType[i][1].RemoveAt(idx);
                        }
                    }
                }

            }
            while (samples.Count != count)
            {
                int row = rnd.Next(height);
                int col = rnd.Next(width);
                int pos = row * width + col;
                if (beginBuffer[pos] < 0 && endBuffer[pos] < 0) // 跳过空值
                {
                    continue;
                }
                samples.Add(new Cell() { row = row, col = col });

            }



            return samples;
        }



        private int getOneClass(Cell cell)
        {
            
            int pos = cell.row * width + cell.col;
            for (int i = 0; i < this.landInfo.NumOfLandUseTypes; i++)
            {
                if (this.landInfo.AllTypes[i].LandUseTypeValue == this.endBuffer[pos])
                {
                    return i;
                }                
            }
            throw new Exception("-1" + this.endBuffer[pos]);
            
        }


        private double GetError(double[][] inputs, double[][] outpus, int[] classes)
        {
            int numOfRight = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                double[] answer = this.network.Compute(inputs[i]);

                int expected = classes[i];
                int actual; 
                answer.Max(out actual);
                if (expected == actual)
                {
                    numOfRight++;
                }                
            }
            return (double)numOfRight / inputs.Length;
        }


        /// <summary>
        /// 获得type指定类型的邻域比例
        /// </summary>
        /// <param name="cellRow"></param>
        /// <param name="cellCol"></param>
        /// <param name="type"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        private double GetNeighbourAffect(int cellRow, int cellCol, double type, int step)
        {
            double all = step * step - 1;
            step = (int)(step / 2); // 
            int rowBegin = cellRow - step;
            int rowEnd = cellRow + step + 1;
            int colBegin = cellCol - step;
            int colEnd = cellCol + step + 1;
            int cnt = 0;
            for (int row = rowBegin; row < rowEnd; row++)
            {
                for (int col = colBegin; col < colEnd; col++)
                {
                    if (row < 0 || col < 0 || row >= height || col >= width || (row == cellRow && col == cellCol))
                    {
                        continue;
                    }
                    int pos = row * width + col;
                    
                    if (beginBuffer[pos] == type)
                    {
                        cnt++;
                    }
                }
            }
            return (double)cnt / all;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private double[] getOneInput(Cell cell)
        {
            List<double> input = new List<double>();
            
            var pos = cell.row * width + cell.col;
            
            for (int i = 0; i < this.landInfo.NumOfLandUseTypes; i++)
            {
                if (this.landInfo.AllTypes[i].LandUseTypeValue == this.beginBuffer[pos])
                {
                    input.Add(1);
                }
                else
                {
                    input.Add(0);
                }
            }
            for (int i = 0; i < this.landInfo.NumOfLandUseTypes; i++)
            {
                input.Add(GetNeighbourAffect(cell.row, cell.col, this.landInfo.AllTypes[i].LandUseTypeValue, 7));
            }
            double[] drive = (from buffer in driveBufferList 
                       select buffer[pos]).ToArray<double>();
            input.AddRange(drive);

            return input.ToArray<double>();
        }


        private double[] getOneOutput(Cell cell)
        {
            var output = new double[this.landInfo.NumOfLandUseTypes];
            int pos = cell.row * width + cell.col;
            for (int i = 0; i < this.landInfo.NumOfLandUseTypes; i++)
            {
                if (this.landInfo.AllTypes[i].LandUseTypeValue == this.endBuffer[pos])
                {
                    output[i] = 1.0;
                }
                else
                {
                    output[i] = 0.0;
                }
            }
            return output;
        }

        #endregion

        #region public methods
        /// <summary>
        /// 训练神经网络
        /// </summary>
        public void Train()
        {
            var samples = getSamples(this.numOfSample);

            double[][] inputs = (from cell in samples
                                 select getOneInput(cell)).ToArray<double[]>();


            int[] classes = (from cell in samples
                             select getOneClass(cell)).ToArray<int>();

            double[][] outputs = Accord.Statistics.Tools.Expand(classes, 0, +1);

            // Create an activation function for the net
            //var function = new BipolarSigmoidFunction();
            var function = new SigmoidFunction();

            // Create an activation network with the function and
            //  4 inputs, 5 hidden neurons and 3 possible outputs:
            int numOfInput = inputs[0].Length;
            int numOfHidden = numOfInput * 2 / 3;
            int numOfOut = outputs[0].Length;
            this.network = new ActivationNetwork(function, numOfInput, numOfHidden, numOfOut);

            // Randomly initialize the network
            new NguyenWidrow(this.network).Randomize();

            // Teach the network using parallel Rprop:
            var teacher = new ParallelResilientBackpropagationLearning(this.network);

            double correctRate = 0.0;
            int times = this.timesOfTrain;
            int cnt = 0;
            while (cnt < times)
            {
                teacher.RunEpoch(inputs, outputs);
                if (cnt % 10 == 0)
                {
                    correctRate = GetError(inputs, outputs, classes);
                    updateConsoleEvent("正确率：" + correctRate * 100 + "%");
                }
                cnt++;

            }
            updateConsoleEvent("训练结束。最终正确率：" + correctRate * 100 + "%");

        }

        /// <summary>
        /// 模拟
        /// </summary>
        public void Simulate(int times)
        {            
            Random rnd = new Random();
            int cnt = 0;
            int numOfChangesOneTime = 0;
            double[] middleBuffer = new double[width * height];
            while (cnt < times)
            {
                for (int row = 0; row < height; row++)
                {

                    for (int col = 0; col < width; col++)
                    {
                        int pos = row * width + col;
                        if (beginBuffer[pos] < 0) // null value 跳过
                        {
                            middleBuffer[pos] = beginBuffer[pos];
                            continue;
                        }
                        var input = getOneInput(new Cell() { row = row, col = col });
                        double[] answer = this.network.Compute(input);
                        int actual;
                        double max = answer.Max(out actual);
                        double ra = (double)(1 + Math.Pow(-Math.Log(rnd.NextDouble(), Math.E), this.alpha));
                        if (max * ra > this.threshold)
                        {
                            middleBuffer[pos] = landInfo.AllTypes[actual].LandUseTypeValue;
                            if (landInfo.AllTypes[actual].LandUseTypeValue != beginBuffer[pos])
                            {
                                numOfChangesOneTime++;
                            }

                        }
                        else
                        {

                            middleBuffer[pos] = beginBuffer[pos];
                        }


                    }
                }
                this.beginBuffer = middleBuffer;
                updateImageEvent(this.beginBuffer, width, height, this.landInfo);  // draw
                middleBuffer = new double[width * height];
                updateConsoleEvent("转化了:" + numOfChangesOneTime);
                numOfChangesOneTime = 0;
                cnt++;
            }
            this.updateConsoleEvent("模拟结束");


        }



        public void Run(object obj)
        {
            int times = (int)obj;
            this.Train();
            this.Simulate(times);
        }

        #endregion

        #region constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="beginLayernName">训练模拟起始图层名字</param>
        /// <param name="endLayerName">训练模拟终止图层名字</param>
        /// <param name="driveLayerNames">驱动因子图层名字</param>
        public AnnCa(string beginLayernName, string endLayerName, List<string> driveLayerNames, LandUseClassificationInfo landInfo)
        {

            this.beginLayernName = beginLayernName;
            this.endLayerName = endLayerName;
            this.driveLayerNames = driveLayerNames;
            this.landInfo = landInfo;
            
            //准备数据
            LoadData();


        }
        #endregion










    }
}

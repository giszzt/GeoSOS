using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotSpatial.Symbology;
using DotSpatial.Data;
using OSGeo.GDAL;
using Accord.Math;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using Accord.MachineLearning.DecisionTrees.Pruning;
using System.Windows.Forms;
using Accord.MachineLearning.DecisionTrees.Rules;
using GIS.AddIns.Ca.CommonClass;

namespace GIS.AddIns.Ca.CaClass
{
    /// <summary>
    /// 决策树类
    /// </summary>
    class CaDecisionTree
    {

        #region delegate
        public delegate void UpdateConsoleDelegate(string line); // 向console中打印消息的委托
        public UpdateConsoleDelegate updateConsoleInfoEvent;
        public delegate void UpdateImageDelegate(double[] buffer, int width, int height, LandUseClassificationInfo landUseInfo);
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
        double[][] driveBufferList = null;
        double[] outputBuffer = null;
        int width = 0;
        int height = 0;
        double sampleRate = 0.01;
        List<string> inputColumnNames = null;
        string outputColumnName = null;
        Func<double[], int> func = null;
        LandUseClassificationInfo landUseInfo = null;


        Random rnd = null;
        #endregion


        #region property

        /// <summary>
        /// 采样率
        /// </summary>
        public double SampleRate
        {
            get
            {
                return this.sampleRate;
            }
            set
            {
                this.sampleRate = value;
            }
        }


        public double[] OuputBuffer 
        {
            get
            {
                return this.outputBuffer;
            }
        }
        
        public int Width 
        {
            get
            {
                return width;
            }
          
        }


        public int Height 
        {
            get
            {
                return height;
            }
    
        }

        /// <summary>
        /// 土地利用类型与栅格数值的对应关系
        /// </summary>
        public LandUseClassificationInfo LandUseInfo
        {
            set
            {
                this.landUseInfo = value;
            }
        }


        #endregion

        #region private methods

        /// <summary>
        /// 根据栅格图层名字返回栅格图层数据数组
        /// </summary>
        /// <param name="layerName">图层名字</param>
        /// <param name="width">ref类型,数据的宽度</param>
        /// <param name="height">ref类型,数据的高度</param>
        /// <returns>数据数组，按行优先</returns>
        private double[] GetData(string layerName, ref int width, ref int height)
        {
            var map = GIS.FrameWork.Application.App.Map;
            var layers = map.Layers;
            ILayer selectedLayer = null;
            for (int i = 0; i < layers.Count; i++)
            {
                var layer = layers[i];
                if (layer.LegendText == layerName)
                {
                    selectedLayer = layer;
                    break;
                }
            }

            if (selectedLayer == null)
            {
                return null; // 图层名无效
            }

            try
            {
                RasterLayer rasterLayer = selectedLayer as RasterLayer;
                IRaster rasterDataSet = rasterLayer.DataSet;
                Dataset dataset = GIS.GDAL.RasterConverter.Ds2GdalRaster(rasterDataSet, null, new int[] { 1 });
                width = dataset.RasterXSize;
                height = dataset.RasterYSize;
                double[] imageBuffer = new double[width * height];
                Band band = dataset.GetRasterBand(1);
                band.ReadRaster(0, 0, width, height, imageBuffer, width, height, 0, 0);
                return imageBuffer;
            }
            catch
            {
                return null; // 图层不是栅格图层或将图层转化为GDAL dataset失败或从GDAL dataset中读取数据失败
            }
        }

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
            string prefix = "";
            string extname = "";
            this.driveBufferList = (from fileName in inputColumnNames
                                    select GdalGetData(prefix + fileName + extname, ref width, ref height)).ToArray<double[]>();
            this.outputBuffer = GdalGetData(prefix + outputColumnName + extname, ref width, ref height);

            // ---

            // --- 生产时

            // this.driveBufferList = (from fileName in inputColumnNames
            //                         select GetData(fileName, ref width, ref height)).ToArray<double[]>();
            // this.outputBuffer = GetData(outputColumnName, ref width, ref height);

            // ---
            
            
            
        }

        /// <summary>
        /// 获得邻域影响值
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="cellRow"></param>
        /// <param name="cellCol"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        private double GetNeighbourAffect(double[] buffer, int width, int height, int cellRow, int cellCol, int step)
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
                    double type = buffer[pos];
                    if (this.landUseInfo.IsExistInUrbanInfos(type))
                    {
                        cnt++;
                    }
                }
            }
            return (double)cnt / all;
        }


        /// <summary>
        /// 构建决策树
        /// </summary>
        public void BuildTree()
        {
            // 采样
            List<Cell> samplePoints = getSample();

            // 样本数目
            int COUNT = samplePoints.Count;

            // 构造输入和输出数据集
            double[][] inputs = new double[COUNT][];
            int[] outputs = new int[COUNT];
            for (int i = 0; i < COUNT; i++)
            {
                Cell cell = samplePoints[i];
                int pos = cell.row * width + cell.col;
                List<double> input = (from buffer in driveBufferList
                                      select buffer[pos]).ToList<double>();
                input.Add(GetNeighbourAffect(outputBuffer, width, height, cell.row, cell.col, 3));
                inputs[i] = input.ToArray<double>();
                outputs[i] = (int)outputBuffer[pos];
            }


            // 训练数据集
            var trainingInputs = inputs.Submatrix(0, COUNT / 2 - 1);
            var trainingOutput = outputs.Submatrix(0, COUNT / 2 - 1);

            // 检验数据集
            var pruningInputs = inputs.Submatrix(COUNT / 2, COUNT - 1);
            var pruningOutput = outputs.Submatrix(COUNT / 2, COUNT - 1);

            // 设置驱动因子的名字
            List<DecisionVariable> featuresList = (from column in this.inputColumnNames
                                                   select new DecisionVariable(column, DecisionVariableKind.Continuous)).ToList<DecisionVariable>();
            featuresList.Add(new DecisionVariable("affectofneighbour", DecisionVariableKind.Continuous));

            // 训练树
            var tree = new DecisionTree(inputs: featuresList, classes: 2);
            var teacher = new C45Learning(tree);
            teacher.Learn(trainingInputs, trainingOutput);

            // 剪枝
            ErrorBasedPruning prune = new ErrorBasedPruning(tree, pruningInputs, pruningOutput);
            prune.Threshold = 0.1;// Gain threshold ？
            double lastError;
            double error = Double.PositiveInfinity;
            do
            {
                lastError = error;
                error = prune.Run();

            } while (error < lastError);

            updateConsoleInfoEvent("错误率：" + error);

            this.func = tree.ToExpression().Compile();
            //UpdateUi("错误率" + error);
            
            DecisionSet rules = tree.ToRules();
            string ruleText = rules.ToString();
            //consolePad.addLineToInfo(ruleText);
            updateConsoleInfoEvent("规则:");
            updateConsoleInfoEvent(ruleText);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<Cell> getSample()
        {
            int count = (int)(this.width * this.height * sampleRate) * 2; // 采样数目为采样率的两倍，多出来的一倍用于剪枝
            // 首先聚集城市和非城市点。
            // 可以实现分类采样，并且提高采样效率
            List<Cell> cityPoints = new List<Cell>();
            List<Cell> noCityPoints = new List<Cell>();
            for (int row = 0; row < this.height; row++)
            {
                for (int col = 0; col < this.width; col++)
                {
                    int pos = row * width + col;
                    double type = (int)this.outputBuffer[pos];
                    // 从城市栅格中采样
                    if (this.landUseInfo.IsExistInUrbanInfos(type))
                    {
                        cityPoints.Add(new Cell { row = row, col = col });
                    }
                    // 从非城市但可以转化为城市的栅格中采样
                    if (this.landUseInfo.IsExistInConvertableInfos(type))
                    {
                        noCityPoints.Add(new Cell { row = row, col = col });
                    }
                }
            }
            

            List<Cell> samplePoints = new List<Cell>();
            
            for (int i = 0; i < count; i++)
            {
                if (i % 2 == 0)
                {
                    int idx = rnd.Next(cityPoints.Count);
                    samplePoints.Add(cityPoints[idx]);
                    cityPoints.RemoveAt(idx);
                }
                else
                {
                    int idx = rnd.Next(noCityPoints.Count);
                    samplePoints.Add(noCityPoints[idx]);
                    noCityPoints.RemoveAt(idx);
                }
            }

            return samplePoints;
        }

        #endregion


        #region constructor

        /// <summary>
        /// 这里的名字是图层的名字
        /// </summary>
        /// <param name="inputColumnNames">驱动因子列名字列表</param>
        /// <param name="outputColumnName">结果列名字</param>
        public CaDecisionTree(List<string> inputColumnNames, string outputColumnName)
        {
            this.inputColumnNames = inputColumnNames;
            this.outputColumnName = outputColumnName;
            this.rnd = new Random();
            //准备数据
            LoadData();

            
        }

        #endregion


        #region public methods

        public void Simulate(object obj)
        {
            int times = (int)obj;
            
            // 开始模拟            
            int cnt = 0;
            int addedCity = 0;
            double[] middleBuffer = new double[width * height];
            while (cnt < times)
            {
                for (int row = 0; row < height; row++)
                {
                    for (int col = 0; col < width; col++)
                    {
                        int pos = row * width + col;
                        double type = outputBuffer[pos];
                        // 只考虑可以转化为城市的栅格
                        if (this.landUseInfo.IsExistInConvertableInfos(type))
                        {
                            List<double> input = (from buffer in driveBufferList
                                                  select buffer[pos]).ToList<double>();
                            input.Add(GetNeighbourAffect(outputBuffer, width, height, row, col, 3));
                            double[] inputArr = input.ToArray<double>();
                            int newType = func(inputArr);
                            // 如果转化为城市
                            if (this.landUseInfo.IsExistInUrbanInfos((double)newType) && rnd.NextDouble() < 0.1)
                            {
                                middleBuffer[pos] = (double)newType;
                                addedCity++;
                            }
                            else
                            {
                                middleBuffer[pos] = type;
                            }
                        }
                        else
                        {
                            middleBuffer[pos] = outputBuffer[pos];
                        }
                    }
                }
                // 将结果复制会outputBuffer
                for (int pos = 0; pos < width * height; pos++)
                {
                    outputBuffer[pos] = middleBuffer[pos];
                }

                updateImageEvent(outputBuffer, width, height, this.landUseInfo); // 通知主线程绘图                
                updateConsoleInfoEvent("总共新增： " + addedCity); // 通知主线程输出控制台消息
                cnt++;
            }
            updateConsoleInfoEvent("模拟结束");

        }

        public void Run(object obj)
        {
            BuildTree();
            Simulate(obj);
        }

        #endregion



    }


}

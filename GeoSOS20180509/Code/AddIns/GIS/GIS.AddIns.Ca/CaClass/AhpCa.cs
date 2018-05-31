using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GIS.AddIns.Ca.CommonClass;

namespace GIS.AddIns.Ca.CaClass
{
    class AhpCa
    {

        #region delegate

        public delegate void UpdateConsoleDelegate(string line); // 向console中打印消息的委托
        public UpdateConsoleDelegate updateConsoleEvent;
        public delegate void UpdateImageDelegate(double[] buffer, int width, int height, LandUseClassificationInfo landUseInfo); // 绘制模拟图像委托
        public UpdateImageDelegate updateImageEvent;

        #endregion


        #region fields
        string beginLayernName = "";
        string endLayerName = "";
        List<string> driveLayerNames = null;
        LandUseClassificationInfo landUseInfo = null;
        double[][] driveBufferList = null;
        double[] beginBuffer = null;
        double[] endBuffer = null;
        int width = 0;
        int height = 0;
        float[] weights = null;

        Random rnd = null;
        #endregion

        #region properties

        /// <summary>
        /// 返回图像
        /// </summary>
        public double[] BeginBuffer
        {
            get
            {
                return this.beginBuffer;
            }            
        }
        /// <summary>
        /// 返回图像宽度
        /// </summary>
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

        public LandUseClassificationInfo LandUseInfo
        {
            get
            {

                return this.landUseInfo;
            }
            set
            {

                this.landUseInfo = value;
            }
        }

        public float[] Weights
        {
            get
            {
                return this.weights;
            }
            set
            {

                this.weights = value;
            }
        }

        public int NumOfSimulation
        {
            get;
            set;
        }

        public int SizeOfNeighbour
        {
            get;
            set;
        }

        public double GlobalFactor
        {
            get;
            set;
        }
        public double LocalFactor
        {
            get;
            set;
        }
        public double Alpha
        {
            get;
            set;
        }

        /// <summary>
        /// 要增加的城市元胞总数
        /// </summary>
        public int CountOfCity
        {
            get;
            set;
        }

        #endregion

        #region constructor

        public AhpCa(string beginLayernName, string endLayerName, List<string> driveLayerNames)
        {
            this.beginLayernName = beginLayernName;
            this.endLayerName = endLayerName;
            this.driveLayerNames = driveLayerNames;

            this.rnd = new Random();
            //准备数据
            LoadData();
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
        #endregion

        #region public method

        public void simulate()
        {
            int numOfCity = 0;
            double maxR = 0;
            double[] rMatrix = new double[width * height];
            int times = 0;
            while (numOfCity < this.CountOfCity)
            {
                times++;
                maxR = double.MinValue;
                for (int row = 0; row < height; row++)
                {
                    for (int col = 0; col < width; col++)
                    {
                        int pos = row * width + col;
                        double r = 0;
                        // 可转化为城市的元胞
                        if (this.landUseInfo.IsExistInConvertableInfos(this.beginBuffer[pos]))
                        {
                            
                            // 全局因素
                            double globalValue = GetGlobalValue(pos) * this.GlobalFactor;
                            if(globalValue == 0)
                            {
                                rMatrix[pos] = 0;
                            }
                            // 局部因素
                            double localValue = GetNeighbourAffect(row, col, this.SizeOfNeighbour) * this.LocalFactor;
                            r = globalValue + localValue;
                            rMatrix[pos] = r;

                            if (r > maxR)
                            {
                                maxR = r;
                            }
                        }                            
                        else // 其它元胞
                        {
                            rMatrix[pos] = 0;
                        }

                    }
                }

                double[] pMatrix = new double[width * height];
                double pAll = 0;
                for (int i = 0; i < width * height; i++)
                {
                    double p  = Math.Exp((rMatrix[i] / maxR - 1) * this.Alpha);
                    pMatrix[i] = p;
                    pAll += p;
                }
                double[] pSmallMatrix = new double[width * height]; 
                for (int i = 0; i < width * height; i++)
                {
                    pSmallMatrix[i] = pMatrix[i] / pAll;
                }

                int cnt = 0;
                for (int k = 0; k < this.width * this.height; k++ )
                {
                    
                                     

                    if (this.beginBuffer[k] < 0)
                    {
                        continue;
                    }
                    double rndNum = this.rnd.NextDouble();
                    if (pSmallMatrix[k] > rndNum)
                    {
                        this.beginBuffer[k] = this.landUseInfo.UrbanInfos[0].LandUseTypeValue;
                        cnt++;
                        numOfCity++;
                    }
                }
                updateConsoleEvent("这次变化了" + cnt + ", 已经变化了：" + numOfCity);

                if (times % 10 == 0)
                {
                    this.updateImageEvent(this.beginBuffer, this.width, this.height, this.landUseInfo);
                }

            }
            this.updateConsoleEvent("模拟结束");
        }

        /// <summary>
        /// 返回一个位置的全局因素
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private double GetGlobalValue(int pos)
        {
            double factor = 0;
            for (int i = 0; i < driveBufferList.Length; i++)
            {
                if (driveBufferList[i][pos] < 0)
                {
                    return 0; // 防止空值
                }
                factor += this.Weights[i] * (1 - driveBufferList[i][pos]); // 因为在这里，全局因素越高代表一个点变成城市的概率越高，而驱动因子数据是距离数据，根据知识，距离越小，概率越高。
            }
            return factor;
        }

        /// <summary>
        /// 获得邻域影响值
        /// </summary>        
        /// <param name="cellRow"></param>
        /// <param name="cellCol"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        private double GetNeighbourAffect(int cellRow, int cellCol, int step)
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
                    double type = this.beginBuffer[pos];
                    if (this.landUseInfo.IsExistInUrbanInfos(type))
                    {
                        cnt++;
                    }
                }
            }
            return (double)cnt / all;
        }

        #endregion


    }
}

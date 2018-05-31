using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Symbology;
using DotSpatial.Data;
using OSGeo.GDAL;

using System.Threading;
using FrameWork.Gui;
using System.Runtime.InteropServices;

namespace GIS.AddIns.Ca.CaDialog
{
    public partial class LogisticPatchCaDialog : Form
    {
        #region field

        private IMap map = null;
        
        //CancellationTokenSource cancellationTokenSource;
        //IProgressMonitor progressMonitor;
        #endregion

        #region properties

        public IMap Map
        {
            get
            {
                return this.map;
            }
            private set
            {
                this.map = value;
            }
        }

        #endregion


        public LogisticPatchCaDialog(IMap map)
        {
            this.Map = map;
            InitializeComponent();
            InitializeComboBox();

        }
        #region private method

        /// <summary>
        /// 初始化下拉框
        /// </summary>
        private void InitializeComboBox()
        {
            var layers = this.Map.Layers;
            string[] layerNameArr = new string[layers.Count];
            for (int i = 0; i < layers.Count; i++)
            {
                if (layers[i].LegendText != null)
                {
                    layerNameArr[i] = layers[i].LegendText;
                }
                else
                {
                    layerNameArr[i] = "null";
                }

            }

            this.comboBoxInitialData.Items.AddRange(layerNameArr);
            this.comboBoxPgData.Items.AddRange(layerNameArr);
            this.comboBoxSuitableData.Items.AddRange(layerNameArr);
        }

        /// <summary>
        /// 根据栅格图层名字返回栅格图层二维数组
        /// </summary>
        /// <param name="layerName"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private double[] GetData(string layerName, ref int width, ref int height)
        {


            var layers = this.map.Layers;
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
                Dataset dataset = GIS.GDAL.RasterConverter.Ds2GdalRaster(rasterDataSet, null, new int[1] { 1 });
                width = dataset.RasterXSize;
                height = dataset.RasterYSize;
                double[] imageBuffer = new double[width * height];
                Band band1 = dataset.GetRasterBand(1);
                band1.ReadRaster(0, 0, width, height, imageBuffer, width, height, 0, 0);
                return imageBuffer;
            }
            catch
            {
                return null; // 图层不是栅格图层或将图层转化为GDAL dataset失败或从GDAL dataset中读取数据失败
            }


        }



        #endregion

        private void buttonOk_Click(object sender, EventArgs e)
        {
            double a0 = 0.0;
            double a1 = 0.0;
            int moorNeighbourSize = 0;
            double tSpon = 0.0;
            double sigma = 0.0;
            int amountOfNewDevelopment = 0;


            // 读取配置参数
            try
            {
                a0 = Convert.ToDouble(this.textBoxA0.Text);
                a1 = Convert.ToDouble(this.textBoxA1.Text);
                moorNeighbourSize = Convert.ToInt32(this.textBoxMoorNeighbourSize.Text);
                tSpon = Convert.ToDouble(this.textBoxTSpon.Text);
                sigma = Convert.ToDouble(this.textBoxSigma.Text);
                amountOfNewDevelopment = Convert.ToInt32(this.textBoxAmountOfNewDevelopment.Text);

            }
            catch
            {
                MessageBox.Show("请输入正确的a0 ,a1 , moorNeighbourSize, tSpon, sigma, amountOfNewDevelopment参数");
                return;
            }

            // 读取图层
            if (this.comboBoxInitialData.SelectedItem == null || this.comboBoxPgData.SelectedItem == null || this.comboBoxSuitableData.SelectedItem == null)
            {
                MessageBox.Show("清先选择图层");
                return;
            }
            string initialUrbanImageLayerName = this.comboBoxInitialData.SelectedItem.ToString();
            string pgLayerName = this.comboBoxPgData.SelectedItem.ToString();
            string landSuitableLayerName = this.comboBoxSuitableData.SelectedItem.ToString();

            int width = 0;
            int height = 0;

            double[] imageBuffer = GetData(initialUrbanImageLayerName, ref width, ref height);            
            double[] suitableBuffer = GetData(landSuitableLayerName, ref width, ref height);
            double[] pg = GetData(pgLayerName, ref width, ref height);

            if (imageBuffer == null || pg == null || suitableBuffer == null)
            {
                MessageBox.Show("图层名无效,图层不是栅格图层,将图层转化为GDAL dataset失败,从GDAL dataset中读取数据失败");
                return;
            }

            //this.logisticPatchCa = new LogisticPatchCa(a0,
            //    a1,
            //    moorNeighbourSize,
            //    tSpon,
            //    sigma,
            //    height,
            //    width,
            //    imageBuffer,
            //    pg,
            //    suitableBuffer,
            //    amountOfNewDevelopment);

            //this.SetSimulate();
            //this.SetProgressBar();
            unsafe
            {
                fixed (double* pImageBuffer = imageBuffer, pSuitableBuffer = suitableBuffer, pPg = pg)
                {

                    // 调用c++接口得到模拟后的数据
                    SWIGTYPE_p_double result = logistic_patch_ca.logistic_patch_ca1(
                        a0,
                        a1,
                        moorNeighbourSize,
                        tSpon,
                        sigma,
                        height,
                        width,
                        new SWIGTYPE_p_double(new IntPtr(pImageBuffer), false),
                        new SWIGTYPE_p_double(new IntPtr(pSuitableBuffer), false),
                        new SWIGTYPE_p_double(new IntPtr(pPg), false),
                        amountOfNewDevelopment,
                        true);
                    // 从指针中得到模拟结果，并纵向翻转
                    IntPtr resultPointer = HandleRef.ToIntPtr(SWIGTYPE_p_double.getCPtr(result));
                    double* pResultImg = (double*)resultPointer.ToPointer();
                    double[] resultImg = new double[width * height];
                    for (int row = 0; row < height; row++)
                    {
                        for (int col = 0; col < width; col++)
                        {
                            resultImg[row * width + col] = pResultImg[(height - row - 1) * width + col];
                        }
                    }

                    // 新建 GDAL dataset
                    OSGeo.GDAL.Driver driver = OSGeo.GDAL.Gdal.GetDriverByName("MEM");
                    OSGeo.GDAL.Dataset dataset = driver.Create("", width, height, 1, OSGeo.GDAL.DataType.GDT_Float64, null);
                    dataset.WriteRaster(0, 0, width, height, resultImg, width, height, 1, new int[1] { 1 }, 0, 0, 0);

                    // 将GDAL dataset转化为IRaster数据集
                    DotSpatial.Data.IRaster raster = GIS.GDAL.RasterConverter.Gdal2DSRaster(dataset, 1);
                    raster.Name = "Result";
                    this.Map.Layers.Add(raster);

                }
            }






        }

        ///// <summary>
        ///// 开启一个模拟线程
        ///// </summary>
        //void SetSimulate()
        //{
        //    //把回调的方法给委托变量
        //    CallBackDelegate cbd = CallBack;
        //    Thread t = new Thread(new ParameterizedThreadStart(RunThreadSimulate));
        //    t.Name = "TaskSimulate";
        //    t.Priority = ThreadPriority.Highest;
        //    t.IsBackground = true;
        //    t.Start(cbd);
        //}
        //public delegate void CallBackDelegate(bool message);
        //private void CallBack(bool message)
        //{
        //    //主线程报告信息,可以根据这个信息做判断操作,执行不同逻辑.
        //    if (message)
        //    {
        //        var width = logisticPatchCa.Width;
        //        var height = logisticPatchCa.Height;
        //        double[] result = logisticPatchCa.InitialData;
        //        double[] resultImg = new double[width * height];
        //        for (int row = 0; row < height; row++)
        //        {
        //            for (int col = 0; col < width; col++)
        //            {
        //                resultImg[row * width + col] = result[(height - row - 1) * width + col];
        //            }
        //        }

        //        //// 新建 GDAL dataset
        //        OSGeo.GDAL.Driver driver = OSGeo.GDAL.Gdal.GetDriverByName("MEM");
        //        OSGeo.GDAL.Dataset dataset = driver.Create("", width, height, 1, OSGeo.GDAL.DataType.GDT_Float64, null);
        //        dataset.WriteRaster(0, 0, width, height, resultImg, width, height, 1, new int[1] { 1 }, 0, 0, 0);

        //        // 将GDAL dataset转化为IRaster数据集
        //        DotSpatial.Data.IRaster raster = GIS.GDAL.RasterConverter.Gdal2DSRaster(dataset, 1);
        //        raster.Name = "Result";
        //        // 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
        //        Action<string> actionDelegate = (x) => { this.Map.Layers.Add(raster); };
                
        //        // Action<string> actionDelegate = delegate(string txt) { this.label2.Text = txt; };
        //        this.Invoke(actionDelegate, "a");
                
        //    }
        //}

        //void RunThreadSimulate(object callBack)
        //{
           

        //    logisticPatchCa.simulate();


        //    CallBackDelegate cb = callBack as CallBackDelegate;
        //    cb(true);
            
                
            
            
        //}

        //void RunThreadProgress()
        //{
        //    while (!cancellationTokenSource.IsCancellationRequested)
        //    {
        //        progressMonitor.TaskName = "模拟进度";
        //        progressMonitor.Progress = (double)this.logisticPatchCa.AreaAll / this.logisticPatchCa.AmountOfNewDevelopment;
        //        if (this.logisticPatchCa.AreaAll >= this.logisticPatchCa.AmountOfNewDevelopment)
        //        {
        //            try
        //            {
        //                cancellationTokenSource.Cancel();
        //                cancellationTokenSource.Dispose();
        //                progressMonitor.Dispose();
        //            }
        //            catch (OperationCanceledException)
        //            {
        //                // ignore cancellation
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show(ex.Message, "error");
        //            }

        //        }
        //        Thread.Sleep(50);
        //    }

        //}

        //void SetProgressBar()
        //{
        //    WorkbenchSingleton.StatusBar.SetCaretPosition(0, 1, 2);//设置X，Y，Z 
        //    WorkbenchSingleton.StatusBar.SetMessage("元胞自动机");//设置状态条内容
        //    cancellationTokenSource = new CancellationTokenSource();
        //    progressMonitor = WorkbenchSingleton.StatusBar.CreateProgressMonitor(cancellationTokenSource.Token);
        //    progressMonitor.TaskName = "模拟进度";
        //    Thread t = new Thread(new ThreadStart(RunThreadProgress));
        //    t.Name = "TaskProgress";
        //    t.Priority = ThreadPriority.BelowNormal;
        //    t.IsBackground = true;
        //    t.Start();
        //}

    }
}

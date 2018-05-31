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
using OSGeo.GDAL;
using DotSpatial.Data;
using System.Runtime.InteropServices;

namespace GIS.AddIns.Ca.CaDialog
{
    public partial class SimpleCaDialog : Form
    {
        #region field

        private IMap map = null;

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

        #region constructor

        public SimpleCaDialog(IMap map)
        {
            this.Map = map;
            InitializeComponent();
            InitializeComboBox();
        }

        public SimpleCaDialog()
        {
            InitializeComponent();
        }

        #endregion

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
                    layerNameArr[i] = " ";
                }
                
            }
            this.comboBoxInitialUrbanImage.Items.AddRange(layerNameArr);
            this.comboBoxPg.Items.AddRange(layerNameArr);
            this.comboBoxLandSuitable.Items.AddRange(layerNameArr);
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

        #region event handle

        private void buttonOk_Click(object sender, EventArgs e)
        {
            int width = 0;
            int height = 0;
            int times = 0;
            double threshold = 0.0;
            if (this.comboBoxInitialUrbanImage.SelectedItem == null || this.comboBoxLandSuitable.SelectedItem == null || this.comboBoxPg.SelectedItem == null)
            {
                MessageBox.Show("清先选择图层");
                return;
            }
            string initialUrbanImageLayerName = this.comboBoxInitialUrbanImage.SelectedItem.ToString();
            string pgLayerName = this.comboBoxPg.SelectedItem.ToString();
            string landSuitableLayerName = this.comboBoxLandSuitable.SelectedItem.ToString();

            try
            {
                times = Convert.ToInt32(this.textBoxTimes.Text);
                threshold = Convert.ToDouble(this.textBoxThreshold.Text);
            }
            catch
            {
                MessageBox.Show("请输入正确的times 和 threshold参数");
                return;
            }
            

            double[] imageBuffer = GetData(initialUrbanImageLayerName, ref width, ref height);
            double[] pg = GetData(pgLayerName, ref width, ref height);
            double[] suitableBuffer = GetData(landSuitableLayerName, ref width, ref height);

            if (imageBuffer == null || pg == null || suitableBuffer == null)
            {
                MessageBox.Show("图层名无效,图层不是栅格图层,将图层转化为GDAL dataset失败,从GDAL dataset中读取数据失败");
                return;
            }



            unsafe
            {
                fixed (double* pImageBuffer = imageBuffer, pSuitableBuffer = suitableBuffer, pPg = pg)
                {

                    // 调用c++接口得到模拟后的数据
                    SWIGTYPE_p_double result = simple_ca.simple_ca_logistic(new SWIGTYPE_p_double(new IntPtr(pImageBuffer), false),
                        new SWIGTYPE_p_double(new IntPtr(pSuitableBuffer), false),
                        new SWIGTYPE_p_double(new IntPtr(pPg), false),
                        width,
                        height, threshold, times);
                    //simple_ca.draw_image(new SWIGTYPE_p_double(new IntPtr(pImageBuffer), false), width, height);
                    //// 从指针中得到模拟结果，并纵向翻转
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




        #endregion

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GIS.FrameWork;
using DotSpatial.Controls;
using DotSpatial.Data;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.WindowsForms;
using OxyPlot.Annotations;
using DotSpatial.Symbology;
using ICSharpCode.Core.WinForms;


namespace GIS.AddIns.Statistic
{
    public partial class MapStatistics : UserControl
    {

        public PlotModel _pm = new PlotModel();
        public LinearAxis _XAxis = new LinearAxis();
        //public LinearColorAxis _XAxis = new LinearColorAxis();
        public LinearAxis _YAxis = new LinearAxis();
        public CategoryAxis _CAxis = new CategoryAxis();

        public string value_X;
        public string value_Y;
        public DataPoint _startPoint1 = new DataPoint();
        public DataPoint _endPoint1 = new DataPoint();
        public Dictionary<string, int> _pointDic1 = new Dictionary<string, int>();
        //public IFeatureLayer featurelayer;

        private System.Windows.Forms.ToolStrip _toolStrip;

        public MapStatistics()
        {
            InitializeComponent();
            _XAxis.AxislineStyle = OxyPlot.LineStyle.Solid;
            _YAxis.AxislineStyle = OxyPlot.LineStyle.Solid;
            _CAxis.AxislineStyle = OxyPlot.LineStyle.Solid;

            //为界面右侧增加一个工具条控件
            _toolStrip = ToolbarService.CreateToolStrip(this, "/FrameWork/ToolStrips/StatisticPlot");
            _toolStrip.GripStyle = ToolStripGripStyle.Hidden;
            _toolStrip.Dock = DockStyle.Right;
            Controls.Add(_toolStrip);
            //plotView1.Model = _pm;

       


        }

        #region Initial
        ///// <summary>
        ///// Gets or sets the Left Title
        ///// </summary>
        public string LeftTitle
        {
            get
            {
                return this.groupBox1.Text;
            }

            set
            {
                this.groupBox1.Text = value;
            }
        }

        ///// <summary>
        ///// Gets or sets the Right Title
        ///// </summary>
        public string RightTitle
        {
            get
            {
                return this.groupBox2.Text;
            }

            set
            {
                this.groupBox2.Text = value;
            }
        }

        ///// <summary>
        ///// Gets or sets the Left Map
        ///// </summary>
        public Map LeftMap
        {
            get
            {
                return leftMap;
            }

            set
            {
                leftMap = value;
            }
        }

        ///// <summary>
        ///// 加载图层
        ///// </summary>
        private void MapStatistics_Load(object sender, EventArgs e)
        {
            if (GIS.FrameWork.Application.App.Map == null || (GIS.FrameWork.Application.App.Map as Map).Name != leftMap.Name)
            {
                GIS.FrameWork.Application.App.Map = leftMap;
            }
            AppManager app = GIS.FrameWork.Application.App;
            IMap map = app.Map;
            map.FunctionMode = FunctionMode.Select;
        }
        #endregion

        /// <summary>
        /// the method of data selection
        /// </summary>
        /// <param name="cmbX"></param>
        /// <param name="cmbY"></param>
        /// <param name="featurelayer"></param>
        public void DataSelection(ComboBox cmbX, ComboBox cmbY, IFeatureLayer featurelayer)
        {
            foreach (DataColumn col in featurelayer.DataSet.DataTable.Columns)
            {
                if (col.DataType == typeof(double) || col.DataType == typeof(Single) || col.DataType == typeof(float) || col.DataType == typeof(int))
                {
                    cmbX.Items.Add(col.Caption);
                    cmbY.Items.Add(col.Caption);
                }
            }
        }
        public void DataSelection(ComboBox cmbY, IFeatureLayer featurelayer)
        {
            foreach (DataColumn col in featurelayer.DataSet.DataTable.Columns)
            {
                if (col.DataType == typeof(double) || col.DataType == typeof(Single) || col.DataType == typeof(float) || col.DataType == typeof(int))
                {
                    cmbY.Items.Add(col.Caption);
                }
            }
        }

        #region LinearSeries implement
        public void DrawPlot(ComboBox cmbX, ComboBox cmbY, MapStatistics ms, LinearBarSeries lb, IFeatureLayer featurelayer)
        {
            foreach (Feature feature in featurelayer.DataSet.Features)
            {
                ms.plotView1.Refresh();
                double X = Convert.ToDouble(feature.DataRow[cmbX.Text]);
                double Y = Convert.ToDouble(feature.DataRow[cmbY.Text]);
                ms.value_X = cmbX.Text;
                ms.value_Y = cmbY.Text;
                DataPoint dataPoint = new DataPoint(X, Y);
                lb.Points.Add(dataPoint);
                if (!ms._pointDic1.ContainsKey(dataPoint.ToString()))
                {
                    ms._pointDic1.Add(dataPoint.ToString(), feature.Fid);
                }


            }
            ms.plotView1.Refresh();
        }

        public void featurelayer_SelectionChanged(LinearBarSeries selectlb, MapStatistics ms, IFeatureLayer featurelayer)
        {
            if (featurelayer != null)
            {
                List<IFeature> featureList = featurelayer.Selection.ToFeatureList();
                if (featureList.Count != 0)
                {

                    foreach (Feature feature in featurelayer.Selection.ToFeatureList())
                    {
                        if (ms.value_X != "" || ms.value_Y != "")
                        {
                            double X = Convert.ToDouble(feature.DataRow[ms.value_X]);
                            double Y = Convert.ToDouble(feature.DataRow[ms.value_Y]);
                            DataPoint datapoint = new DataPoint(X, Y);
                            selectlb.Points.Add(datapoint);
                        }
                    }

                }
                else
                {
                    selectlb.Points.Clear();
                }
            }
            ms.plotView1.Refresh();
        }

        public void PlotMouseDown(LinearBarSeries lb, MapStatistics ms, OxyMouseDownEventArgs e)
        {
            if (lb.Points.Count != 0)
            {
                DataPoint dd = new DataPoint(lb.InverseTransform(e.Position).X, lb.InverseTransform(e.Position).Y);
                ms._startPoint1 = dd;

            }
        }

        public void PlotMouseMove(LinearBarSeries lb, MapStatistics ms, OxyMouseEventArgs e)
        {
            if (lb.Points.Count != 0)
            {
                ms._endPoint1 = lb.InverseTransform(e.Position);
                if (ms._startPoint1.X != 0 || ms._startPoint1.Y != 0)
                {
                    ms._pm.Annotations.Clear();

                    var rectangle = new PolygonAnnotation();
                    rectangle.Layer = AnnotationLayer.BelowAxes;
                    rectangle.StrokeThickness = 0.5;
                    rectangle.Stroke = OxyColors.Red;
                    rectangle.Fill = OxyColors.Transparent;
                    rectangle.LineStyle = OxyPlot.LineStyle.Dot;

                    rectangle.Points.Add(new DataPoint(ms._startPoint1.X, ms._startPoint1.Y));
                    rectangle.Points.Add(new DataPoint(ms._endPoint1.X, ms._startPoint1.Y));
                    rectangle.Points.Add(new DataPoint(ms._endPoint1.X, ms._endPoint1.Y));
                    rectangle.Points.Add(new DataPoint(ms._startPoint1.X, ms._endPoint1.Y));

                    ms._pm.Annotations.Add(rectangle as Annotation);
                    ms._pm.InvalidatePlot(true);
                }
            }
        }

        public void PlotMouseUp(LinearBarSeries selectlb, LinearBarSeries lb, MapStatistics ms, IFeatureLayer featurelayer)
        {
            if (ms._startPoint1.X != ms._startPoint1.Y)
            {
                List<int> indexs = SelectByRectangle(selectlb, lb, ms, featurelayer);
                featurelayer.Select(indexs);
                DataPoint zerodata = new DataPoint(0, 0);
                ms._startPoint1 = zerodata;
                ms._pm.Annotations.Clear();
                ms.plotView1.Model.InvalidatePlot(true);
            }
         }

        public List<int> SelectByRectangle(LinearBarSeries selectlb, LinearBarSeries lb, MapStatistics ms, IFeatureLayer featurelayer)
         {
            selectlb.Points.Clear();
            featurelayer.ClearSelection();
            List<int> result = new List<int>();
            double minX, minY, maxX, maxY;
            if (ms._startPoint1.X < ms._endPoint1.X)
            {
                minX = ms._startPoint1.X;
                maxX = ms._endPoint1.X;
            }
            else
            {
                minX = ms._endPoint1.X;
                maxX = ms._startPoint1.X;
            }

            if (ms._startPoint1.Y < ms._endPoint1.Y)
            {
                minY = ms._startPoint1.Y;
                maxY = ms._endPoint1.Y;
            }
            else
            {
                minY = ms._endPoint1.Y;
                maxY = ms._startPoint1.Y;
            }

            foreach (DataPoint item in lb.Points)
            {
                if (item.X < minX || item.X > maxX || item.Y < minY || item.Y > maxY)
                {
                    continue;
                }
                else
                {
                    int pointIndex = ms._pointDic1[item.ToString()];
                    result.Add(pointIndex);
                    selectlb.Points.Add(item);
                }
            }

            ms.plotView1.Refresh();
            return result;
        }
        #endregion


        #region lineSeries implement
        /// <summary>
        /// 为散点图/折线图添加坐标点，刷新plotview视图，绘制图形
        /// </summary>
        /// <param name="cmbX"></param>
        /// <param name="cmbY"></param>
        /// <param name="ms"></param>
        /// <param name="lb"></param>
        /// <param name="featurelayer"></param>
        public void DrawPlot(ComboBox cmbX, ComboBox cmbY, MapStatistics ms, LineSeries lb, IFeatureLayer featurelayer)
        {
            foreach (Feature feature in featurelayer.DataSet.Features)
            {
                ms.plotView1.Refresh();
                double X = Convert.ToDouble(feature.DataRow[cmbX.Text]);
                double Y = Convert.ToDouble(feature.DataRow[cmbY.Text]);
                ms.value_X = cmbX.Text;
                ms.value_Y = cmbY.Text;
                DataPoint dataPoint = new DataPoint(X, Y);
                lb.Points.Add(dataPoint);
                if (!ms._pointDic1.ContainsKey(dataPoint.ToString()))
                {
                    ms._pointDic1.Add(dataPoint.ToString(), feature.Fid);
                }


            }
            ms.plotView1.Refresh();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectlb"></param>
        /// <param name="ms"></param>
        /// <param name="featurelayer"></param>
        public void featurelayer_SelectionChanged(LineSeries selectlb, MapStatistics ms, IFeatureLayer featurelayer)
        {
            if (featurelayer != null)
            {
                List<IFeature> featureList = featurelayer.Selection.ToFeatureList();
                if (featureList.Count != 0)
                {

                    foreach (Feature feature in featurelayer.Selection.ToFeatureList())
                    {
                        if (ms.value_X != "" || ms.value_Y != "")
                        {
                            double X = Convert.ToDouble(feature.DataRow[ms.value_X]);
                            double Y = Convert.ToDouble(feature.DataRow[ms.value_Y]);
                            DataPoint datapoint = new DataPoint(X, Y);
                            selectlb.Points.Add(datapoint);
                        }
                    }

                }
                else
                {
                    selectlb.Points.Clear();
                }
            }
            ms.plotView1.Refresh();
        }

        public void PlotMouseDown(LineSeries lb, MapStatistics ms, OxyMouseDownEventArgs e)
        {
            if (lb.Points.Count != 0)
            {
                //DataPoint dd = new DataPoint(lb.InverseTransform(e.Position).X, lb.InverseTransform(e.Position).Y);
                ms._startPoint1 = lb.InverseTransform(e.Position);

            }
        }

        public void PlotMouseMove(LineSeries lb, MapStatistics ms, OxyMouseEventArgs e)
        {
            if (lb.Points.Count != 0)
            {
                ms._endPoint1 = lb.InverseTransform(e.Position);
                if (ms._startPoint1.X != 0 || ms._startPoint1.Y != 0)
                {
    
                    ms._pm.Annotations.Clear();

                    var rectangle = new PolygonAnnotation();
                    rectangle.Layer = AnnotationLayer.BelowAxes;
                    rectangle.StrokeThickness = 0.5;
                    rectangle.Stroke = OxyColors.Red;
                    rectangle.Fill = OxyColors.Transparent;
                    rectangle.LineStyle = OxyPlot.LineStyle.Dot;

                    rectangle.Points.Add(new DataPoint(ms._startPoint1.X, ms._startPoint1.Y));
                    rectangle.Points.Add(new DataPoint(ms._endPoint1.X, ms._startPoint1.Y));
                    rectangle.Points.Add(new DataPoint(ms._endPoint1.X, ms._endPoint1.Y));
                    rectangle.Points.Add(new DataPoint(ms._startPoint1.X, ms._endPoint1.Y));

                    ms._pm.Annotations.Add(rectangle as Annotation);
                    ms._pm.InvalidatePlot(true);
                }
            }
        }

        public void PlotMouseUp(LineSeries selectlb, LineSeries lb, MapStatistics ms, IFeatureLayer featurelayer)
        {
            if (ms._startPoint1.X != ms._startPoint1.Y)
            {
                List<int> indexs = SelectByRectangle(selectlb, lb, ms, featurelayer);
                featurelayer.Select(indexs);
                DataPoint zerodata = new DataPoint(0, 0);
                ms._startPoint1 = zerodata;
                ms._pm.Annotations.Clear();
                ms.plotView1.Model.InvalidatePlot(true);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectlb"></param>
        /// <param name="lb"></param>
        /// <param name="ms"></param>
        /// <param name="featurelayer"></param>
        /// <returns></returns>
        public List<int> SelectByRectangle(LineSeries selectlb, LineSeries lb, MapStatistics ms, IFeatureLayer featurelayer)
        {
            selectlb.Points.Clear();
            featurelayer.ClearSelection();
            List<int> result = new List<int>();
            double minX, minY, maxX, maxY;
            if (ms._startPoint1.X < ms._endPoint1.X)
            {
                minX = ms._startPoint1.X;
                maxX = ms._endPoint1.X;
            }
            else
            {
                minX = ms._endPoint1.X;
                maxX = ms._startPoint1.X;
            }

            if (ms._startPoint1.Y < ms._endPoint1.Y)
            {
                minY = ms._startPoint1.Y;
                maxY = ms._endPoint1.Y;
            }
            else
            {
                minY = ms._endPoint1.Y;
                maxY = ms._startPoint1.Y;
            }

            foreach (DataPoint item in lb.Points)
            {
                if (item.X < minX || item.X > maxX || item.Y < minY || item.Y > maxY)
                {
                    continue;
                }
                else
                {
                    int pointIndex = ms._pointDic1[item.ToString()];
                    result.Add(pointIndex);
                    selectlb.Points.Add(item);
                }
            }

            ms.plotView1.Refresh();
            return result;
        }
        #endregion

    }



}

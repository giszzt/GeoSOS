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
using FrameWork.Gui;


namespace GIS.AddIns.Statistic
{   
    public partial class LineBarPlot : UserControl
    {
        LinearBarSeries _lb = new LinearBarSeries();
        LinearBarSeries _selectlb = new LinearBarSeries();

        IFeatureLayer _featurelayer = null;

        public MapStatistics _ms
        {
            get;
            set;
        }
        public LineBarPlot(MapStatistics ms, IFeatureLayer featurelayer)
        {
            InitializeComponent();
            _ms = ms;
            _featurelayer = featurelayer;
            _ms._pm.Axes.Clear();
            _ms._pm.Background = OxyColors.White;
            _ms._pm.SelectionColor = OxyColors.Red;
            _ms._pm.MouseDown += PlotMouseDown;
            _ms._pm.MouseMove += PlotMouseMove;
            _ms._pm.MouseUp += PlotMouseUp;
            _featurelayer.SelectionChanged += new EventHandler(_featurelayer_SelectionChanged);

            _lb.FillColor = OxyColors.HotPink;
            _lb.NegativeFillColor = OxyColors.LightSkyBlue;
            _lb.StrokeThickness = 1;
            _lb.StrokeColor = OxyColors.Ivory;
            _lb.Selectable = true;
            _lb.SelectionMode = OxyPlot.SelectionMode.Multiple;

            _ms._XAxis.Position = AxisPosition.Bottom;
            _ms._XAxis.Title = "X";
            _ms._YAxis.Title = "Y";
   

            _ms._pm.Axes.Add(_ms._XAxis);
            _ms._pm.Axes.Add(_ms._YAxis);
            //_lb.BarWidth = 20;

            //_selectSeries1.LineStyle = OxyPlot.LineStyle.None;
            //_selectSeries1.MarkerType = MarkerType.Square;
            //_selectSeries1.MarkerSize = 6;
            //_selectSeries1.MarkerFill = OxyColors.Red;
            _selectlb.FillColor = OxyColors.Red;
            _selectlb.NegativeFillColor = OxyColors.Red;
            _selectlb.StrokeThickness = 1;
            _selectlb.Selectable = true;
            _selectlb.SelectionMode = OxyPlot.SelectionMode.Multiple;
            
            //_selectlb.BarWidth = 20;
            _ms._pm.Series.Add(_selectlb);
            _ms._pm.Series.Add(_lb);
            _ms.plotView1.Model = _ms._pm;

            _ms.DataSelection(cmbX, cmbY,_featurelayer);
            
            
        }
        
        public void DrawPlot()
        {
            //foreach(Feature feature in _ms.featurelayer.DataSet.Features)
            //{
            //    _ms.plotView1.Refresh();
            //    double X = Convert.ToDouble(feature.DataRow[cmbX.Text]);
            //    double Y = Convert.ToDouble(feature.DataRow[cmbY.Text]);
            //    _ms.value_X = cmbX.Text;
            //    _ms.value_Y = cmbY.Text;
            //    DataPoint dataPoint = new DataPoint(X, Y);
            //    _lb.Points.Add(dataPoint);
            //    if (!_ms._pointDic1.ContainsKey(dataPoint.ToString()))
            //    {
            //        _ms._pointDic1.Add(dataPoint.ToString(), feature.Fid);//字典里加东西；
            //    }
            //}
            //_ms.plotView1.Refresh();
            _ms.DrawPlot(cmbX, cmbY, _ms, _lb,_featurelayer);
        }

        public void ReSet()
        {
            _lb.Points.Clear();
            _ms.plotView1.Refresh();
        }


        private void _featurelayer_SelectionChanged(object sender, EventArgs e)
        {

            //if (_ms.featurelayer != null)
            //{
            //    List<IFeature> featureList = _ms.featurelayer.Selection.ToFeatureList();
            //    if (featureList.Count != 0)
            //    {

            //        foreach (Feature feature in _ms.featurelayer.Selection.ToFeatureList())
            //        {
            //            if (_ms.value_X != "" || _ms.value_Y != "")
            //            {
            //                double X = Convert.ToDouble(feature.DataRow[_ms.value_X]);
            //                double Y = Convert.ToDouble(feature.DataRow[_ms.value_Y]);
            //                DataPoint datapoint = new DataPoint(X, Y);
            //                _selectlb.Points.Add(datapoint);
            //            }
            //        }

            //    }
            //    else
            //    {
            //        _selectlb.Points.Clear();
            //    }
            //}
            //_ms.plotView1.Refresh();
            _ms.featurelayer_SelectionChanged(_selectlb, _ms,_featurelayer);
        }

        private void PlotMouseDown(object sender, OxyMouseDownEventArgs e)
        {
            //if (_lb.Points.Count != 0)
            //{
            //    DataPoint dd = new DataPoint(_lb.InverseTransform(e.Position).X, _lb.InverseTransform(e.Position).Y);
            //    _ms._startPoint1 = dd;

            //}
            _ms.PlotMouseDown(_lb, _ms, e);
        }

        private void PlotMouseMove(object sender, OxyMouseEventArgs e)
        {
            //if (_lb.Points.Count != 0)
            //{
            //    _ms._endPoint1 = _lb.InverseTransform(e.Position);
            //    if (_ms._startPoint1.X != 0 || _ms._startPoint1.Y != 0)
            //    {
            //        _ms._pm.Annotations.Clear();

            //        var rectangle = new PolygonAnnotation();
            //        rectangle.Layer = AnnotationLayer.BelowAxes;
            //        rectangle.StrokeThickness = 0.5;
            //        rectangle.Stroke = OxyColors.Red;
            //        rectangle.Fill = OxyColors.Transparent;
            //        rectangle.LineStyle = OxyPlot.LineStyle.Dot;

            //        rectangle.Points.Add(new DataPoint(_ms._startPoint1.X, _ms._startPoint1.Y));
            //        rectangle.Points.Add(new DataPoint(_ms._endPoint1.X, _ms._startPoint1.Y));
            //        rectangle.Points.Add(new DataPoint(_ms._endPoint1.X, _ms._endPoint1.Y));
            //        rectangle.Points.Add(new DataPoint(_ms._startPoint1.X, _ms._endPoint1.Y));

            //        _ms._pm.Annotations.Add(rectangle as Annotation);
            //        _ms._pm.InvalidatePlot(true);
            //    }
            //}
            _ms.PlotMouseMove(_lb,_ms,e);
        }

        private void PlotMouseUp(object sender, OxyMouseEventArgs e)
        {
            //List<int> indexs = SelectByRectangle();
            //_ms.featurelayer.Select(indexs);
            //DataPoint zerodata = new DataPoint(0, 0);
            //_ms._startPoint1 = zerodata;
            //_ms._pm.Annotations.Clear();
            //_ms.plotView1.Model.InvalidatePlot(true);
            _ms.PlotMouseUp(_selectlb, _lb, _ms,_featurelayer);
        }

        //一个返回LIST值的方法；
        private List<int> SelectByRectangle()
        {
            //_selectlb.Points.Clear();
            //_ms.featurelayer.ClearSelection();
            //List<int> result = new List<int>();
            //double minX, minY, maxX, maxY;
            //if (_ms._startPoint1.X < _ms._endPoint1.X)
            //{
            //    minX = _ms._startPoint1.X;
            //    maxX = _ms._endPoint1.X;
            //}
            //else
            //{
            //    minX = _ms._endPoint1.X;
            //    maxX = _ms._startPoint1.X;
            //}

            //if (_ms._startPoint1.Y < _ms._endPoint1.Y)
            //{
            //    minY = _ms._startPoint1.Y;
            //    maxY = _ms._endPoint1.Y;
            //}
            //else
            //{
            //    minY = _ms._endPoint1.Y;
            //    maxY = _ms._startPoint1.Y;
            //}

            //foreach (DataPoint item in _lb.Points)
            //{
            //    if (item.X < minX || item.X > maxX || item.Y < minY || item.Y > maxY)
            //    {
            //        continue;
            //    }
            //    else
            //    {
            //        int pointIndex = _ms._pointDic1[item.ToString()];
            //        result.Add(pointIndex);
            //        _selectlb.Points.Add(item);
            //    }
            //}

            //_ms.plotView1.Refresh();
            //return result;
            
            return _ms.SelectByRectangle(_selectlb, _lb, _ms,_featurelayer);
        }
    }
}

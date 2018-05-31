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
    public partial class LinePlot : UserControl
    {
        LineSeries _ls = new LineSeries();
         LineSeries _selectSeries1 = new LineSeries();
        IFeatureLayer _featurelayer = null;

        public MapStatistics _ms
        {
            get;
            set;
        }
        public LinePlot(MapStatistics ms,IFeatureLayer featurelayer)
        {
            InitializeComponent();
            _ms = ms;
            _featurelayer = featurelayer;
            _ms._pm.Axes.Clear();
            _featurelayer.SelectionChanged += new EventHandler(_featureLayer_SelectionChanged);
            //_ms._pm.PlotType = PlotType.Cartesian;
            _ms._pm.Background = OxyColors.White;
            _ms._pm.SelectionColor = OxyColors.Red;
            _ms._pm.MouseDown += PlotMouseDown;
            _ms._pm.MouseMove += PlotMouseMove;
            _ms._pm.MouseUp += PlotMouseUp;

            _ms._XAxis.Position = AxisPosition.Bottom;
            _ms._XAxis.Title = "X";
            _ms._YAxis.Title = "Y";

            _ms._pm.Axes.Add(_ms._XAxis);
            _ms._pm.Axes.Add(_ms._YAxis);

            _ls.LineStyle = OxyPlot.LineStyle.Automatic;
            _ls.Selectable = true;
            _ls.SelectionMode = OxyPlot.SelectionMode.Multiple;                                                                                                                                      
            _ls.StrokeThickness = 2;
            _ls.Color = OxyColors.Automatic;
            _ls.Smooth = true;

            _selectSeries1.LineStyle = OxyPlot.LineStyle.Solid;
            _selectSeries1.Color = OxyColors.Red;

            
            _ms._pm.Series.Add(_ls);
            _ms._pm.Series.Add(_selectSeries1);
            _ms.plotView1.Model=_ms._pm;

            cmbStyle.Items.Add("Dash");
            cmbStyle.Items.Add("DashDashDot");
            cmbStyle.Items.Add("DashDashDotDot");
            cmbStyle.Items.Add("LongDash");
            cmbStyle.Items.Add("Solid");

            cmbAngle.Items.Add("Smooth");
            cmbAngle.Items.Add("Sharp");


            _ms.DataSelection(cmbX, cmbY, _featurelayer);

        }

        public void DrawPlot()
        {
            _ms.DrawPlot(cmbX, cmbY, _ms, _ls, _featurelayer);
        }

        public void ReSet()
        {
            _ls.Points.Clear();
            _ms.plotView1.Refresh();
        }

        private void _featureLayer_SelectionChanged(object sender, EventArgs e)
        {

            _ms.featurelayer_SelectionChanged(_selectSeries1, _ms, _featurelayer);
        }
        private void PlotMouseDown(object sender, OxyMouseDownEventArgs e)
        {
            _ms.PlotMouseDown(_ls, _ms, e);
        }

        private void PlotMouseMove(object sender, OxyMouseEventArgs e)
        {
            _ms.PlotMouseMove(_ls, _ms, e);
        }

        private void PlotMouseUp(object sender, OxyMouseEventArgs e)
        {
            _ms.PlotMouseUp(_selectSeries1, _ls, _ms, _featurelayer);
        }

        private List<int> SelectByRectangle()
        {
            return _ms.SelectByRectangle(_selectSeries1, _ls, _ms, _featurelayer);
        }

        private void cmbStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbStyle.SelectedIndex)
            {
                case 0:
                    _ls.LineStyle = OxyPlot.LineStyle.Dash;
                    break;
                case 1:
                    _ls.LineStyle = OxyPlot.LineStyle.DashDashDot;
                    break;
                case 2:
                    _ls.LineStyle = OxyPlot.LineStyle.DashDashDotDot;
                    break;
                case 3:
                    _ls.LineStyle = OxyPlot.LineStyle.LongDash;
                    break;
                case 4:
                    _ls.LineStyle = OxyPlot.LineStyle.Solid;
                    break;
            }
        }

        private void cmbAngle_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbAngle.SelectedIndex)
            {
                case 0:
                    _ls.Smooth = true;
                    break;
                case 1:
                    _ls.Smooth = false;
                    break;

            }
        }


    }
}

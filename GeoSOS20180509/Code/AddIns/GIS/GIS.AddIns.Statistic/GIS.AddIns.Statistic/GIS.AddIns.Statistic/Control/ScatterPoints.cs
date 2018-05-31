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
using OxyPlot.Axes ;
using OxyPlot.WindowsForms;
using OxyPlot.Annotations;
using DotSpatial.Symbology;
using ICSharpCode.Core.WinForms;
using FrameWork.Gui;
using System.Collections;

namespace GIS.AddIns.Statistic
{
    public partial class ScatterPoints : UserControl
    {    
        #region Initial
        public LineSeries _series1 = new LineSeries();
        public LineSeries _selectSeries1 = new LineSeries();
        public LineSeries _regress = new LineSeries();
        public LineSeries _equalSeries = new LineSeries();
        List<double> _rePointx = new List<double>();
        List<double> _rePointy = new List<double>();
 
        //public Dictionary<string, int> _pointDic1 = new Dictionary<string, int>();
        //public DataPoint _startPoint1 = new DataPoint();
        //public DataPoint _endPoint1 = new DataPoint();
        public IFeatureLayer _featurelayer = null;
        //public string value_X ;
        //public  string value_Y;
        public MapStatistics _ms
        {
            get;
            set;
        }

        
        public ScatterPoints()
        {
            InitializeComponent();

            
        }


        public ScatterPoints(MapStatistics ms,IFeatureLayer featureLayer)
        {
            InitializeComponent();
            
            _ms = ms;
            _featurelayer = featureLayer;
            _featurelayer.SelectionChanged += new EventHandler(_featureLayer_SelectionChanged);
            //_ms._pm.PlotType = PlotType.Cartesian;
            _ms._pm.Background = OxyColors.White;
            _ms._pm.SelectionColor = OxyColors.Red;
            _ms._pm.MouseDown += PlotMouseDown;
            _ms._pm.MouseMove += PlotMouseMove;
            _ms._pm.MouseUp += PlotMouseUp;
            //_ms._pm.MouseLeave += PlotMouseLeave;

            _series1.LineStyle = OxyPlot.LineStyle.None;
            _series1.MarkerType = MarkerType.Circle;

            
            _ms._XAxis.Position = AxisPosition.Bottom;
          
            //_ms._XAxis.TicklineColor = OxyColors.CadetBlue;
            //_ms._XAxis.AxislineThickness = 2;
            //_ms._XAxis.AxislineColor = OxyColors.BlueViolet;

            //_regress.MarkerType = MarkerType.Circle;
            //_regress.MarkerFill = OxyColors.Black;
            _regress.LineStyle = OxyPlot.LineStyle.Solid;
            _regress.Color = OxyPlot.OxyColors.Black;
            _ms._pm.Series.Add(_regress);

            _ms._pm.Series.Add(_equalSeries);

            _ms._pm.Axes.Add(_ms._XAxis);
            _ms._pm.Axes.Add(_ms._YAxis);

            _series1.Selectable = true;
            _series1.MarkerFill = OxyColors.ForestGreen;
            _series1.SelectionMode = OxyPlot.SelectionMode.Multiple;
            _ms._pm.Series.Add(_series1);

            _selectSeries1.LineStyle = OxyPlot.LineStyle.None;
            _selectSeries1.MarkerType = MarkerType.Circle;
            _selectSeries1.MarkerFill = OxyColors.Red;
            _ms._pm.Series.Add(_selectSeries1);

            _ms.plotView1.Model = _ms._pm;

            cmbType.Items.Add("Circle");
            cmbType.Items.Add("Diamond");
            cmbType.Items.Add("Square");
            cmbType.Items.Add("Triangle");
            cmbType.Items.Add("Star");

            _ms.DataSelection(cmbX, cmbY, _featurelayer);



        }
       #endregion

        #region 实现选中高亮功能：统计图引起地图变化
        /// <summary>
        /// 鼠标按下获取初始点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlotMouseDown(object sender, OxyMouseDownEventArgs e)
        {
            _ms.PlotMouseDown(_series1, _ms, e);
        }
        /// <summary>
        /// 鼠标移动绘制选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlotMouseMove(object sender, OxyMouseEventArgs e)
        {
            _ms.PlotMouseMove(_series1, _ms, e);

        }
        /// <summary>
        /// 鼠标抬起，高亮显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlotMouseUp(object sender, OxyMouseEventArgs e)
        {
            _ms.PlotMouseUp(_selectSeries1, _series1, _ms, _featurelayer);

        }

        private List<int> SelectByRectangle()
        {
            return _ms.SelectByRectangle(_selectSeries1, _series1, _ms, _featurelayer);
        }
        #endregion

        /// <summary>
        /// 地图引起统计图变化
        /// </summary>
        private void _featureLayer_SelectionChanged(object sender, EventArgs e)
        {
            _ms.featurelayer_SelectionChanged(_selectSeries1, _ms, _featurelayer);
        }

        #region 主窗体调用函数
        /// <summary>
        /// 绘制统计图
        /// </summary>
        public void DrawPlot()
        {
            //_ms.DrawPlot(cmbX, cmbY, _ms, _series1, _featurelayer);
            foreach (Feature feature in _featurelayer.DataSet.Features)
            {
                _ms.plotView1.Refresh();
                double X = Convert.ToDouble(feature.DataRow[cmbX.Text]);
                _rePointx.Add(X);
                double Y = Convert.ToDouble(feature.DataRow[cmbY.Text]);
                _rePointy.Add(Y);
                _ms.value_X = cmbX.Text;
                _ms.value_Y = cmbY.Text;
                DataPoint dataPoint = new DataPoint(X, Y);
                _series1.Points.Add(dataPoint);
                if (!_ms._pointDic1.ContainsKey(dataPoint.ToString()))
                {
                    _ms._pointDic1.Add(dataPoint.ToString(), feature.Fid);
                }
            }


            //计算回归方程
            if (checkBox1.Checked == true)
            {
                CalcRegress(_rePointx, _rePointy);
            }




        }

        /// <summary>
        /// 回归方程算一算
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="count"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="maxErr"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public void CalcRegress(List<double> _xSeries, List<double> _ySeries)
        {

            double sumX=0, sumY=0, aveX=0, aveY=0;
            double SPxy = 0;
            double SSx = 0;
            double SSy = 0;
            double a, b,r1,r2,r;
            for(int i=0;i<_xSeries.Count;i++)
            {
                sumX += Convert.ToDouble(_xSeries[i]);
 
            }
            for (int i = 0; i < _ySeries.Count; i++)
            {
                sumY += Convert.ToDouble(_ySeries[i]);

            }
            aveX = sumX / _xSeries.Count;
            aveY = sumY / _ySeries.Count;


            for (int i = 0; i < _xSeries.Count; i++)
            {
                SPxy += (_xSeries[i] - aveX) * (_ySeries[i] - aveY);
                SSx += (_xSeries[i] - aveX) * (_xSeries[i] - aveX);
                SSy += (_ySeries[i] - aveY) * (_ySeries[i] - aveY);
            }

            if (SSy == 0)
            {
                MessageBox.Show("一条水平线，算不出来!");
            }
            
            //y=bx+a
            b = SPxy / SSx;
            a = aveY - b * aveX;


            //开始计算R²值
            r1 = SPxy * SPxy;//分子的平方
            r2 = SSx * SSy;//分母的平方
            r = r1 / r2;    //计算R²值

            //_regress.LabelFormatString = string.Format("y={0}x+{1}", b, a);
            //_regress.LabelFormatString = string.Format("R2:{0}",r);



            //下面代码计算最大偏差            
            //double maxErr = 0;
            double max_Y = 0;
            double max_X = 0;
            for (int i = 0; i < _xSeries.Count; i++)
            {
                double yi = a + b * _xSeries[i];
                DataPoint dataPoint = new DataPoint(_xSeries[i], yi);
                _regress.Points.Add(dataPoint);
                double absErrYi = Math.Abs(yi - _ySeries[i]);//假动作

                
                if (yi> max_Y)
                {
                    max_Y = yi;
                    max_X = _xSeries[i];
                }

            }

            //显示回归方程
            DataPoint dataPoint2 = new DataPoint(max_X * 0.95, max_Y * 0.95);
            _equalSeries.Points.Add(dataPoint2);
            //表示平方的话，按住alt加数字178
            _equalSeries.LabelFormatString = string.Format("R²={0}", r.ToString("f2"));//保留两位小数
            
        }
        /// <summary>
        /// 清空统计图元素
        /// </summary>
        public void reset()
        {
            _series1.Points.Clear();
            _ms.plotView1.Refresh();
        }

 
        /// <summary>
        /// 更改散点图风格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _series1.MarkerStroke = OxyColors.Automatic;
            switch (cmbType.SelectedIndex)
            {
                case 0:
                    {
                        _series1.MarkerType = MarkerType.Circle;
                    }
                    break;

                case 1:
                    {
                        _series1.MarkerType = MarkerType.Diamond;
                    }
                    break;

                case 2:
                    {
                        _series1.MarkerType = MarkerType.Square;
                    }
                    break;

                case 3:
                    {
                        _series1.MarkerType = MarkerType.Triangle;
                    }
                    break;

                case 4:
                    {
                        _series1.MarkerStroke = OxyColors.HotPink;
                        _series1.MarkerType = MarkerType.Star;
                    }
                    break;

            }
        }

        #endregion

        private void cmbRegression_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

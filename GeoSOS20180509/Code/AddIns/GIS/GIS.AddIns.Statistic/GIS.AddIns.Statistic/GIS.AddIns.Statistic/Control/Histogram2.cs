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
using System.Collections;

namespace GIS.AddIns.Statistic.Control
{
    public partial class Histogram2 : UserControl
    {
        /*
        _dic是生成直方图时将每个属性值与对应的fid号加到字典里，
        dic_back是为了从统计图反选矢量图层设计的，在生成直方图时就把每一个柱子里所包含的值的fid加入列表
        dic2与dic类似，用于选中图层引起直方图变化，相当于重新生成一个直方图
        */
        ColumnSeries _cs = new ColumnSeries();
        ColumnSeries _select_cs = new ColumnSeries();
        Dictionary<double, int> _dic = new Dictionary<double, int>();
        Dictionary<string, List<int>> _dic_back = new Dictionary<string, List<int>>();
        List<int> _dic_back2 = new List<int>();
        
        
        Dictionary<double, int> _dic2 = new Dictionary<double, int>();
        ArrayList _se_list = new ArrayList();
        CategoryAxis _Axis = new CategoryAxis();
        TextBox txtEdit = new TextBox();
        ColumnItem _item = new ColumnItem();
       
        IFeatureLayer _featurelayer = null;
        DataPoint _startPoint1 = new DataPoint();
        DataPoint _endPoint1 = new DataPoint();
        List<int> _fid_list1 = new List<int>();
        List<int> _fid_list2 = new List<int>();
        List<int> _fid_list3 = new List<int>();
        List<int> _fid_list4 = new List<int>();
        List<int> _fid_list5 = new List<int>();
        List<int> _fid_list6 = new List<int>();
        List<int> _fid_list7 = new List<int>();
        List<int> _fid_list8 = new List<int>();
        List<double> threshold_list = new List<double>();
        public string value_X;
        public string value_Y;
        int flag = 1;  //控制数量还是类别统计,1为数量，2为类别
        List<string> Xname = new List<string>();
        int[] _csValue = new int[] { };
        int a = 0, b = 0, c = 0, d = 0, e = 0, f = 0;
        public MapStatistics _ms
        {
            get;
            set;
        }
        public Histogram2(MapStatistics ms, IFeatureLayer featurelayer)
        {
            InitializeComponent();
            _ms = ms;
            _featurelayer = featurelayer;
            _ms._pm.Axes.Clear();
            _cs.FillColor = OxyColors.BlueViolet;
            _cs.ColumnWidth = 2;
            _cs.Selectable = true;
            _cs.IsStacked = true;
            _select_cs.IsStacked = true;
            

            _select_cs.FillColor = OxyColors.Cyan;
            _select_cs.ColumnWidth = 2;

            _ms._pm.Axes.Add(_ms._CAxis);
            _ms._pm.Axes.Add(_ms._YAxis);
            

            _ms._pm.Series.Add(_select_cs);
            _ms._pm.Series.Add(_cs);
            _ms.plotView1.Model = _ms._pm;

            _ms._pm.MouseDown += PlotMouseDown;
            _ms._pm.MouseMove += PlotMouseMove;
            _ms._pm.MouseUp += PlotMouseUp;
         
            _featurelayer.SelectionChanged += new EventHandler(_featureLayer_SelectionChanged);
            cmbInterval.Items.AddRange(new object[] { 3, 4, 5, 6,});
           
            if (flag == 2)
            {
                foreach (DataColumn col in _featurelayer.DataSet.DataTable.Columns)
                {
                   cmbStasticField.Items.Clear();
                   cmbStasticField.Items.Add(col.Caption);
                    //if (col.DataType != typeof(double) && col.DataType != typeof(Single) && col.DataType != typeof(float) && col.DataType != typeof(int))
                    //{
                    //    cmbStasticField.Items.Add(col.Caption);
                    //}
                  
                }
            }

            else
            {
                foreach (DataColumn col in _featurelayer.DataSet.DataTable.Columns)
                {

                   // cmbStasticField.Items.Clear();
                    if (col.DataType == typeof(double) || col.DataType == typeof(Single) || col.DataType == typeof(float) || col.DataType == typeof(int))
                    {
                        cmbStasticField.Items.Add(col.Caption);
                    }
                }
            }
        }

        public void DrawPlot()
        {
            _dic_back.Clear();
            double minY = 0;
            double maxY = 0;
            

            foreach (Feature feature in _featurelayer.DataSet.Features)
            {
                double Y = Convert.ToDouble(feature.DataRow[cmbStasticField.Text]);
                value_Y = cmbStasticField.Text;
                if (!_dic.ContainsKey(Y))
                {
                    _dic.Add(Y, feature.Fid);
                }


            }

            foreach (double j in _dic.Keys)
            {

                if (j > maxY)
                {
                    maxY = j;
                }
            }
            
            foreach (double m in _dic.Keys)
            {
              minY = m;
              break;
            }
            foreach (double k in _dic.Keys)
            {

                if (minY >= k)
                {
                    minY = k;
                }

            }

            /// <summary>
            /// 主要思想是：获取字段内数值的最大最小值，按照间距等分，显示在直方图中
            /// 以直方图柱子根数为6为例
            /// </summary>
            if (flag == 1)
            {
                
                _ms._YAxis.Title = "Frequency";
                _ms._CAxis.Title = cmbStasticField.SelectedItem.ToString();
                switch (cmbInterval.SelectedIndex)
                {
                    case 0:
                        {

                            double interval = (maxY - minY) / 3;
                            for (int m = 0; m < 3; m++)
                            {
                                threshold_list.Add(minY + m * interval);
                            }
                            
                            //int a = 0, b = 0, c = 0;
                            foreach (double i in _dic.Keys)
                            {
                                if (i > minY && i <= minY + interval)
                                {
                                    a++;
                                    _fid_list1.Add(_dic[i]);
                                }
                                else if (i > minY + interval && i <= minY + 2 * interval)
                                {
                                    b++;
                                    _fid_list2.Add(_dic[i]);
                                }
                                else
                                {
                                    c++;
                                    _fid_list3.Add(_dic[i]);
                                }

                            }


                            string col1 = Convert.ToInt32(minY).ToString() + "~" + Convert.ToInt32(minY + interval).ToString();
                            string col2 = Convert.ToInt32(minY + interval).ToString() + "~" + Convert.ToInt32(minY + 2 * interval).ToString();
                            string col3 = Convert.ToInt32(minY + 2 * interval).ToString() + "~" + Convert.ToInt32(maxY).ToString();

                            
                            _dic_back.Add(0.ToString(), _fid_list1);
                            _dic_back.Add(1.ToString(), _fid_list2);
                            _dic_back.Add(2.ToString(), _fid_list3);
                             
                            _cs.Items.AddRange(new[] { new ColumnItem { Value = a }, new ColumnItem { Value = b }, new ColumnItem { Value = c } });

                            _csValue = new int[] { a, b, c };
                            //X轴的标签
                            _ms._CAxis.Labels.AddRange(new[] { col1, col2, col3 });
                            
                            //_ms._CAxis.SetCurrentMaxValue(0, 0, maxY);
                            // _ms._CAxis.SetCurrentMinValue(0, 0, minY);

                            _cs.Items[0].CategoryIndex = 0;
                            _cs.Items[1].CategoryIndex = 1;
                            _cs.Items[2].CategoryIndex = 2;


                            _ms._pm.InvalidatePlot(true);
                        }
                        break;

                    case 1:
                        {
                            double interval = (maxY - minY) / 4;
                            for (int m = 0; m < 4; m++)
                            {
                                threshold_list.Add(minY + m * interval);
                            }
                          //  int a = 0, b = 0, c = 0, d = 0;
                            foreach (double i in _dic.Keys)
                            {
                                if (i > minY && i <= minY + interval)
                                {
                                    a++;
                                    _fid_list1.Add(_dic[i]);
                                }
                                else if (i > minY + interval && i <= minY + 2 * interval)
                                {
                                    b++;
                                    _fid_list2.Add(_dic[i]);
                                }
                                else if (i > minY + 2 * interval && i <= minY + 3 * interval)
                                {
                                    c++;
                                    _fid_list3.Add(_dic[i]);
                                }
                                else
                                {
                                    d++;
                                    _fid_list4.Add(_dic[i]);
                                }
                            }

                            _dic_back.Add(1.ToString(), _fid_list1);
                            _dic_back.Add(2.ToString(), _fid_list2);
                            _dic_back.Add(3.ToString(), _fid_list3);
                            _dic_back.Add(4.ToString(), _fid_list4);
                            


                            string col1 = Convert.ToInt32(minY).ToString() + "~" + Convert.ToInt32(minY + interval).ToString();
                            string col2 = Convert.ToInt32(minY + interval).ToString() + "~" + Convert.ToInt32(minY + 2 * interval).ToString();
                            string col3 = Convert.ToInt32(minY + 2 * interval).ToString() + "~" + Convert.ToInt32(minY + 3 * interval).ToString();
                            string col4 = Convert.ToInt32(minY + 3 * interval).ToString() + "~" + Convert.ToInt32(maxY).ToString();
                            _cs.Items.AddRange(new[] { new ColumnItem { Value = a }, new ColumnItem { Value = b }, new ColumnItem { Value = c }, new ColumnItem { Value = d } });
                            //_csValue = new double[] { a, b, c, d };
                            _ms._CAxis.Labels.AddRange(new[] { col1, col2, col3, col4 });

                            _csValue = new int[] { a, b, c, d };
                            
                            _cs.Items[0].CategoryIndex = 0;
                            _cs.Items[1].CategoryIndex = 1;
                            _cs.Items[2].CategoryIndex = 2;
                            _cs.Items[3].CategoryIndex = 3;

                            // threshold = new double[] { Convert.ToDouble(LBox.Items[0]), Convert.ToDouble(LBox.Items[1]), Convert.ToDouble(LBox.Items[2]), Convert.ToDouble(LBox.Items[3]) };
                            _ms._pm.InvalidatePlot(true);
                        }
                        break;
                    case 2:
                        {
                            double interval = (maxY - minY) / 5;
                            for (int m = 0; m < 5; m++)
                            {
                                threshold_list.Add(minY + m * interval);
                            }
                           // int a = 0, b = 0, c = 0, d = 0,e = 0;
                            foreach (double i in _dic.Keys)
                            {
                                if (i > minY && i <= minY + interval)
                                {
                                    a++;
                                    _fid_list1.Add(_dic[i]);
                                }
                                else if (i > minY + interval && i <= minY + 2 * interval)
                                {
                                    b++;
                                    _fid_list2.Add(_dic[i]);
                                }
                                else if (i > minY + 2 * interval && i <= minY + 3 * interval)
                                {
                                    c++;
                                    _fid_list3.Add(_dic[i]);
                                }
                                else if (i > minY + 3 * interval && i <= minY + 4 * interval)
                                {
                                    d++;
                                    _fid_list4.Add(_dic[i]);                          
                                
                                }
                                else
                                {
                                    e++;
                                    _fid_list5.Add(_dic[i]);
                                }
                            }
                            _dic_back.Add(1.ToString(), _fid_list1);
                            _dic_back.Add(2.ToString(), _fid_list2);
                            _dic_back.Add(3.ToString(), _fid_list3);
                            _dic_back.Add(4.ToString(), _fid_list4);
                            _dic_back.Add(5.ToString(), _fid_list5);
 

                            string col1 = Convert.ToInt32(minY).ToString() + "~" + Convert.ToInt32(minY + interval).ToString();
                            string col2 = Convert.ToInt32(minY + interval).ToString() + "~" + Convert.ToInt32(minY + 2 * interval).ToString();
                            string col3 = Convert.ToInt32(minY + 2 * interval).ToString() + "~" + Convert.ToInt32(minY + 3 * interval).ToString();
                            string col4 = Convert.ToInt32(minY + 3 * interval).ToString() + "~" + Convert.ToInt32(minY + 4 * interval).ToString();
                            string col5 = Convert.ToInt32(minY + 4 * interval).ToString() + "~" + Convert.ToInt32(maxY).ToString();
                            _cs.Items.AddRange(new[] { new ColumnItem { Value = a }, new ColumnItem { Value = b }, new ColumnItem { Value = c }, new ColumnItem { Value = d }, new ColumnItem { Value = e } });
                            
                            _csValue = new int[] { a, b, c, d, e };
                            _ms._CAxis.Labels.AddRange(new[] { col1, col2, col3, col4, col5 });
                            
                            _cs.Items[0].CategoryIndex = 0;
                            _cs.Items[1].CategoryIndex = 1;
                            _cs.Items[2].CategoryIndex = 2;
                            _cs.Items[3].CategoryIndex = 3;
                            _cs.Items[4].CategoryIndex = 4;
                            // threshold = new double[] { Convert.ToDouble(LBox.Items[0]), Convert.ToDouble(LBox.Items[1]), Convert.ToDouble(LBox.Items[2]), Convert.ToDouble(LBox.Items[3]) };
                            _ms._pm.InvalidatePlot(true);
                        }
                        break;

                    case 3:
                        {
                            double interval = (maxY - minY) / 6;
                            for (int m = 0; m < 6; m++)
                            {
                                threshold_list.Add(minY + m * interval);
                            }
                            
                            foreach (double i in _dic.Keys)
                            {
                             
                                if (i > minY && i <= minY + interval)
                                {
                                    a++;
                                    _fid_list1.Add(_dic[i]);
                                }
                                else if (i > minY + interval && i <= minY + 2 * interval)
                                {
                                    b++;
                                    _fid_list2.Add(_dic[i]);
                                }
                                else if (i > minY + 2 * interval && i <= minY + 3 * interval)
                                {
                                    c++;
                                    _fid_list3.Add(_dic[i]);
                                }
                                else if (i > minY + 3 * interval && i <= minY + 4 * interval)
                                {
                                    d++;
                                    _fid_list4.Add(_dic[i]);
                                }
                                else if (i > minY + 4 * interval && i <= minY + 5 * interval)
                                {
                                    e++;
                                    _fid_list5.Add(_dic[i]);
                                }
                                else
                                {
                                    f++;
                                    _fid_list6.Add(_dic[i]);
                                }
                              
                            }



                            _dic_back.Add(1.ToString(), _fid_list1);
                            _dic_back.Add(2.ToString(), _fid_list2);
                            _dic_back.Add(3.ToString(), _fid_list3);
                            _dic_back.Add(4.ToString(), _fid_list4);
                            _dic_back.Add(5.ToString(), _fid_list5);
                            _dic_back.Add(6.ToString(), _fid_list6);

                            string col1 = Convert.ToInt32(minY).ToString() + "~" + Convert.ToInt32(minY + interval).ToString();
                            string col2 = Convert.ToInt32(minY + interval).ToString() + "~" + Convert.ToInt32(minY + 2 * interval).ToString();
                            string col3 = Convert.ToInt32(minY + 2 * interval).ToString() + "~" + Convert.ToInt32(minY + 3 * interval).ToString();
                            string col4 = Convert.ToInt32(minY + 3 * interval).ToString() + "~" + Convert.ToInt32(minY + 4 * interval).ToString();
                            string col5 = Convert.ToInt32(minY + 4 * interval).ToString() + "~" + Convert.ToInt32(minY + 5 * interval).ToString();
                            string col6 = Convert.ToInt32(minY + 5 * interval).ToString() + "~" + Convert.ToInt32(maxY).ToString();


                            _cs.Items.AddRange(new[] { new ColumnItem { Value = a }, new ColumnItem { Value = b }, new ColumnItem { Value = c }, new ColumnItem { Value = d }, new ColumnItem { Value = e }, new ColumnItem { Value = f } });
                            _csValue = new int[] { a, b, c, d,e ,f };
                            _ms._CAxis.Labels.AddRange(new[] { col1, col2, col3, col4, col5, col6 });
                            
                            _cs.Items[0].CategoryIndex = 0;
                            _cs.Items[1].CategoryIndex = 1;
                            _cs.Items[2].CategoryIndex = 2;
                            _cs.Items[3].CategoryIndex = 3;
                            _cs.Items[4].CategoryIndex = 4;
                            _cs.Items[5].CategoryIndex = 5;
                            // threshold = new double[] { Convert.ToDouble(LBox.Items[0]), Convert.ToDouble(LBox.Items[1]), Convert.ToDouble(LBox.Items[2]), Convert.ToDouble(LBox.Items[3]) };
                            _ms._pm.InvalidatePlot(true);
                        }
                        break;
                }

            }
            if (flag == 2)
            {

                MessageBox.Show("Catagories is programming,coming soon...");

            }
        }


        #region 直方图变化引起图层变化
        /// <summary>
        /// 直方图变化引起图层被选中变化：鼠标选中直方图的某些频数，会对应直方图对于的要素被选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlotMouseMove(object sender, OxyMouseEventArgs e)
        {
            if (_cs.Items.Count != 0)
            {
                _endPoint1 = _cs.InverseTransform(e.Position);
                if (_startPoint1.X != 0 || _startPoint1.Y != 0)
                {
                    _ms._pm.Annotations.Clear();

                    var rectangle = new PolygonAnnotation();
                    rectangle.Layer = AnnotationLayer.BelowAxes;
                    rectangle.StrokeThickness = 1;
                    rectangle.Stroke = OxyColors.Red;
                    rectangle.Fill = OxyColors.Transparent;
                    rectangle.LineStyle = OxyPlot.LineStyle.Dot;

                    rectangle.Points.Add(new DataPoint(_startPoint1.X, _startPoint1.Y));
                    rectangle.Points.Add(new DataPoint(_endPoint1.X, _startPoint1.Y));
                    rectangle.Points.Add(new DataPoint(_endPoint1.X, _endPoint1.Y));
                    rectangle.Points.Add(new DataPoint(_startPoint1.X, _endPoint1.Y));

                    _ms._pm.Annotations.Add(rectangle as Annotation);
                    _ms._pm.InvalidatePlot(true);
                }
            }
        }

        private void PlotMouseDown(object sender, OxyMouseDownEventArgs e)
        {
            if (_cs.Items.Count != 0)
            {
                DataPoint dd = new DataPoint(_cs.InverseTransform(e.Position).X, _cs.InverseTransform(e.Position).Y);
                _startPoint1 = dd;

            }
        }

        private void PlotMouseUp(object sender, OxyMouseEventArgs e)
        {
            List<int> indexs = SelectByRectangle();
            _featurelayer.Select(indexs);
            DataPoint zerodata = new DataPoint(0, 0);
            _startPoint1 = zerodata;

            _ms._pm.Annotations.Clear();
            _ms.plotView1.Model.InvalidatePlot(true);
        }

        private List<int> SelectByRectangle()
        {
            _select_cs.Items.Clear();
            _featurelayer.ClearSelection();
            List<int> process = new List<int>();
            List<int> result = new List<int>();
            double _minX, _minY, _maxX, _maxY;
            if (_startPoint1.X < _endPoint1.X)
            {
                _minX = _startPoint1.X;
                _maxX = _endPoint1.X;
            }
            else
            {
                _minX = _endPoint1.X;
                _maxX = _startPoint1.X;
            }

            if (_startPoint1.Y < _endPoint1.Y)
            {
                _minY = _startPoint1.Y;
                _maxY = _endPoint1.Y;
            }
            else
            {
                _minY = _endPoint1.Y;
                _maxY = _startPoint1.Y;
            }

            foreach (ColumnItem item in _cs.Items)
            {
                if (item.Value < _minY || (item.CategoryIndex + 0.25) < _minX || (item.CategoryIndex - 0.25) > _maxX)
                {
                    continue;
                }
                else
                {

                   
                 foreach(string i in _dic_back.Keys )
                 {
                    if (_dic_back[i].Count==item.Value )
                    {
                        process = _dic_back[i];
                    }
                 }

                 foreach (int fid in process)
                 {
                        result.Add(fid);
                 }

                }

            }
            _ms.plotView1.Refresh();
            return result;
        }


        //选中图层引起直方图变化
        
        private void _featureLayer_SelectionChanged(object sender, EventArgs e)
        {
            double A = 0, B = 0, C = 0, D = 0, E = 0, F = 0;
            if (_cs.Items.Count != 0)
            {
                _dic2.Clear();
                if (_featurelayer != null)
                {
                    List<IFeature> featureList = _featurelayer.Selection.ToFeatureList();
                    if (featureList.Count != 0)
                    {

                        foreach (Feature feature in _featurelayer.Selection.ToFeatureList())
                        {

                            if (value_Y != "")
                            {
                                double Y2 = Convert.ToDouble(feature.DataRow[value_Y]);

                                if (!_dic2.ContainsKey(Y2))
                                {
                                    _dic2.Add(Y2, feature.Fid);
                                }

                            }
                        }


                        switch (_cs.Items.Count)
                        {

                            case 3:
                                {

                                    foreach (double i in _dic2.Keys)
                                    {
                                        if (i > threshold_list[0] && i <= threshold_list[1])
                                            A++;
                                        else if (i > threshold_list[1] && i <= threshold_list[2])
                                            B++;
                                        else
                                            C++;
                                    }
                                    _select_cs.Items.AddRange(new[] { new ColumnItem { Value = A }, new ColumnItem { Value = B }, new ColumnItem { Value = C } });
                                    _cs.Items[0].Value = _cs.Items[0].Value - A;
                                    _cs.Items[1].Value = _cs.Items[1].Value - B;
                                    _cs.Items[2].Value = _cs.Items[2].Value - C;
                                    //_select_cs.Items.Add(new ColumnItem { Value = a, CategoryIndex = 0 });
                                    //_select_cs.Items.Add(new ColumnItem { Value = b, CategoryIndex = 1 });
                                    //_select_cs.Items.Add(new ColumnItem { Value = c, CategoryIndex = 2 });
                                    _ms._pm.InvalidatePlot(true);
                                }
                                break;
                            case 4:
                                {
                                    foreach (double i in _dic2.Keys)
                                    {
                                        if (i > threshold_list[0] && i <= threshold_list[1])
                                            A++;
                                        else if (i > threshold_list[1] && i <= threshold_list[2])
                                            B++;
                                        else if (i > threshold_list[2] && i <= threshold_list[3])
                                            C++;
                                        else
                                            D++;
                                    }
                                    _select_cs.Items.AddRange(new[] { new ColumnItem { Value = A }, new ColumnItem { Value = B }, new ColumnItem { Value = C }, new ColumnItem { Value = D } });
                                    _cs.Items[0].Value = _cs.Items[0].Value - A;
                                    _cs.Items[1].Value = _cs.Items[1].Value - B;
                                    _cs.Items[2].Value = _cs.Items[2].Value - C;
                                    _cs.Items[3].Value = _cs.Items[3].Value - D;
                                    _ms._pm.InvalidatePlot(true);
                                }
                                break;
                            case 5:
                                {
                                    foreach (double i in _dic2.Keys)
                                    {
                                        if (i > threshold_list[0] && i <= threshold_list[1])
                                            A++;
                                        else if (i > threshold_list[1] && i <= threshold_list[2])
                                            B++;
                                        else if (i > threshold_list[2] && i <= threshold_list[3])
                                            C++;
                                        else if (i > threshold_list[3] && i <= threshold_list[4])
                                            D++;
                                        else
                                            E++;
                                    }
                                    _select_cs.Items.AddRange(new[] { new ColumnItem { Value = A }, new ColumnItem { Value = B }, new ColumnItem { Value = C }, new ColumnItem { Value = D }, new ColumnItem { Value = E } });
                                    _cs.Items[0].Value = _cs.Items[0].Value - A;
                                    _cs.Items[1].Value = _cs.Items[1].Value - B;
                                    _cs.Items[2].Value = _cs.Items[2].Value - C;
                                    _cs.Items[3].Value = _cs.Items[3].Value - D;
                                    _cs.Items[4].Value = _cs.Items[4].Value - E;
                                    _ms._pm.InvalidatePlot(true);
                                }
                                break;
                            case 6:
                                {
                                    foreach (double i in _dic2.Keys)
                                    {
                                        if (i > threshold_list[0] && i <= threshold_list[1])
                                            A++;
                                        else if (i > threshold_list[1] && i <= threshold_list[2])
                                            B++;
                                        else if (i > threshold_list[2] && i <= threshold_list[3])
                                            C++;
                                        else if (i > threshold_list[3] && i <= threshold_list[4])
                                            D++;
                                        else if (i > threshold_list[4] && i <= threshold_list[5])
                                            E++;
                                        else
                                            F++;
                                    }
                                    _select_cs.Items.AddRange(new[] { new ColumnItem { Value = A }, new ColumnItem { Value = B }, new ColumnItem { Value = C }, new ColumnItem { Value = D }, new ColumnItem { Value = E }, new ColumnItem { Value = F } });
                                    _cs.Items[0].Value = _cs.Items[0].Value - A;
                                    _cs.Items[1].Value = _cs.Items[1].Value - B;
                                    _cs.Items[2].Value = _cs.Items[2].Value - C;
                                    _cs.Items[3].Value = _cs.Items[3].Value - D;
                                    _cs.Items[4].Value = _cs.Items[4].Value - E;
                                    _cs.Items[5].Value = _cs.Items[5].Value - F;
                                    _ms._pm.InvalidatePlot(true);
                                }
                                break;
                          
                        }

                    }
                    else
                    {
                        _select_cs.Items.Clear();

                        switch (_cs.Items.Count)
                        {
                            case 3:
                                {
                                    _cs.Items[0].Value = _csValue[0];
                                    _cs.Items[1].Value = _csValue[1];
                                    _cs.Items[2].Value = _csValue[2];
                                    //_ms._pm.InvalidatePlot(true);
                                }
                                break;
                            case 4:
                                {
                                    _cs.Items[0].Value = _csValue[0];
                                    _cs.Items[1].Value = _csValue[1];
                                    _cs.Items[2].Value = _csValue[2];
                                    _cs.Items[3].Value = _csValue[3];
                                }
                                break;
                            case 5:
                                {
                                    _cs.Items[0].Value = _csValue[0];
                                    _cs.Items[1].Value = _csValue[1];
                                    _cs.Items[2].Value = _csValue[2];
                                    _cs.Items[3].Value = _csValue[3];
                                    _cs.Items[4].Value = _csValue[4];
                                }
                                break;
                            case 6:
                                {
                                    _cs.Items[0].Value = _csValue[0];
                                    _cs.Items[1].Value = _csValue[1];
                                    _cs.Items[2].Value = _csValue[2];
                                    _cs.Items[3].Value = _csValue[3];
                                    _cs.Items[4].Value = _csValue[4];
                                    _cs.Items[5].Value = _csValue[5];
                                }
                                break;

                        }
                        _ms._pm.InvalidatePlot(true);
                    }
                }
                _ms.plotView1.Refresh();
            }
        }
        
        
        #endregion
        /// <summary>
        /// 控制直方图统计的类型为数量型（如100，200....）还是类别型（如第一产业、第二产业...)
        /// 1为数量型，2为类别型
        /// </summary>
        private void Cbx_datatype_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            cmbStasticField.Refresh();
            if (Cbx_datatype.SelectedIndex == 0)
            {
                flag = 1;          
            }
            else flag = 2;
        }

        private void cmbInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            _dic_back.Clear();
            _dic.Clear();
            _dic2.Clear();
        }

        public void ReSet()
        {
            _dic_back.Clear();
            _dic.Clear();
            _dic2.Clear();
            _ms.plotView1.Refresh();
        }
        
    }
}

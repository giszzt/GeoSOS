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


namespace GIS.AddIns.Statistic
{
    public partial class Histogram : UserControl
    {
         ColumnSeries _cs = new ColumnSeries();
         ColumnSeries _select_cs = new ColumnSeries();
        Dictionary<double,int> _dic=new Dictionary<double,int>();
        Dictionary<string, List<int>> _dic_back = new Dictionary<string, List<int>>();
        Dictionary<double, int> _dic2 = new Dictionary<double, int>();
        ArrayList _se_list = new ArrayList();
        CategoryAxis _Axis = new CategoryAxis();
        TextBox txtEdit = new TextBox(); 
        ColumnItem _item=new ColumnItem();
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
        public string value_X;
        public string value_Y;
        double[] threshold = new double[] { };
        double[] _csValue = new double[] { };
        public MapStatistics _ms
        {
            get;
            set;
        }
        public Histogram(MapStatistics ms, IFeatureLayer featurelayer)
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
            
            
            _select_cs.FillColor = OxyColors.Red;
            _select_cs.ColumnWidth = 2;
            

            //_ms._XAxis.Position = AxisPosition.Bottom;

            
         

            _ms._pm.Axes.Add(_ms._CAxis);
            _ms._pm.Axes.Add(_ms._YAxis);

           
            _ms._pm.Series.Add(_select_cs);
            _ms._pm.Series.Add(_cs);
            _ms.plotView1.Model = _ms._pm;
           
            _ms._pm.MouseDown += PlotMouseDown;
            _ms._pm.MouseMove += PlotMouseMove;
            _ms._pm.MouseUp += PlotMouseUp;
            
           
 
            txtEdit.KeyDown += new KeyEventHandler(txtEdit_KeyDown);
            _featurelayer.SelectionChanged += new EventHandler(_featureLayer_SelectionChanged);

            Cbx_threshold.Items.AddRange(new object[] { 0,50,100,150,200,250,300});
            Cbx_Xname.Items.AddRange(new object[] { "A", "B", "C", "D", "E", "F", "G" });
            
            foreach (DataColumn col in _featurelayer.DataSet.DataTable.Columns)
            {
                cmbY.Items.Add(col.Caption);
                //if (col.DataType == typeof(double) || col.DataType == typeof(Single) || col.DataType == typeof(float) || col.DataType == typeof(int))
                //{

                //    cmbY.Items.Add(col.Caption);
                //}
            }
        }

        
        //绘制直方图
        public void   DrawPlot()
        {
            _dic_back.Clear();
            foreach (Feature feature in _featurelayer.DataSet.Features)
            {
               
                double Y = Convert.ToDouble(feature.DataRow[cmbY.Text]);              
                value_Y = cmbY.Text;
                if (!_dic.ContainsKey(Y))
                {
                    _dic.Add(Y, feature.Fid);
                }
               

            }

            switch (LBox.Items.Count)
            {
                case 3:
                    {

                        int a = 0, b = 0, c = 0;
                        foreach (double i in _dic.Keys)
                        {
                            if (i > Convert.ToDouble(LBox.Items[0]) && i <= Convert.ToDouble(LBox.Items[1]))
                            {
                                a++;
                                _fid_list1.Add(_dic[i]);
                            }
                            else if (i > Convert.ToDouble(LBox.Items[1]) && i <= Convert.ToDouble(LBox.Items[2]))
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
                        _dic_back.Add(a.ToString(), _fid_list1);
                        _dic_back.Add(b.ToString(), _fid_list2);
                        _dic_back.Add(c.ToString(), _fid_list3);
                        _cs.Items.AddRange(new[] { new ColumnItem { Value = a }, new ColumnItem { Value = b }, new ColumnItem { Value = c } });
                        _csValue = new double[] { a, b, c };
                        _ms._CAxis.Labels.AddRange(new[] { LBox2.Items[0].ToString(), LBox2.Items[1].ToString(), LBox2.Items[2].ToString() });
                        
                        _cs.Items[0].CategoryIndex = 0;
                        _cs.Items[1].CategoryIndex = 1;
                        _cs.Items[2].CategoryIndex = 2;

                         threshold = new double[] { Convert.ToDouble(LBox.Items[0]), Convert.ToDouble(LBox.Items[1]), Convert.ToDouble(LBox.Items[2]) };                        
                         _ms._pm.InvalidatePlot(true);
                    }
                    break;

                case 4:
                    {
                        int a = 0, b = 0, c = 0, d = 0;
                        foreach (double i in _dic.Keys)
                        {
                            if (i > Convert.ToDouble(LBox.Items[0]) && i <= Convert.ToDouble(LBox.Items[1]))
                            {
                                a++;
                                _fid_list1.Add(_dic[i]);
                            }
                            else if (i > Convert.ToDouble(LBox.Items[1]) && i <= Convert.ToDouble(LBox.Items[2]))
                            {
                                b++;
                                _fid_list2.Add(_dic[i]);
                            }
                            else if (i > Convert.ToDouble(LBox.Items[2]) && i <= Convert.ToDouble(LBox.Items[3]))
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
                        _dic_back.Add(a.ToString(), _fid_list1);
                        _dic_back.Add(b.ToString(), _fid_list2);
                        _dic_back.Add(c.ToString(), _fid_list3);
                        _dic_back.Add(d.ToString(), _fid_list4);
                        _cs.Items.AddRange(new[] { new ColumnItem { Value = a }, new ColumnItem { Value = b }, new ColumnItem { Value = c }, new ColumnItem { Value = d } });
                        _csValue = new double[] { a, b, c,d };
                        _ms._CAxis.Labels.AddRange(new[] { LBox2.Items[0].ToString(), LBox2.Items[1].ToString(), LBox2.Items[2].ToString(), LBox2.Items[3].ToString() });

                        _cs.Items[0].CategoryIndex = 0;
                        _cs.Items[1].CategoryIndex = 1;
                        _cs.Items[2].CategoryIndex = 2;
                        _cs.Items[3].CategoryIndex = 3;

                        threshold = new double[] { Convert.ToDouble(LBox.Items[0]), Convert.ToDouble(LBox.Items[1]), Convert.ToDouble(LBox.Items[2]), Convert.ToDouble(LBox.Items[3]) };
                        _ms._pm.InvalidatePlot(true);
                    }
                    break;
                case 5:
                    {
                        int a = 0, b = 0, c = 0, d = 0,e=0;
                        foreach (double i in _dic.Keys)
                        {
                            if (i > Convert.ToDouble(LBox.Items[0]) && i <= Convert.ToDouble(LBox.Items[1]))
                            {
                                a++;
                                _fid_list1.Add(_dic[i]);
                            }
                            else if (i > Convert.ToDouble(LBox.Items[1]) && i <= Convert.ToDouble(LBox.Items[2]))
                            {
                                b++;
                                _fid_list2.Add(_dic[i]);
                            }
                            else if (i > Convert.ToDouble(LBox.Items[2]) && i <=Convert.ToDouble(LBox.Items[3]))
                            {
                                c++;
                                _fid_list3.Add(_dic[i]);
                            }
                            else if (i > Convert.ToDouble(LBox.Items[3]) && i <= Convert.ToDouble(LBox.Items[4]))
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
                        _dic_back.Add(a.ToString(), _fid_list1);
                        _dic_back.Add(b.ToString(), _fid_list2);
                        _dic_back.Add(c.ToString(), _fid_list3);
                        _dic_back.Add(d.ToString(), _fid_list4);
                        _dic_back.Add(e.ToString(), _fid_list5);
                        _cs.Items.AddRange(new[] { new ColumnItem { Value = a }, new ColumnItem { Value = b }, new ColumnItem { Value = c }, new ColumnItem { Value = d }, new ColumnItem { Value = e } });
                        _csValue = new double[] { a, b, c, d,e };
                        _ms._CAxis.Labels.AddRange(new[] { LBox2.Items[0].ToString(), LBox2.Items[1].ToString(), LBox2.Items[2].ToString(), LBox2.Items[3].ToString(), LBox2.Items[4].ToString() });

                        _cs.Items[0].CategoryIndex = 0;
                        _cs.Items[1].CategoryIndex = 1;
                        _cs.Items[2].CategoryIndex = 2;
                        _cs.Items[3].CategoryIndex = 3;
                        _cs.Items[4].CategoryIndex = 4;

                         threshold = new double[] { Convert.ToDouble(LBox.Items[0]), Convert.ToDouble(LBox.Items[1]), Convert.ToDouble(LBox.Items[2]), Convert.ToDouble(LBox.Items[3]), Convert.ToDouble(LBox.Items[4]) };
                         _ms._pm.InvalidatePlot(true);
                    }
                    break;
                case 6:
                    {
                        int a = 0, b = 0, c = 0, d = 0, e = 0,f=0;
                        foreach (double i in _dic.Keys)
                        {
                            if (i > Convert.ToDouble(LBox.Items[0]) && i <= Convert.ToDouble(LBox.Items[1]))
                            {
                                a++;
                                _fid_list1.Add(_dic[i]);
                            }
                            else if (i > Convert.ToDouble(LBox.Items[1]) && i <= Convert.ToDouble(LBox.Items[2]))
                            {
                                b++;
                                _fid_list2.Add(_dic[i]);
                            }
                            else if (i > Convert.ToDouble(LBox.Items[2]) && i <= Convert.ToDouble(LBox.Items[3]))
                            {
                                c++;
                                _fid_list3.Add(_dic[i]);
                            }
                            else if (i > Convert.ToDouble(LBox.Items[3]) && i <= Convert.ToDouble(LBox.Items[4]))
                            {
                                d++;
                                _fid_list4.Add(_dic[i]);
                            }
                            else if (i > Convert.ToDouble(LBox.Items[4]) && i <= Convert.ToDouble(LBox.Items[5]))
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
                        _dic_back.Add(a.ToString(), _fid_list1);
                        _dic_back.Add(b.ToString(), _fid_list2);
                        _dic_back.Add(c.ToString(), _fid_list3);
                        _dic_back.Add(d.ToString(), _fid_list4);
                        _dic_back.Add(e.ToString(), _fid_list5);
                        _dic_back.Add(f.ToString(), _fid_list6);
                        _cs.Items.AddRange(new[] { new ColumnItem { Value = a }, new ColumnItem { Value = b }, new ColumnItem { Value = c }, new ColumnItem { Value = d }, new ColumnItem { Value = e }, new ColumnItem { Value = f } });
                        _csValue = new double[] { a, b, c, d, e ,f};
                        _ms._CAxis.Labels.AddRange(new[] { LBox2.Items[0].ToString(), LBox2.Items[1].ToString(), LBox2.Items[2].ToString(), LBox2.Items[3].ToString(), LBox2.Items[4].ToString(), LBox2.Items[5].ToString() });

                        _cs.Items[0].CategoryIndex = 0;
                        _cs.Items[1].CategoryIndex = 1;
                        _cs.Items[2].CategoryIndex = 2;
                        _cs.Items[3].CategoryIndex = 3;
                        _cs.Items[4].CategoryIndex = 4;
                        _cs.Items[5].CategoryIndex = 5;
                        
                        threshold = new double[] { Convert.ToDouble(LBox.Items[0]), Convert.ToDouble(LBox.Items[1]), Convert.ToDouble(LBox.Items[2]), Convert.ToDouble(LBox.Items[3]), Convert.ToDouble(LBox.Items[4]), Convert.ToDouble(LBox.Items[5]) };
                        _ms._pm.InvalidatePlot(true);
                    }
                    break;
                case 7:
                    {
                        int a = 0, b = 0, c = 0, d = 0,e = 0, f = 0,g=0;
                        foreach (double i in _dic.Keys)
                        {
                            if (i > Convert.ToDouble(LBox.Items[0]) && i <= Convert.ToDouble(LBox.Items[1]))
                            {
                                a++;
                                _fid_list1.Add(_dic[i]);
                            }
                            else if (i > Convert.ToDouble(LBox.Items[1]) && i <= Convert.ToDouble(LBox.Items[2]))
                            {
                                b++;
                                _fid_list2.Add(_dic[i]);
                            }
                            else if (i > Convert.ToDouble(LBox.Items[2]) && i <= Convert.ToDouble(LBox.Items[3]))
                            {
                                c++;
                                _fid_list3.Add(_dic[i]);
                            }
                            else if (i > Convert.ToDouble(LBox.Items[3]) && i <= Convert.ToDouble(LBox.Items[4]))
                            {
                                d++;
                                _fid_list4.Add(_dic[i]);
                            }
                            else if (i > Convert.ToDouble(LBox.Items[4]) && i <= Convert.ToDouble(LBox.Items[5]))
                            {
                                 e++;
                                _fid_list5.Add(_dic[i]);
                            }
                            else if (i > Convert.ToDouble(LBox.Items[5]) && i <= Convert.ToDouble(LBox.Items[6]))
                            {
                                 f++;
                                _fid_list6.Add(_dic[i]);
                            }
                            else
                            {
                                g++;
                                _fid_list7.Add(_dic[i]);
                            }
                        }
                        _dic_back.Add(a.ToString(), _fid_list1);
                        _dic_back.Add(b.ToString(), _fid_list2);
                        _dic_back.Add(c.ToString(), _fid_list3);
                        _dic_back.Add(d.ToString(), _fid_list4);
                        _dic_back.Add(e.ToString(), _fid_list5);
                        _dic_back.Add(f.ToString(), _fid_list6);
                        _dic_back.Add(g.ToString(), _fid_list7);
                        _cs.Items.AddRange(new[] { new ColumnItem { Value = a }, new ColumnItem { Value = b }, new ColumnItem { Value = c }, new ColumnItem { Value = d }, new ColumnItem { Value = e }, new ColumnItem { Value = f }, new ColumnItem { Value = g } });
                        _csValue = new double[] { a, b, c, d, e, f ,g};
                        _ms._CAxis.Labels.AddRange(new[] { LBox2.Items[0].ToString(), LBox2.Items[1].ToString(), LBox2.Items[2].ToString(), LBox2.Items[3].ToString(), LBox2.Items[4].ToString(), LBox2.Items[5].ToString(), LBox2.Items[6].ToString() });
                       
                        _cs.Items[0].CategoryIndex = 0;
                        _cs.Items[1].CategoryIndex = 1;
                        _cs.Items[2].CategoryIndex = 2;
                        _cs.Items[3].CategoryIndex = 3;
                        _cs.Items[4].CategoryIndex = 4;
                        _cs.Items[5].CategoryIndex = 5;
                        _cs.Items[6].CategoryIndex = 6;
                        
                        threshold = new double[] { Convert.ToDouble(LBox.Items[0]), Convert.ToDouble(LBox.Items[1]), Convert.ToDouble(LBox.Items[2]), Convert.ToDouble(LBox.Items[3]), Convert.ToDouble(LBox.Items[4]), Convert.ToDouble(LBox.Items[5]), Convert.ToDouble(LBox.Items[6]) };
                        _ms._pm.InvalidatePlot(true);
                    }
                    break;
                case 8:
                    {
                        int a = 0, b = 0, c = 0, d = 0, e = 0, f = 0, g = 0,h=0;
                        foreach (double i in _dic.Keys)
                        {
                            if (i > Convert.ToDouble(LBox.Items[0]) && i <= Convert.ToDouble(LBox.Items[1]))
                            {
                                a++;
                                _fid_list1.Add(_dic[i]);
                            }
                            else if (i > Convert.ToDouble(LBox.Items[1]) && i <= Convert.ToDouble(LBox.Items[2]))
                            {
                                b++;
                                _fid_list2.Add(_dic[i]);
                            }
                            else if (i > Convert.ToDouble(LBox.Items[2]) && i <= Convert.ToDouble(LBox.Items[3]))
                            {
                                c++;
                                _fid_list3.Add(_dic[i]);
                            }
                            else if (i > Convert.ToDouble(LBox.Items[3]) && i <= Convert.ToDouble(LBox.Items[4]))
                            {
                                d++;
                                _fid_list4.Add(_dic[i]);
                            }
                            else if (i > Convert.ToDouble(LBox.Items[4]) && i <= Convert.ToDouble(LBox.Items[5]))
                            {
                                e++;
                                _fid_list5.Add(_dic[i]);
                            }
                            else if (i > Convert.ToDouble(LBox.Items[5]) && i <= Convert.ToDouble(LBox.Items[6]))
                            {
                                f++;
                                _fid_list6.Add(_dic[i]);
                            }
                            else if (i > Convert.ToDouble(LBox.Items[6]) && i <= Convert.ToDouble(LBox.Items[7]))
                            {
                                g++;
                                _fid_list7.Add(_dic[i]);
                            }
                            else
                            {
                                g++;
                                _fid_list8.Add(_dic[i]);
                            }
                        }
                        _dic_back.Add(a.ToString(), _fid_list1);
                        _dic_back.Add(b.ToString(), _fid_list2);
                        _dic_back.Add(c.ToString(), _fid_list3);
                        _dic_back.Add(d.ToString(), _fid_list4);
                        _dic_back.Add(e.ToString(), _fid_list5);
                        _dic_back.Add(f.ToString(), _fid_list6);
                        _dic_back.Add(g.ToString(), _fid_list7);
                        _dic_back.Add(h.ToString(), _fid_list8);
                        _cs.Items.AddRange(new[] { new ColumnItem { Value = a }, new ColumnItem { Value = b }, new ColumnItem { Value = c }, new ColumnItem { Value = d }, new ColumnItem { Value = e }, new ColumnItem { Value = f }, new ColumnItem { Value = g }, new ColumnItem { Value = h } });
                        _csValue = new double[] { a, b, c, d, e, f, g ,h};
                        _ms._CAxis.Labels.AddRange(new[] { LBox2.Items[0].ToString(), LBox2.Items[1].ToString(), LBox2.Items[2].ToString(), LBox2.Items[3].ToString(), LBox2.Items[4].ToString(), LBox2.Items[5].ToString(), LBox2.Items[6].ToString(), LBox2.Items[7].ToString() });
                        
                        _cs.Items[0].CategoryIndex = 0;
                        _cs.Items[1].CategoryIndex = 1;
                        _cs.Items[2].CategoryIndex = 2;
                        _cs.Items[3].CategoryIndex = 3;
                        _cs.Items[4].CategoryIndex = 4;
                        _cs.Items[5].CategoryIndex = 5;
                        _cs.Items[6].CategoryIndex = 6;
                        _cs.Items[7].CategoryIndex = 7;
                        
                        threshold = new double[] { Convert.ToDouble(LBox.Items[0]), Convert.ToDouble(LBox.Items[1]), Convert.ToDouble(LBox.Items[2]), Convert.ToDouble(LBox.Items[3]), Convert.ToDouble(LBox.Items[4]), Convert.ToDouble(LBox.Items[5]), Convert.ToDouble(LBox.Items[6]), Convert.ToDouble(LBox.Items[7]) };
                         _ms._pm.InvalidatePlot(true);
                    }

                    break;

            }

            //_ms._pm.InvalidatePlot(true);

            
            
        }
        //选中图层引起直方图变化
        private void _featureLayer_SelectionChanged(object sender, EventArgs e)
        {
            double A = 0, B = 0, C = 0, D = 0, E = 0, F = 0, G = 0, H = 0;
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
                                        if (i > threshold[0] && i <= threshold[1])
                                            A++;
                                        else if (i > threshold[1] && i <= threshold[2])
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
                                        if (i > threshold[0] && i <= threshold[1])
                                            A++;
                                        else if (i > threshold[1] && i <= threshold[2])
                                            B++;
                                        else if (i > threshold[2] && i <= threshold[3])
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
                                        if (i > threshold[0] && i <= threshold[1])
                                            A++;
                                        else if (i > threshold[1] && i <= threshold[2])
                                            B++;
                                        else if (i > threshold[2] && i <= threshold[3])
                                            C++;
                                        else if (i > threshold[3] && i <= threshold[4])
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
                                        if (i > threshold[0] && i <= threshold[1])
                                            A++;
                                        else if (i > threshold[1] && i <= threshold[2])
                                            B++;
                                        else if (i > threshold[2] && i <= threshold[3])
                                            C++;
                                        else if (i > threshold[3] && i <= threshold[4])
                                            D++;
                                        else if (i > threshold[4] && i <= threshold[5])
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
                            case 7:
                                {
                                    foreach (double i in _dic2.Keys)
                                    {
                                        if (i > threshold[0] && i <= threshold[1])
                                            A++;
                                        else if (i > threshold[1] && i <= threshold[2])
                                            B++;
                                        else if (i > threshold[2] && i <= threshold[3])
                                            C++;
                                        else if (i > threshold[3] && i <= threshold[4])
                                            D++;
                                        else if (i > threshold[4] && i <= threshold[5])
                                            E++;
                                        else if (i > threshold[5] && i <= threshold[6])
                                            F++;
                                        else
                                            G++;
                                    }
                                    _select_cs.Items.AddRange(new[] { new ColumnItem { Value = A }, new ColumnItem { Value = B }, new ColumnItem { Value = C }, new ColumnItem { Value = D }, new ColumnItem { Value = E }, new ColumnItem { Value = F }, new ColumnItem { Value = G } });
                                    _cs.Items[0].Value = _cs.Items[0].Value - A;
                                    _cs.Items[1].Value = _cs.Items[1].Value - B;
                                    _cs.Items[2].Value = _cs.Items[2].Value - C;
                                    _cs.Items[3].Value = _cs.Items[3].Value - D;
                                    _cs.Items[4].Value = _cs.Items[4].Value - E;
                                    _cs.Items[5].Value = _cs.Items[5].Value - F;
                                    _cs.Items[6].Value = _cs.Items[6].Value - G;
                                    _ms._pm.InvalidatePlot(true);
                                }
                                break;
                            case 8:
                                {
                                    foreach (double i in _dic2.Keys)
                                    {
                                        if (i > threshold[0] && i <= threshold[1])
                                            A++;
                                        else if (i > threshold[1] && i <= threshold[2])
                                            B++;
                                        else if (i > threshold[2] && i <= threshold[3])
                                            C++;
                                        else if (i > threshold[3] && i <= threshold[4])
                                            D++;
                                        else if (i > threshold[4] && i <= threshold[5])
                                            E++;
                                        else if (i > threshold[5] && i <= threshold[6])
                                            F++;
                                        else if (i > threshold[6] && i <= threshold[7])
                                            G++;
                                        else
                                            H++;
                                    }
                                    _select_cs.Items.AddRange(new[] { new ColumnItem { Value = A }, new ColumnItem { Value = B }, new ColumnItem { Value = C }, new ColumnItem { Value = D }, new ColumnItem { Value = E }, new ColumnItem { Value = F }, new ColumnItem { Value = G }, new ColumnItem { Value = H } });
                                    _cs.Items[0].Value = _cs.Items[0].Value - A;
                                    _cs.Items[1].Value = _cs.Items[1].Value - B;
                                    _cs.Items[2].Value = _cs.Items[2].Value - C;
                                    _cs.Items[3].Value = _cs.Items[3].Value - D;
                                    _cs.Items[4].Value = _cs.Items[4].Value - E;
                                    _cs.Items[5].Value = _cs.Items[5].Value - F;
                                    _cs.Items[6].Value = _cs.Items[6].Value - G;
                                    _cs.Items[7].Value = _cs.Items[7].Value - H;
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
                            case 7:
                                {
                                    _cs.Items[0].Value = _csValue[0];
                                    _cs.Items[1].Value = _csValue[1];
                                    _cs.Items[2].Value = _csValue[2];
                                    _cs.Items[3].Value = _csValue[3];
                                    _cs.Items[4].Value = _csValue[4];
                                    _cs.Items[5].Value = _csValue[5];
                                    _cs.Items[6].Value = _csValue[6];
                                }
                                break;
                            case 8:
                                {
                                    _cs.Items[0].Value = _csValue[0];
                                    _cs.Items[1].Value = _csValue[1];
                                    _cs.Items[2].Value = _csValue[2];
                                    _cs.Items[3].Value = _csValue[3];
                                    _cs.Items[4].Value = _csValue[4];
                                    _cs.Items[5].Value = _csValue[5];
                                    _cs.Items[6].Value = _csValue[6];
                                    _cs.Items[7].Value = _csValue[7];
                                }
                                break;
                        }
                        _ms._pm.InvalidatePlot(true);
                    }
                }
                _ms.plotView1.Refresh();
            }
        }
        #region 直方图引起图层变化
        private void PlotMouseMove(object sender, OxyMouseEventArgs e)
        {
            if (_cs.Items.Count!=0)
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
            double minX, minY, maxX, maxY;
            if (_startPoint1.X < _endPoint1.X)
            {
                minX = _startPoint1.X;
                maxX = _endPoint1.X;
            }
            else
            {
                minX = _endPoint1.X;
                maxX = _startPoint1.X;
            }

            if (_startPoint1.Y < _endPoint1.Y)
            {
                minY = _startPoint1.Y;
                maxY = _endPoint1.Y;
            }
            else
            {
                minY = _endPoint1.Y;
                maxY = _startPoint1.Y;
            }

            foreach (ColumnItem  item in _cs.Items)
            {
                if (item.Value < minY || (item.CategoryIndex + 0.25) < minX || (item.CategoryIndex - 0.25) > maxX)
                    {
                        continue;
                    }
                    else
                    {
                        process = _dic_back[item.Value.ToString()];
                        foreach (int fid in process)
                        {
                            result.Add(fid);
                        }

                        //_select_cs.Items.Add(new ColumnItem {Value=item.Value,CategoryIndex =item.CategoryIndex } );
                        
                    }
                
            }

            _ms.plotView1.Refresh();
            return result;
        }

        #endregion


        #region 数据添加与修改
        private void Btn1_Click(object sender, EventArgs e)
        {
            LBox.Items.Add(Cbx_threshold.Text);
            
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            if (LBox.SelectedIndex == -1)
                MessageBox.Show("Please select an item");
            else
                LBox.Items.RemoveAt(LBox.SelectedIndex);
        }

        private void Btn3_Click(object sender, EventArgs e)
        {
            LBox2.Items.Add(Cbx_Xname.Text);
           
        }
        #endregion
        #region listbox 编辑
        //使listbox值可自行编辑
        private void LBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            e.Graphics.DrawString(LBox.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds); 
        }

        /// <summary>  
        /// KeyDown事件定义  
        /// </summary>  
        private void txtEdit_KeyDown(object sender, KeyEventArgs e)
        {
            //Enter键 更新项并隐藏编辑框  
            if (e.KeyCode == Keys.Enter)
            {
                LBox.Items[LBox.SelectedIndex] = txtEdit.Text;
                txtEdit.Visible = false;
            }
            //Esc键 直接隐藏编辑框  
            if (e.KeyCode == Keys.Escape)
                txtEdit.Visible = false;
        }

        /// <summary>  
        /// 双击项时显示编辑框  
        /// </summary>  
        private void LBox_DoubleClick(object sender, EventArgs e)
        {
            if (LBox.Items.Count != 0)
            {
                int itemSelected = LBox.SelectedIndex;
                string itemText = LBox.Items[itemSelected].ToString();

                Rectangle rect = LBox.GetItemRectangle(itemSelected);
                txtEdit.Parent = LBox;
                txtEdit.Bounds = rect;
                txtEdit.Multiline = true;
                txtEdit.Visible = true;
                txtEdit.Text = itemText;
                txtEdit.Focus();
                txtEdit.SelectAll();
            }
        }

        /// <summary>  
        /// 点击其它项 隐藏编辑框  
        /// </summary>  
        private void LBox_MouseClick(object sender, MouseEventArgs e)
        {
            txtEdit.Visible = false ;
        }
        #endregion


    }
}

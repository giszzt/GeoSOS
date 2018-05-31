using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpCode.Core.WinForms;
using DotSpatial.Controls;
using DotSpatial.Symbology;
using DotSpatial.Data;
using System.Diagnostics;

namespace GIS.Common.Dialogs
{
    public partial class Attributes : UserControl
    {

        #region Private variable
        private Legend _legend;
        private IMap _map;
        private ToolStrip _toolStrip;
        private FeatureLayer _activeLayer;
        private DataTable _attiveTable;
        private int _attivePageIndex;
        private DataGridView _attiveDataGridView;
        private bool _pageLocker;

        private String _fidField = "FID";
        private bool _ignoreTableSelectionChanged;
        private bool _ignoreSelectionChanged;
        // 已展示属性表的图层
        Dictionary<string, DataGridView> _nameGridDictory;

        #endregion

        #region construct
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Map"></param>
        /// <param name="legend"></param>
        public Attributes(IMap Map, Legend legend)
        {
            // 界面初始化
            InitializeComponent();

            // 工具条插件口
            _toolStrip = ToolbarService.CreateToolStrip(this, "/FrameWork/ToolStrips/Attributes");
            _toolStrip.GripStyle = ToolStripGripStyle.Hidden;
            _toolStrip.Dock = DockStyle.Top;
            Controls.Add(_toolStrip);

            //get current map object
            _map = Map;
            _legend = legend;
            _nameGridDictory = new Dictionary<string, DataGridView>();

            // tab event binding            
            tabControl.SelectedIndexChanged += tabControlSelectedPageChanged;
            // 清空默认的tab
            tabControl.TabPages.Clear();



        }

        #endregion

        #region Properties

        public int PageIndex
        {
            get { return _attivePageIndex; }
            set { _attivePageIndex = value; }
        }

        public bool IgnoreTableSelectionChanged        
        {
            get { return _ignoreTableSelectionChanged; }
            set { _ignoreTableSelectionChanged = value; }
        }

        public FeatureLayer AttributesLayer
        {
            get { return _activeLayer; }
            set { _activeLayer = value; }
        }

        public DataTable AttributesTable
        {
            get { return _attiveTable; }
            set { _attiveTable = value; }
        }

        #endregion

        #region Event

        /// <summary>
        /// 字段计算器新增字段，更新回来
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AttributeCalNewFieldAdded(object sender, EventArgs e)
        {
            AttributeCalculator attributeCal = sender as AttributeCalculator;
            if (attributeCal != null)
            {
                _activeLayer.DataSet = attributeCal.FeatureSet;
            }
        }

        /// <summary>
        /// featurelayer's selection change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedFeaturesChanged(object sender, EventArgs e)
        {
            SetSelectionFromLayer();
        }



        /// <summary>
        /// 将数据表的选择集的改变同步到地图上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewSelectionChanged(object sender, EventArgs e)
        {

            if (_activeLayer == null)
            {
                return;
            }

            if (!_activeLayer.DataSet.AttributesPopulated)
            {
                return; // For now can handle only populated data sets with fid column
            }
            Debug.Assert(_fidField != null);

            // 如果表更改被忽略， 则结束
            if (_ignoreTableSelectionChanged)
            {
                return;
            }

            // 在将表更改同步到地图上时，要忽略地图更改，否则会形成死循环
            _ignoreSelectionChanged = true;

            try
            {
                //manage selection using the Selection property
                IndexSelection sel = _activeLayer.Selection as IndexSelection;
                if (sel != null)
                {
                    sel.SuspendChanges();
                    //set the selected state of the corresponding feature
                    foreach (DataGridViewRow row in _attiveDataGridView.Rows)
                    {
                        int fid = (int)row.Cells[_fidField].Value;
                        if (row.Selected)
                        {
                            sel.Add(fid);
                        }
                        else
                        {
                            sel.Remove(fid);
                        }
                    }
                    sel.ResumeChanges();
                }
                else
                {
                    //List<int> adds = new List<int>();
                    //List<int> removes = _selectedRows.ToList();
                    //foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    //{
                    //    if (!_selectedRows.Contains(row.Index))
                    //    {
                    //        adds.Add(row.Index);
                    //    }
                    //    removes.Remove(row.Index);
                    //}
                    //_featureLayer.Select(adds);
                    //_featureLayer.UnSelect(removes);
                }
            }
            finally
            {
                _ignoreSelectionChanged = false;
            }
        }



        private void tabControlSelectedPageChanged(object sender, EventArgs e)
        {
            if (tabControl.TabPages.Count > 0)
            {
                for (int i = 0; i < _map.Layers.Count; i++)
                {
                    if (_map.Layers[i].DataSet.Name == tabControl.SelectedTab.Text)
                    {
                        _activeLayer = _map.Layers[i] as FeatureLayer;

                        _attiveTable = _activeLayer.DataSet.DataTable;
                        _attivePageIndex = tabControl.SelectedIndex;
                        try
                        {
                            _attiveDataGridView = _nameGridDictory[_activeLayer.Name.Trim()];
                        }
                        catch (Exception e1)
                        {

                        }

                        //if (_pageLocker == true)
                        //{
                        //    _attiveDataGridView.DataSource = _activeLayer.DataSet.DataTable;
                        //}

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < tabControl.TabPages.Count; i++)
                {
                    if (tabControl.GetTabRect(i).Contains(new Point(e.X, e.Y)))
                    {
                        this.tabControl.SelectedTab = this.tabControl.TabPages[i];
                        break;
                    }
                }
                this.tabControl.ContextMenuStrip = this.TabContextMenuStrip;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl_MouseLeave(object sender, EventArgs e)
        {
            this.tabControl.ContextMenuStrip = null;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _nameGridDictory.Remove(this.tabControl.SelectedTab.Text.Trim());
            tabControl.TabPages.Remove(this.tabControl.SelectedTab);
            _attiveDataGridView = null;
            _attiveTable = null;
            _activeLayer = null;
            // 删除字典内容

        }


        /// <summary>
        /// DataGridView的双击事件响应函数
        /// 将显示范围缩放到被选中的要素上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (_activeLayer == null)
            {
                return;
            }

            _activeLayer.ZoomToSelectedFeatures();
        }



        #endregion

        #region Private Method

        //// 给数据表添加FID字段，记录它的序号
        //private DataTable CreatFIDTable()
        //{
        //    DataColumn dc = new DataColumn("FID");
        //    dc.DataType = typeof(int);
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add(dc);
        //    for (int i = 0; i < _attiveTable.Rows.Count; i++)
        //    {
        //        DataRow row = dt.NewRow();
        //        row[0] = i;
        //    }

        //    dt.Merge(_attiveTable);

        //    return dt;
        //}


        // 根据feature layer的选中状态，更新data grid view的选中状态
        private void SetSelectionFromLayer()
        {
            if (_activeLayer == null)
            {
                return;
            }
            if (_ignoreSelectionChanged)
            {
                return;
            }

            _attiveDataGridView.SuspendLayout();

            _ignoreSelectionChanged = true;
            _ignoreTableSelectionChanged = true;

            try
            {
                if (!_activeLayer.EditMode)
                {
                    // 得到要素的绘制状态参数
                    FastDrawnState[] states = _activeLayer.DrawnStates;
                    if (states == null)
                    {
                        return;
                    }

                    if (_activeLayer.DataSet.AttributesPopulated)
                    {
                        foreach (DataGridViewRow row in _attiveDataGridView.Rows)
                        {
                            int fid = (int)row.Cells[_fidField].Value;
                            row.Selected = states[fid].Selected;
                        }
                    }
                }
                else
                {
                    IFeatureSelection fs = _activeLayer.Selection as IFeatureSelection;
                    if (fs == null)
                    {
                        return;
                    }
                    if (_activeLayer.DataSet.AttributesPopulated)
                    {
                        foreach (DataGridViewRow row in _attiveDataGridView.Rows)
                        {
                            int fid = (int)row.Cells[_fidField].Value;
                            row.Selected = fs.Filter.DrawnStates[_activeLayer.DataSet.Features[fid]].IsSelected;
                        }
                    }
                }
            }
            finally
            {
                _ignoreSelectionChanged = false;
                _ignoreTableSelectionChanged = false;
                _attiveDataGridView.ResumeLayout();
            }
        }




        #endregion

        #region Public Method

        /// <summary>
        /// 字段计算器
        /// </summary>
        public void FieldCalculationExecute()
        {
            AttributeCalculator attributeCal = new AttributeCalculator();
            attributeCal.Show();
            attributeCal.FeatureSet = _activeLayer.DataSet;
            List<string> fieldList = new List<string>();
            foreach (DataColumn dataCol in _activeLayer.DataSet.DataTable.Columns)
            {
                fieldList.Add(dataCol.ToString());
            }
            attributeCal.LoadTableField(fieldList); //Load all fields
            attributeCal.NewFieldAdded += AttributeCalNewFieldAdded; //when user click new field to put the calulated values.
        }


        /// <summary>
        /// Shows all rows (both selected and unselected).
        /// </summary>
        public void ShowAllRows()
        {
            if (_activeLayer == null)
            {
                return;
            }
            if (_activeLayer.DataSet.AttributesPopulated)
            {
                _ignoreTableSelectionChanged = true;
                _ignoreSelectionChanged = true;

                _attiveDataGridView.DataSource = _activeLayer.DataSet.DataTable;


                for (int i = 0; i < _activeLayer.DataSet.DataTable.Rows.Count; i++)
                {
                    if (_activeLayer.DrawnStates[i].Selected)
                    {
                        _attiveDataGridView.Rows[i].Selected = true;

                    }
                }

                _ignoreTableSelectionChanged = false;
                _ignoreSelectionChanged = false;
            }
            else
            {
                //_attributeCache = new AttributeCache(_featureLayer.DataSet, 16);
                //dataGridView1.RowCount = _featureLayer.DrawnStates.Length;
            }

            Refresh();
        }



        /// <summary>
        /// Limits the displayed rows only to rows which are selected.
        /// </summary>
        public void ShowOnlySelectedRows()
        {
            _ignoreSelectionChanged = true;
            _ignoreTableSelectionChanged = true;
            if (_activeLayer == null)
            {
                return;
            }
            if (_activeLayer.DataSet.AttributesPopulated)
            {
                _attiveDataGridView.SuspendLayout();

                // 新建一个空的table
                var selection = new DataTable();
                selection.Columns.AddRange(_activeLayer.DataSet.GetColumns());
                if (!selection.Columns.Contains(_fidField))
                {
                    selection.Columns.Add(_fidField, typeof(int));
                }

                //if (_selectionIndices == null) _selectionIndices = new List<int>();
                //_selectionIndices.Clear();
                //_selectedRows.Clear();

                // 填充新的table
                int numRows = _activeLayer.DataSet.DataTable.Rows.Count;
                for (int row = 0; row < numRows; row++)
                {
                    if (!_activeLayer.DrawnStates[row].Selected)
                    {
                        continue;
                    }
                    DataRow dr = selection.NewRow();
                    dr.ItemArray = _activeLayer.DataSet.DataTable.Rows[row].ItemArray;
                    selection.Rows.Add(dr);
                    //_selectionIndices.Add(row);
                    //_selectedRows.Add(row);
                }

                // 更改数据源
                _attiveDataGridView.DataSource = selection;
                _attiveDataGridView.SelectAll();


                _attiveDataGridView.ResumeLayout();
            }
            else
            {
                //_attributeCache = new AttributeCache(_featureLayer.Selection, 16);
                //dataGridView1.Rows.Clear(); // without this setting rowCount takes a looooong time
                //dataGridView1.RowCount = _featureLayer.Selection.Count;
            }

            _ignoreTableSelectionChanged = false;
            _ignoreSelectionChanged = false;
            //_showOnlySelectedRows = true;
        }


        /// <summary>
        /// 创建一个新的属性表tab
        /// </summary>
        /// <param name="name"></param>
        public void CreateNewPage(string name)
        {
            // 如果图层的属性表已添加，无操作
            if (_nameGridDictory.ContainsKey(name.Trim()))
            {
                return;

            }


            _pageLocker = false;

            // get the active layer
            _activeLayer = _legend.SelectedLegendMenuItem as FeatureLayer;

            // create a new tab
            TabPage page = new TabPage(name);
            tabControl.TabPages.Add(page);
            tabControl.SelectedTab = page;

            // create a new data grid view
            DataGridView _newTable = new DataGridView();

            // add fid field to active layer in order to link map and table
            _activeLayer.DataSet.AddFid();

            _attiveTable = _activeLayer.DataSet.DataTable;
            _newTable.DataSource = _attiveTable;

            // listen to select change
            _activeLayer.SelectionChanged += SelectedFeaturesChanged;

            page.Controls.Add(_newTable);
            _newTable.Dock = DockStyle.Fill;

            _map.FunctionMode = FunctionMode.Select;

            _attiveDataGridView = _newTable;
            _attiveDataGridView.AllowUserToAddRows = false;
            _attiveDataGridView.SelectionChanged += DataGridViewSelectionChanged;
            _attiveDataGridView.DoubleClick += DataGridView_DoubleClick;
            _nameGridDictory[name.Trim()] = _attiveDataGridView;

            _pageLocker = true;

        }

        /// <summary>
        /// 删除属性表
        /// </summary>
        /// <param name="removeItem"></param>
        public void RemovePage(ILegendItem removeItem)
        {
            //MessageBox.Show(removeItem.LegendText);
            foreach (TabPage tp in tabControl.TabPages)
            {
                if (removeItem.LegendText == tp.Text)
                {
                    // 删除tab页面
                    tabControl.TabPages.Remove(tp);
                    // 删除字典内容
                    _nameGridDictory.Remove(removeItem.LegendText.Trim());
                    if (tabControl.TabPages.Count == 0)
                    {
                        //this.Hide();
                    }

                }
            }
        }

        public DataTable GetTable()
        {
            return _attiveTable;
        }

        public int GetPage()
        {
            return _attivePageIndex;
        }

        public DataGridView GetDgv()
        {
            return _attiveDataGridView;
        }

        public FeatureLayer GetLayer()
        {
            return _activeLayer;
        }

        #endregion

    }
}

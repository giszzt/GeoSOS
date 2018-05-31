using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotSpatial.Symbology;
using DotSpatial.Data;
using System.Diagnostics;
using ICSharpCode.Core.WinForms;

namespace GIS.Common.Dialogs
{
    public partial class TableEditorControl1 : UserControl
    {

        #region Fields

        private IFeatureLayer _featureLayer;
        private bool _ignoreSelectionChanged;
        private bool _ignoreTableSelectionChanged;
        private string _fidField;
        private AttributeCache _attributeCache;
        private List<int> _selectedRows;

        // 工具条
        private ToolStrip _toolStrip;

        #endregion

        #region methods

        // 根据feature layer的选中状态，更新data grid view的选中状态
        private void SetSelectionFromLayer()
        {
            if (_featureLayer == null)
            {
                return;
            }
            if (_ignoreSelectionChanged)
            {
                return;
            }

            dataGridView1.SuspendLayout();

            _ignoreSelectionChanged = true;
            _ignoreTableSelectionChanged = true;

            try
            {
                if (!_featureLayer.EditMode)
                {
                    // 得到要素的绘制状态参数
                    FastDrawnState[] states = _featureLayer.DrawnStates;
                    if (states == null)
                    {
                        return;
                    }

                    if (_featureLayer.DataSet.AttributesPopulated)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            int fid = (int)row.Cells[_fidField].Value;
                            row.Selected = states[fid].Selected;
                        }
                    }
                    else
                    {
                        foreach (AttributeCache.DataPage page in _attributeCache.Pages)
                        {
                            for (int row = page.LowestIndex; row <= page.HighestIndex; row++)
                            {
                                dataGridView1.Rows[row].Selected = states[row].Selected;
                            }
                        }
                    }
                }
                else
                {
                    IFeatureSelection fs = _featureLayer.Selection as IFeatureSelection;
                    if (fs == null)
                    {
                        return;
                    }
                    if (_featureLayer.DataSet.AttributesPopulated)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            int fid = (int)row.Cells[_fidField].Value;
                            row.Selected = fs.Filter.DrawnStates[_featureLayer.DataSet.Features[fid]].IsSelected;
                        }
                    }
                    else
                    {
                        foreach (AttributeCache.DataPage page in _attributeCache.Pages)
                        {
                            for (int row = page.LowestIndex; row <= page.HighestIndex; row++)
                            {
                                dataGridView1.Rows[row].Selected =
                                    fs.Filter.DrawnStates[_featureLayer.DataSet.Features[row]].IsSelected;
                            }
                        }
                    }
                }
            }
            finally
            {
                _ignoreSelectionChanged = false;
                _ignoreTableSelectionChanged = false;
                dataGridView1.ResumeLayout();
            }
        }

        /// <summary>
        /// 在数据表中增加一个字段，存储行号
        /// </summary>
        /// <param name="table"></param>
        private void AddFid(DataTable table)
        {
            const string name = "FID";
            // 避免字段重名
            int i = 0;
            while (table.Columns.Contains(name + i))
            {
                i++;
            }
            _fidField = name + i;

            table.Columns.Add(_fidField, typeof(int));
            for (var row = 0; row < table.Rows.Count; row++)
            {
                table.Rows[row][_fidField] = row;
            }
        }

        #endregion


        #region Properties

        /// <summary>
        /// Gets or sets the feature layer used by this data Table
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public IFeatureLayer FeatureLayer
        {
            get
            {
                return _featureLayer;
            }
            set
            {
                if (_featureLayer == value)
                {
                    return;
                }
                if (_featureLayer != null)
                {
                    _featureLayer.SelectionChanged -= SelectedFeaturesChanged;
                    if (_fidField != null)
                    {
                        _featureLayer.DataSet.DataTable.Columns.Remove(_fidField);
                        _fidField = null;
                    }
                }
                _featureLayer = value;

                dataGridView1.CellValueNeeded -= DataGridView1CellValueNeeded;
                dataGridView1.CellValuePushed -= dataGridView1_CellValuePushed;
                dataGridView1.RowValidated -= dataGridView1_RowValidated;

                if (_featureLayer != null)
                {
                    //to show the Filename label
                    //DisplayFilePathLabel(FeatureLayer.DataSet.Filename);

                    _featureLayer.ProgressHandler = null;

                    if (_featureLayer.DataSet.NumRows() < 10000 && !_featureLayer.DataSet.AttributesPopulated)
                    {
                        _featureLayer.DataSet.FillAttributes();
                    }

                    if (_featureLayer.DataSet.AttributesPopulated)
                    {
                        dataGridView1.VirtualMode = false;
                        AddFid(_featureLayer.DataSet.DataTable);
                        dataGridView1.DataSource = _featureLayer.DataSet.DataTable;
                    }
                    else
                    {
                        dataGridView1.VirtualMode = true;
                        dataGridView1.CellValueNeeded += DataGridView1CellValueNeeded;
                        dataGridView1.CellValuePushed += dataGridView1_CellValuePushed;
                        dataGridView1.RowValidated += dataGridView1_RowValidated;
                        _attributeCache = new AttributeCache(FeatureLayer.DataSet, 16);
                        foreach (var field in _featureLayer.DataSet.GetColumns())
                        {
                            dataGridView1.Columns.Add(field.ColumnName, field.ColumnName);
                        }
                        dataGridView1.RowCount = _featureLayer.DataSet.NumRows();
                    }
                    _featureLayer.SelectionChanged += SelectedFeaturesChanged;
                }
            }
        }
        #endregion

        #region EventHandlers

        /// <summary>
        /// when the selected features are changed on the feature layer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void SelectedFeaturesChanged(object sender, EventArgs e)
        {
            SetSelectionFromLayer();
        }


        private void DataGridView1CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            e.Value = _attributeCache.RetrieveElement(e.RowIndex, e.ColumnIndex);
        }


        private void dataGridView1_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            if (_attributeCache.EditRowIndex != dataGridView1.CurrentCell.RowIndex)
            {
                _attributeCache.EditRowIndex = dataGridView1.CurrentCell.RowIndex;
            }
            _attributeCache.EditRow[dataGridView1.Columns[e.ColumnIndex].Name] = e.Value;
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (_attributeCache.EditRow != null)
            {
                _attributeCache.SaveChanges();
            }
            _attributeCache.EditRowIndex = -1; // this also sets the EditRow to null;
        }
        /// <summary>
        /// handle data grid view selection change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView1SelectionChanged(object sender, EventArgs e)
        {
            if (!_featureLayer.DataSet.AttributesPopulated)
            {
                return; // For now can handle only populated data sets with fid column
            }
            Debug.Assert(_fidField != null);
            if (_ignoreTableSelectionChanged)
            {
                return;
            }
            _ignoreSelectionChanged = true;

            try
            {
                //manage selection using the Selection property
                IndexSelection sel = _featureLayer.Selection as IndexSelection;
                if (sel != null)
                {
                    sel.SuspendChanges();
                    //set the selected state of the corresponding feature
                    foreach (DataGridViewRow row in dataGridView1.Rows)
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
                    List<int> adds = new List<int>();
                    List<int> removes = _selectedRows.ToList();
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        if (!_selectedRows.Contains(row.Index))
                        {
                            adds.Add(row.Index);
                        }
                        removes.Remove(row.Index);
                    }
                    _featureLayer.Select(adds);
                    _featureLayer.UnSelect(removes);
                }
            }
            finally
            {
                _ignoreSelectionChanged = false;
            }
        }


        #endregion


        #region Constructors

        public TableEditorControl1()
            : this(null)
        {

        }

        public TableEditorControl1(IFeatureLayer layer)
        {
            InitializeComponent();

            // 工具条插件接口
            _toolStrip = ToolbarService.CreateToolStrip(this, "/FrameWork/Attributes/ToolStrip");
            Controls.Add(_toolStrip);

            FeatureLayer = layer;
            _selectedRows = new List<int>();

            Disposed += delegate
            {
                FeatureLayer = null;
            };

            // 从图层向数据表更新
            Load += delegate
            {
                SetSelectionFromLayer();
                dataGridView1.SelectionChanged += DataGridView1SelectionChanged;
            };


        }


        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotSpatial.Controls;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// 字段计算器界面
    /// </summary>
    public partial class FieldCaculator : Form
    {
        #region Private Variable

        DataGridView _dgvAttributeTable;
        List<double> _caculateResult = new List<double>();
        IMap _map;
        DataTable _dataTable;
        bool _boolVar1, _boolVar2;



        #endregion

        #region Construct

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FieldCaculator()
        {
            InitializeComponent();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="map1">地图对象</param>
        /// <param name="dataTable">数据表对象</param>
        /// <param name="dgv">数据网格对象</param>
        public FieldCaculator(IMap map1, DataTable dataTable, DataGridView dgv)
        {
            InitializeComponent();

            _map = map1;
            _dataTable = dataTable;
            _dgvAttributeTable = dgv;

            // 默认选中加号
            caculateMode.SelectedIndex = 0;



            if (_dataTable != null)
            {
                for (int i = 0; i < _dataTable.Columns.Count; i++)
                {
                    // 字段计算器只显示可以计算的字段
                    try
                    {
                        Convert.ToDouble(_dataTable.Rows[0][i]);
                        fieldList.Items.Add(_dataTable.Columns[i].ColumnName);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            else
            {
                MessageBox.Show("dataTable is null!");
            }
        }


        #endregion

        #region Event

        /// <summary>
        /// 计算新属性值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Caculate(object sender, EventArgs e)
        {
            //check caculate items is var list or const
            for (int j = 0; j < fieldList.Items.Count; j++)
            {
                if (fieldList.Items[j].ToString() == var1Text.Text)
                {
                    _boolVar1 = true;
                }
                if (fieldList.Items[j].ToString() == var2Text.Text)
                {
                    _boolVar2 = true;
                }
            }
            if (_boolVar1 == true && _boolVar2 == true)
            {
                for (int i = 0; i <= _dgvAttributeTable.RowCount - 1; i++)
                {
                    switch (caculateMode.SelectedIndex)
                    {
                        case 0:
                            _caculateResult.Add(Convert.ToDouble(_dgvAttributeTable.Rows[i].Cells[var1Text.Text].Value) + Convert.ToDouble(_dgvAttributeTable.Rows[i].Cells[var2Text.Text].Value));
                            break;
                        case 1:
                            _caculateResult.Add(Convert.ToDouble(_dgvAttributeTable.Rows[i].Cells[var1Text.Text].Value) - Convert.ToDouble(_dgvAttributeTable.Rows[i].Cells[var2Text.Text].Value));
                            break;
                        case 2:
                            _caculateResult.Add(Convert.ToDouble(_dgvAttributeTable.Rows[i].Cells[var1Text.Text].Value) * Convert.ToDouble(_dgvAttributeTable.Rows[i].Cells[var2Text.Text].Value));
                            break;
                        case 3:
                            _caculateResult.Add(Convert.ToDouble(_dgvAttributeTable.Rows[i].Cells[var1Text.Text].Value) / Convert.ToDouble(_dgvAttributeTable.Rows[i].Cells[var2Text.Text].Value));
                            break;
                    }
                }
            }
            else if (_boolVar1 == true && _boolVar2 == false)
            {
                for (int i = 0; i <= _dgvAttributeTable.RowCount - 1; i++)
                {
                    switch (caculateMode.SelectedIndex)
                    {
                        case 0:
                            _caculateResult.Add(Convert.ToDouble(_dgvAttributeTable.Rows[i].Cells[var1Text.Text].Value) + Convert.ToDouble(var2Text.Text));
                            break;
                        case 1:
                            _caculateResult.Add(Convert.ToDouble(_dgvAttributeTable.Rows[i].Cells[var1Text.Text].Value) - Convert.ToDouble(var2Text.Text));
                            break;
                        case 2:
                            _caculateResult.Add(Convert.ToDouble(_dgvAttributeTable.Rows[i].Cells[var1Text.Text].Value) * Convert.ToDouble(var2Text.Text));
                            break;
                        case 3:
                            _caculateResult.Add(Convert.ToDouble(_dgvAttributeTable.Rows[i].Cells[var1Text.Text].Value) / Convert.ToDouble(var2Text.Text));
                            break;
                    }
                }
            }
            else if (_boolVar1 == false && _boolVar2 == true)
            {
                for (int i = 0; i <= _dgvAttributeTable.RowCount - 1; i++)
                {
                    switch (caculateMode.SelectedIndex)
                    {
                        case 0:
                            _caculateResult.Add(Convert.ToDouble(var1Text.Text) + Convert.ToDouble(_dgvAttributeTable.Rows[i].Cells[var2Text.Text].Value));
                            break;
                        case 1:
                            _caculateResult.Add(Convert.ToDouble(var1Text.Text) - Convert.ToDouble(_dgvAttributeTable.Rows[i].Cells[var2Text.Text].Value));
                            break;
                        case 2:
                            _caculateResult.Add(Convert.ToDouble(var1Text.Text) * Convert.ToDouble(_dgvAttributeTable.Rows[i].Cells[var2Text.Text].Value));
                            break;
                        case 3:
                            _caculateResult.Add(Convert.ToDouble(var1Text.Text) / Convert.ToDouble(_dgvAttributeTable.Rows[i].Cells[var2Text.Text].Value));
                            break;
                    }
                }
            }
            else
            {
                for (int i = 0; i <= _dgvAttributeTable.RowCount - 1; i++)
                {
                    switch (caculateMode.SelectedIndex)
                    {
                        case 0:
                            _caculateResult.Add(Convert.ToDouble(var1Text.Text) + Convert.ToDouble(var2Text.Text));
                            break;
                        case 1:
                            _caculateResult.Add(Convert.ToDouble(var1Text.Text) - Convert.ToDouble(var2Text.Text));
                            break;
                        case 2:
                            _caculateResult.Add(Convert.ToDouble(var1Text.Text) * Convert.ToDouble(var2Text.Text));
                            break;
                        case 3:
                            _caculateResult.Add(Convert.ToDouble(var1Text.Text) / Convert.ToDouble(var2Text.Text));
                            break;
                    }
                }
            }


            AddCaculateResult(newColumnText.Text, _caculateResult);

            this.Close();

        }

        private void SelectField(object sender, EventArgs e)
        {
            //MessageBox.Show("ok");

            if (var1Text.Text == "")
            {
                var1Text.Text += fieldList.SelectedItem;
            }
            else if (var2Text.Text == "")
            {
                var2Text.Text += fieldList.SelectedItem;
            }
            else
            {
                var1Text.Clear();
                var2Text.Clear();
                var1Text.Text += fieldList.SelectedItem;
            }
        }

        private void ChangeCaculateMode(object sender, EventArgs e)
        {
            symbol.Text = caculateMode.SelectedItem.ToString();

        }

        #endregion

        #region Method

        private void AddCaculateResult(string _newColumnName, List<double> result)
        {
            //Declare a datatable
            //check the layers
            if (_map.Layers.Count > 0)
            {
                //Add new column
                DataColumn column = new DataColumn(_newColumnName);

                _dataTable.Columns.Add(column);



                for (int i = 0; i < _dataTable.Rows.Count; i++)
                {
                    _dgvAttributeTable.Rows[i].Cells[(_dgvAttributeTable.Columns.Count - 1)].Value = result[i];
                }

            }
            else
            {
                MessageBox.Show("Please add a layer to the map.");
            }
        }

        #endregion

        

    }
}

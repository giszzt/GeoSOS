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
using DotSpatial.Data;
using ICSharpCode.Core;

namespace GIS.Common.Dialogs
{
    public partial class SQLBulider : Form
    {
        #region Const String

        string _getUnique = StringParser.Parse("${res:GIS.Common.SQLBulider.getUnique}");
        string _quire = StringParser.Parse("${res:GIS.Common.SQLBulider.quire}");
        string _clear = StringParser.Parse("${res:GIS.Common.SQLBulider.clear}");
        string _SQLBuilding = StringParser.Parse("${res:GIS.Common.SQLBulider.Form}");

        #endregion

        #region Private Variable

        DataGridView _dgvAttributeTable;
        IMap _map;
        FeatureLayer _attiveLayer;

        #endregion

        #region Construct

        public SQLBulider(DataTable dt,DataGridView dgv,FeatureLayer layer)
        {
            InitializeComponent();

            _dgvAttributeTable = dgv;
            _attiveLayer = layer;
            
            _dgvAttributeTable.DataSource = dt;

            for (int i = 0; i < _dgvAttributeTable.ColumnCount; i++)
            {
                fieldList.Items.Add(_dgvAttributeTable.Columns[i].Name);
            }

            label2.Text = _attiveLayer.DataSet.Name;
            
            label3.Location = new Point(label3.Location.X - 41 + label2.Width, label3.Location.Y);

            //translate the name of btn into Chinese
            btnGetUnique.Text = _getUnique;
            btnQuire.Text = _quire;
            btnClearExpression.Text = _clear;
            this.Text = _SQLBuilding;
        }

        #endregion

        #region Event

        private void SelectField(object sender, EventArgs e)
        {
            //input the field_list item to select_expression
            select_expression.Text += fieldList.SelectedItem;
        }

        private void SelectUniqueValue(object sender, EventArgs e)
        {
            //input the unique_list item to select_expression
            int num1;long num2;float num3;double num4;
            if(
                float.TryParse(uniqueList.SelectedItem.ToString() ,out num3) == false||
                long.TryParse(uniqueList.SelectedItem.ToString(), out num2) == false||
                int.TryParse(uniqueList.SelectedItem.ToString(), out num1) == false||
                double.TryParse(uniqueList.SelectedItem.ToString(), out num4) == false
                )
            {
                select_expression.Text += "'" + uniqueList.SelectedItem + "'";
            }
            else
            {
                select_expression.Text += uniqueList.SelectedItem;

            }
        }

        private void GetUnique(object sender, EventArgs e)
        {
            uniqueList.Items.Clear();

            int Row_Count = _dgvAttributeTable.RowCount;

            List<string> uniqe_item = new List<string>();

            //if the field list is empty
            if (fieldList.SelectedItem != null)
            {
                //judge seleted column item if unique
                uniqe_item.Add(_dgvAttributeTable.Rows[0].Cells[fieldList.SelectedItem.ToString()].Value.ToString());
                for (int i = 1; i <= (Row_Count - 2); i++)
                {
                    if (uniqe_item.Contains(_dgvAttributeTable.Rows[i].Cells[fieldList.SelectedItem.ToString()].Value.ToString()) == false)
                    {
                        uniqe_item.Add(_dgvAttributeTable.Rows[i].Cells[fieldList.SelectedItem.ToString()].Value.ToString());
                    }

                }

                for (int k = 0; k <= (uniqe_item.Count - 1); k++)
                    uniqueList.Items.Add(uniqe_item[k]);
            }
            else
            {
                MessageBox.Show("please select an field");
               
            }

        }

        private void ClearExpression(object sender, EventArgs e)
        {
            select_expression.Clear();

            uniqueList.Items.Clear();

            _attiveLayer.ClearSelection();
        }

        private void Quire(object sender, EventArgs e)
        {
            try
            {
                _attiveLayer.SelectByAttribute(select_expression.Text);

                _attiveLayer.ZoomToSelectedFeatures();

                MapRelateToAttributes();
            } catch (Exception)
            {
                MessageBox.Show("Please input correct expression.");
            }
            

            
        }

        #region Caculate Mode

        private void btnEqualClick(object sender, EventArgs e)
        {
            select_expression.Text += btnEqual.Text;
        }

        private void btnGreatClick(object sender, EventArgs e)
        {
            select_expression.Text += btnGreater.Text;
        }

        private void btnLessClick(object sender, EventArgs e)
        {
            select_expression.Text += btnLess.Text;
        }

        private void btnUnderLineClick(object sender, EventArgs e)
        {
            select_expression.Text += btnUnderLine.Text;
        }

        private void btnmoClick(object sender, EventArgs e)
        {
            select_expression.Text += btnMod.Text;
        }

        private void btnisClick(object sender, EventArgs e)
        {
            select_expression.Text += "is";
        }

        private void btnunequalClick(object sender, EventArgs e)
        {
            select_expression.Text += btnunequal.Text;
        }

        private void btngeClick(object sender, EventArgs e)
        {
            select_expression.Text += btnge.Text;
        }

        private void btnleClick(object sender, EventArgs e)
        {
            select_expression.Text += btnle.Text;
        }

        private void btnkhClick(object sender, EventArgs e)
        {
            select_expression.Text = "(" + select_expression.Text + ")";
        }

        private void btnKClick(object sender, EventArgs e)
        {
            select_expression.Text += "like";
        }

        private void btnNClick(object sender, EventArgs e)
        {
            select_expression.Text += "and";
        }

        private void btnorClick(object sender, EventArgs e)
        {
            select_expression.Text += "or";
        }

        private void btnnotClick(object sender, EventArgs e)
        {
            select_expression.Text += "not";
        }

        #endregion

        #endregion

        #region Method

        private void MapRelateToAttributes()
        {
            //connect map & datatable 

            _dgvAttributeTable.Refresh();

            //FeatureLayer selected_layer = _map.Layers[0] as FeatureLayer;

            ISelection selected_features = _attiveLayer.Selection;

            FeatureSet selected_featureSet = selected_features.ToFeatureSet();

            _dgvAttributeTable.DataSource = selected_featureSet.DataTable;
        }

        #endregion

    }
}

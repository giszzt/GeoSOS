using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DotSpatial.Data;
using DotSpatial.Symbology;
using ICSharpCode.Core;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// Control to create, edit and validate label expressions.
    /// </summary>
    public partial class ExpressionControl : UserControl
    {
        private DataTable _table;
        private IAttributeSource _attributeSource;
        private Expression exp;
        
        /// <summary>
        /// Control to edit and validate expressions that can be used to label features.
        /// </summary>
        public ExpressionControl()
        {
            InitializeComponent();
            exp = new Expression();
        }

        #region Properties
        /// <summary>
        /// Gets/Sets whether empty expressions are valid.
        /// </summary>
        public Boolean AllowEmptyExpression { get; set; }

        /// <summary>
        /// Setting this is an alternative to specifying the table.  This allows the
        /// query control to build a query using pages of data instead of the whole
        /// table at once.
        /// </summary>
        public IAttributeSource AttributeSource
        {
            get { return _attributeSource; }
            set
            {
                _attributeSource = value;
                UpdateFields();
            }
        }

        /// <summary>
        /// Gets or sets the data Table for this control. Setting this will
        /// automatically update the fields shown in the list.
        /// </summary>
        public DataTable Table
        {
            get { return _table; }
            set
            {
                _table = value;
                UpdateFields();
            }
        }

        /// <summary>
        /// Expression that gets edited and validated in ExpressionControl.
        /// </summary>
        public string ExpressionText
        {
            get
            {
                return rtbExpression.Text.Trim();
            }
            set
            {
                rtbExpression.Text = value;
            }
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Adds a new line.
        /// </summary>
        private void btNewLine_Click(object sender, EventArgs e)
        {
            rtbExpression.SelectedText = "\n";
        }

        /// <summary>
        /// Validates the expression and shows the error or result.
        /// </summary>
        private void btValidate_Click(object sender, EventArgs e)
        {
            ValidateExpression();
        }

        /// <summary>
        /// Adds the field that was double clicked to the expression.
        /// </summary>
        private void dgvFields_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex > dgvFields.Rows.Count - 1) return;
            rtbExpression.SelectedText = "[" + dgvFields.Rows[e.RowIndex].Cells[dgvcName.Name].Value.ToString() + "]";
        }

        /// <summary>
        /// Suppress tab if it was entered because it is illigal in expressions.
        /// </summary>
        private void rtbExpression_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        #endregion

        /// <summary>
        /// Validates the Expression including syntax and operations.
        /// </summary>
        /// <returns>True, if Expression is valid.</returns>
        public bool ValidateExpression()
        {
            if (string.IsNullOrWhiteSpace(rtbExpression.Text))
            {
                if (AllowEmptyExpression)
                {
                    lblResult.Text = "";
                    return true;
                }
                else
                {
                    lblResult.Text = StringParser.Parse("${res:GIS.Common.Dialogs.SymbologyFormsMessageStrings.ExpressionControl_EmptyExpression}");
                    lblResult.ForeColor = Color.Red;
                    return false;
                }
            }
            var res = exp.ParseExpression(rtbExpression.Text);
            if (!res)
            {
                lblResult.Text = exp.ErrorMessage;
                lblResult.ForeColor = Color.Red;
                return false;
            }

            string retVal = "";
            if (exp.IsValidOperation(ref retVal, _table.Rows.Count > 0 ? _table.Rows[0] : null))// calculate with real values if possible else use temporary values
            {
                lblResult.Text = retVal;
                lblResult.ForeColor = Color.Black;
                return true;
            }
            else
            {
                lblResult.Text = exp.ErrorMessage;
                lblResult.ForeColor = Color.Red;
                return false;
            }
        }

        /// <summary>
        /// Replaces the expression fields by the columns of the current _attributeSource or _table.
        /// </summary>
        private void UpdateFields()
        {
            bool hasFID = false;
            exp.ClearFields();
            dgvFields.SuspendLayout();
            dgvFields.Rows.Clear();
            if (_attributeSource != null)
            {
                DataColumn[] columns = _attributeSource.GetColumns();
                foreach (DataColumn dc in columns)
                {
                    exp.AddField(dc);
                    dgvFields.Rows.Add(dc.ColumnName, dc.DataType.ToString().Replace("System.", ""));
                    if (dc.ColumnName.ToLower() == "fid")
                        hasFID = true;
                }
            }
            else if (_table != null)
            {
                foreach (DataColumn dc in _table.Columns)
                {
                    exp.AddField(dc);
                    dgvFields.Rows.Add(dc.ColumnName, dc.DataType.ToString().Replace("System.", ""));
                    if (dc.ColumnName.ToLower() == "fid")
                        hasFID = true;
                }
            }
            if (!hasFID)
                dgvFields.Rows.Add("FID", typeof(int).ToString().Replace("System.", ""));
            dgvFields.ResumeLayout();
        }
    }
}

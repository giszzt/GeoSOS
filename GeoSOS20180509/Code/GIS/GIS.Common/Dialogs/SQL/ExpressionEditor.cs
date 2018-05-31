using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DotSpatial.Symbology;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// ExpressionEditor
    /// </summary>
    public class ExpressionEditor : UITypeEditor
    {
        #region Private Variables

        ITypeDescriptorContext _context;

        #endregion

        #region Methods

        /// <summary>
        /// This describes how to launch the form etc.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="provider"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            _context = context;

            IWindowsFormsEditorService dialogProvider = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            SQLExpressionDialog dlgExpression = new SQLExpressionDialog();
            string original = (string)value;
            dlgExpression.Expression = (string)value;

            // Try to find the Table
            IFeatureCategory category = context.Instance as IFeatureCategory;
            if (category != null)
            {
                IFeatureScheme scheme = category.GetParentItem() as IFeatureScheme;
                if (scheme != null)
                {
                    IFeatureLayer layer = scheme.GetParentItem() as IFeatureLayer;
                    if (layer != null)
                    {
                        dlgExpression.Table = layer.DataSet.DataTable;
                    }
                }
                else
                {
                    IFeatureLayer layer = category.GetParentItem() as IFeatureLayer;
                    if (layer != null)
                    {
                        dlgExpression.Table = layer.DataSet.DataTable;
                    }
                }
            }

            dlgExpression.ChangesApplied += DlgExpressionChangesApplied;
            var result = dialogProvider.ShowDialog(dlgExpression);
            dlgExpression.ChangesApplied -= DlgExpressionChangesApplied;
            return result != DialogResult.OK ? original : dlgExpression.Expression;
        }

        private void DlgExpressionChangesApplied(object sender, EventArgs e)
        {
            SQLExpressionDialog dlg = sender as SQLExpressionDialog;
            if (dlg != null)
            {
                string exp = dlg.Expression;
                _context.PropertyDescriptor.SetValue(_context.Instance, exp);
            }
        }

        /// <summary>
        /// This tells the editor that it should open a dialog form when editing the value from a ... button
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        #endregion

        #region Properties

        #endregion
    }
}
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DotSpatial.Serialization;
using DotSpatial.Symbology;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// LineSymbolizerEditor
    /// </summary>
    public class LineSymbolizerEditor : UITypeEditor
    {
        #region Private Variables

        private ILineSymbolizer _copy;
        private ILineSymbolizer _original;

        #endregion

        #region Methods

        /// <summary>
        /// Launches a form for editing the line symbolizer
        /// </summary>
        /// <param name="context"></param>
        /// <param name="provider"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            _original = value as ILineSymbolizer;
            if (_original == null) return value;
            _copy = _original.Copy();
            IWindowsFormsEditorService dialogProvider = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            DetailedLineSymbolDialog dialog = new DetailedLineSymbolDialog(_copy);
            dialog.ChangesApplied += DialogChangesApplied;
            if (dialogProvider.ShowDialog(dialog) != DialogResult.OK) return _original;
            _original.CopyProperties(_copy);
            return value;
        }

        private void DialogChangesApplied(object sender, EventArgs e)
        {
            _original.CopyProperties(_copy);
        }

        /// <summary>
        /// Indicates to launch a form, rather than using a drop-down edit style
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
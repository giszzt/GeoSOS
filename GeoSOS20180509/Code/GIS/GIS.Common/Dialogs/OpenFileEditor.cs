using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// UIOpenFileEditor
    /// </summary>
    public class OpenFileEditor : UITypeEditor
    {
        /// <summary>
        /// This should launch an open file dialog instead of the usual thing.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="provider"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            // change this once a DataProvider has been sorted out
            //ofd.Filter = "Binary Grids (*.bgd)";
            if (ofd.ShowDialog() != DialogResult.OK) return null;
            return ofd.FileName;
        }

        /// <summary>
        /// Either allows the editor to work or else nips it in the butt
        /// </summary>
        /// <param name="context">ITypeDescriptorContext</param>
        /// <returns>UITypeEditorEditStyle</returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
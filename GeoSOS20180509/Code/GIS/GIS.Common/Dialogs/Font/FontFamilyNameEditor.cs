using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// FontFamilyNameEditor
    /// </summary>
    public class FontFamilyNameEditor : UITypeEditor
    {
        private IWindowsFormsEditorService _dialogProvider;

        #region Methods

        /// <summary>
        /// Edits a value based on some user input which is collected from a character control.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="provider"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            _dialogProvider = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            ListBox cmb = new ListBox();
            FontFamily[] fams = FontFamily.Families;
            cmb.SuspendLayout();
            foreach (FontFamily fam in fams)
            {
                cmb.Items.Add(fam.Name);
            }
            cmb.SelectedValueChanged += CmbSelectedValueChanged;
            cmb.ResumeLayout();
            if (_dialogProvider != null) _dialogProvider.DropDownControl(cmb);
            string test = (string)cmb.SelectedItem;
            return test;
        }

        private void CmbSelectedValueChanged(object sender, EventArgs e)
        {
            _dialogProvider.CloseDropDown();
        }

        /// <summary>
        /// Gets the UITypeEditorEditStyle, which in this case is drop down.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Overrides the ISDropDownResizable to allow this control to be adjusted.
        /// </summary>
        public override bool IsDropDownResizable
        {
            get
            {
                return true;
            }
        }

        #endregion
    }
}
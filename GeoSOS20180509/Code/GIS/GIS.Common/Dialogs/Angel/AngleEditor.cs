using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// CharacterCodeEditor
    /// </summary>
    public class AngleEditor : UITypeEditor
    {
        IWindowsFormsEditorService _dialogProvider;

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
            AngleControl ac = new AngleControl { Angle = Convert.ToInt32(value) };
            ac.AngleChosen += AcAngleChosen;
            if (_dialogProvider != null) _dialogProvider.DropDownControl(ac);
            return (double)ac.Angle;
        }

        private void AcAngleChosen(object sender, EventArgs e)
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
        /// Ensures that we can widen the drop-down without having to close the drop down,
        /// widen the control, and re-open it again.
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
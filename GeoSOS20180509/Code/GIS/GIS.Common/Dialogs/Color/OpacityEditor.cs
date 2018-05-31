using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// OpacityEditor
    /// </summary>
    public class OpacityEditor : UITypeEditor
    {
        #region Private Variables

        IWindowsFormsEditorService _dialogProvider;

        #endregion

        #region Constructors

        #endregion

        #region Methods

        /// <summary>
        /// Edits the value by showing a slider control in the drop down.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="provider"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            _dialogProvider = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            RampSlider rs = new RampSlider
                                {
                                    Maximum = 1,
                                    Minimum = 0,
                                    MaximumColor = Color.SteelBlue,
                                    MinimumColor = Color.Transparent,
                                    RampText = "Opacity",
                                    RampTextBehindRamp = true,
                                    Value = Convert.ToDouble(value),
                                    ShowValue = false,
                                    Width = 75,
                                    Height = 50
                                };
            rs.ValueChanged += RsValueChanged;
            if (_dialogProvider != null) _dialogProvider.DropDownControl(rs);
            return (float)rs.Value;
        }

        private void RsValueChanged(object sender, EventArgs e)
        {
            _dialogProvider.CloseDropDown();
        }

        /// <summary>
        /// Sets the behavior to drop-down
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

        #region Protected Methods

        #endregion
    }
}
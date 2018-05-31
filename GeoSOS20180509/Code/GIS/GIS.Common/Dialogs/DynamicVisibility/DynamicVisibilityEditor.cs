using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using DotSpatial.Symbology;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// DynamicVisibilityEditor
    /// </summary>
    public class DynamicVisibilityEditor : UITypeEditor
    {
        #region Private Variables

        private ILayer _layer;

        #endregion

        #region Methods

        /// <summary>
        /// Display a drop down when editing instead of the normal control, and allow the user to "grab" a
        /// new dynamic visibility extent.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="provider"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            _layer = context.Instance as ILayer;
            IWindowsFormsEditorService dialogProvider = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            DynamicVisibilityControl dvc = new DynamicVisibilityControl(dialogProvider, _layer);
            if (dialogProvider != null) dialogProvider.DropDownControl(dvc);
            if (_layer != null) _layer.Invalidate();
            return dvc.UseDynamicVisibility;
        }

        /// <summary>
        /// Indicate that we should use a drop-down for controlling dynamic visibility.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        #endregion

        #region Properties

        #endregion
    }
}
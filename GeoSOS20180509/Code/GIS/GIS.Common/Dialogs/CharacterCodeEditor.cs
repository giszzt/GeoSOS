using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using DotSpatial.Symbology;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// CharacterCodeEditor
    /// </summary>
    public class CharacterCodeEditor : UITypeEditor
    {
        #region Constructors

        #endregion

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
            ICharacterSymbol symbol = context.Instance as ICharacterSymbol;
            IWindowsFormsEditorService dialogProvider = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            CharacterControl cc = new CharacterControl(dialogProvider, symbol);
            if (dialogProvider != null) dialogProvider.DropDownControl(cc);
            return cc.SelectedChar;
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
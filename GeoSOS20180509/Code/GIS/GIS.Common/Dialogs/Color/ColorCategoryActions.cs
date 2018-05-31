using DotSpatial.Symbology;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// Default implementation of IColorCategoryActions
    /// </summary>
    public class ColorCategoryActions : LegendItemActionsBase, IColorCategoryActions
    {
        /// <summary>
        /// Show the color category editor form.
        /// </summary>
        /// <param name="e"></param>
        public void ShowEdit(IColorCategory e)
        {
            using (var frm = new ColorPicker(e))
            {
                ShowDialog(frm);
            }
        }
    }
}
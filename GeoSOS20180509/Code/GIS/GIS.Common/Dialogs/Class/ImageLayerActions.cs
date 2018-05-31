using System.Windows.Forms;
using DotSpatial.Data;
using DotSpatial.Symbology;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// Actions that occur on an image layer in the legend.
    /// </summary>
    public class ImageLayerActions : LegendItemActionsBase, IImageLayerActions
    {
        /// <summary>
        /// Show the properties of an image layer in the legend. 
        /// </summary>
        /// <param name="e"></param>
        public void ShowProperties(IImageLayer e)
        {
            using (var dlg = new LayerDialog(e,new ImageCategoryControl()))
            {
                ShowDialog(dlg);
            }
        }

        /// <summary>
        /// Export data from an image layer.
        /// </summary>
        /// <param name="e"></param>
        public void ExportData(IImageData e)
        {
            using (var sfd = new SaveFileDialog
                {
                    Filter = DataManager.DefaultDataManager.RasterWriteFilter
                })
            {
                if (ShowDialog(sfd) == DialogResult.OK)
                {
                    e.SaveAs(sfd.FileName);
                }
            }
        }
    }
}
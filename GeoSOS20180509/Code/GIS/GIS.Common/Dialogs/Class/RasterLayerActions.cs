using System.Windows.Forms;
using DotSpatial.Data;
using DotSpatial.Symbology;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// Legend actions on a raster layer.
    /// </summary>
    public class RasterLayerActions: LegendItemActionsBase, IRasterLayerActions
    {
        /// <summary>
        /// Shows the properties of the current raster legend item.
        /// </summary>
        /// <param name="e"></param>
        public void ShowProperties(IRasterLayer e)
        {
            using (var dlg = new LayerDialog(e, new RasterCategoryControl()))
            {
                ShowDialog(dlg);
            }
        }

        /// <summary>
        /// Export data from a raster layer.
        /// </summary>
        /// <param name="e"></param>
        public void ExportData(IRaster e)
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
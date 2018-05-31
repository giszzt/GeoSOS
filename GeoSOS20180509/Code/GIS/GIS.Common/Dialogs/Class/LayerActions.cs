using System.Windows.Forms;
using DotSpatial.Symbology;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// Actions that occur on a layer legend item.
    /// </summary>
    public class LayerActions : LegendItemActionsBase, ILayerActions
    {
        /// <summary>
        /// Determines whether a layer has dynamic visibility and hence is only shown at certain scales.
        /// </summary>
        /// <param name="e"></param>
        public void DynamicVisibility(IDynamicVisibility e, IFrame MapFrame)
        {
            using (var dvg = new DynamicVisibilityModeDialog())
            {
                var result = ShowDialog(dvg);

                if (result == DialogResult.OK)
                {
                    e.DynamicVisibilityMode = dvg.DynamicVisibilityMode;
                    e.UseDynamicVisibility = true;
                    e.DynamicVisibilityWidth = MapFrame.ViewExtents.Width;
                }

                if (result == DialogResult.No)
                {
                    e.UseDynamicVisibility = false;
                }
            }
        }
    }
}
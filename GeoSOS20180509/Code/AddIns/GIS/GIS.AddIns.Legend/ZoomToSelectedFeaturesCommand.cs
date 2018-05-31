using System;
using ICSharpCode.Core;
using DotSpatial.Symbology;
using GIS.Common.Dialogs;
using GeoAPI.Geometries;
using DotSpatial.Data;

namespace GIS.AddIns.Legend
{
    public class ZoomToSelectedFeaturesCommand : AbstractCommand
    {
        public override void Run()
        {
            ILegendItem selectedItem = (GIS.FrameWork.Application.App.Legend as GIS.Common.Dialogs.Legend).SelectedLegendMenuItem;
            if (selectedItem != null)
            {
                ZoomToSelected(selectedItem,2, 2);
            }
        }

        /// <summary>
        /// Zooms to the envelope of the selected features, adding a border of the size specified.
        /// </summary>
        public void ZoomToSelected(ILegendItem _selectedItem, double distanceX, double distanceY)
        {
            ISelection selection;
            if (_selectedItem != null)
            {
                if (!(_selectedItem is FeatureLayer))
                {
                    return;
                }
                selection = (_selectedItem as FeatureLayer).Selection;
                if (selection.Count == 0)
                    return;

                Envelope env = selection.Envelope;
                _selectedItem.ParentMapFrame().ViewExtents = env.ToExtent();
            }
        }
    }
}

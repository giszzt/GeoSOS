using System.Collections.Generic;
using ICSharpCode.Core;
using DotSpatial.Controls;
using DotSpatial.Symbology;
using GIS.GDAL;
using GIS.Common;

namespace GIS.AddIns.Legend
{
    class SelectByROICommand : AbstractCommand
    {
        private IMap _map = GIS.FrameWork.Application.App.Map;
        public override void Run()
        {
            ILegendItem selectedItem = (GIS.FrameWork.Application.App.Legend as GIS.Common.Dialogs.Legend).SelectedLegendMenuItem;
            if (selectedItem != null && selectedItem is IFeatureLayer)
            {
                SelectByPolygons(selectedItem as IFeatureLayer);
            }               
        }

        public void SelectByPolygons(IFeatureLayer selectedItem)
        {
            IFeatureLayer baseLayer = selectedItem;
            IFeatureLayer selectLayer = null;
            foreach (ILayer layer in _map.MapFrame.DrawingLayers)
            {
                if (layer.LegendText == "Polygon")
                {
                    selectLayer = layer as IFeatureLayer;
                    break;
                }
            }

            List<int> resultIndices = null;
            if (selectLayer != null)
            {
                resultIndices = FeatureSetQuery.SpatialFilter(baseLayer.DataSet, selectLayer.DataSet);
                //resultIndices = OgrQuery.OgrSpatialFilter(baseLayer.DataSet, selectLayer.DataSet);
            }
            if (resultIndices != null)
            {
                baseLayer.UnSelectAll();
                baseLayer.Select(resultIndices);
                _map.Refresh();
            }
        }
    }
}

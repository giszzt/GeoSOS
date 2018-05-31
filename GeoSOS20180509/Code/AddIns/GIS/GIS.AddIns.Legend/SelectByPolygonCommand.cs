using System.Collections.Generic;
using ICSharpCode.Core;
using DotSpatial.Controls;
using DotSpatial.Symbology;
using GIS.Common.MapFunctions;
using GeoAPI.Geometries;
using GIS.GDAL;
using GIS.Common;

namespace GIS.AddIns.Legend
{
    class SelectByPolygonCommand : AbstractCommand
    {
        private IMap _map = GIS.FrameWork.Application.App.Map;
        public override void Run()
        {
            DrawPolygonFunction drawPolygon = new DrawPolygonFunction(_map);
            drawPolygon.ConfigureParameters(false, false, new DrawPolygonFunction.OperatedDelegate(SelectByPolygon));
            _map.ActivateMapFunction(drawPolygon);
            _map.Refresh();
        }

        //作为委托传入到DrawPolygon操作中
        public void SelectByPolygon(IFeatureLayer drawedLayer)
        {
            ILegendItem selectedItem = (GIS.FrameWork.Application.App.Legend as GIS.Common.Dialogs.Legend).SelectedLegendMenuItem;
            if (selectedItem != null && selectedItem is IFeatureLayer)
            {
                IFeatureLayer baseLayer = selectedItem as IFeatureLayer;
                IFeatureLayer selectLayer = drawedLayer;
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
}

using System;
using ICSharpCode.Core;
using DotSpatial.Symbology;
using GIS.Common.Dialogs;

namespace GIS.AddIns.Legend
{
    public class ExportDataCommand : AbstractCommand
    {
        public override void Run()
        {
            ILegendItem selectedItem = (GIS.FrameWork.Application.App.Legend as GIS.Common.Dialogs.Legend).SelectedLegendMenuItem;
            if (selectedItem != null)
            {
                if (selectedItem is FeatureLayer)
                {
                    FeatureLayerActions fla = new FeatureLayerActions();
                    if (fla != null)
                    {
                        fla.ExportData(selectedItem as IFeatureLayer);
                    }
                }
                else if(selectedItem is RasterLayer)
                {
                    RasterLayerActions rla = new RasterLayerActions();
                    if (rla != null)
                    {
                        rla.ExportData((selectedItem as RasterLayer).DataSet);
                    }
                }
                else if(selectedItem is ImageLayer)
                {
                    ImageLayerActions ila = new ImageLayerActions();
                    if (ila != null)
                    {
                        ila.ExportData((selectedItem as ImageLayer).DataSet);
                    }
                }
            }
        }
    }
}

﻿using System;
using ICSharpCode.Core;
using DotSpatial.Symbology;
using GIS.Common.Dialogs;

namespace GIS.AddIns.Legend
{
    public class PropertiesCommand : AbstractCommand
    {
        public override void Run()
        {
            ILegendItem selectedItem = (GIS.FrameWork.Application.App.Legend as GIS.Common.Dialogs.Legend).SelectedLegendMenuItem;
            if (selectedItem != null)
            {
                if (selectedItem != null)
                {
                    if (selectedItem is FeatureLayer)
                    {
                        FeatureLayerActions fla = new FeatureLayerActions();
                        if (fla != null && selectedItem != null)
                        {
                            fla.ShowProperties(selectedItem as IFeatureLayer);
                        }
                    }
                    else if (selectedItem is RasterLayer)
                    {
                        RasterLayerActions rla = new RasterLayerActions();
                        if (rla != null && selectedItem != null)
                        {
                            rla.ShowProperties(selectedItem as IRasterLayer);
                        }
                    }
                    else if (selectedItem is ImageLayer)
                    {
                        ImageLayerActions ila = new ImageLayerActions();
                        if (ila != null && selectedItem != null)
                        {
                            ila.ShowProperties(selectedItem as IImageLayer);
                        }
                    }
                }
            }
        }
    }
}

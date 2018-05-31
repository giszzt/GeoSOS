using System;
using System.Linq;
using ICSharpCode.Core;
using DotSpatial.Symbology;
using GIS.Common.Dialogs;
using DotSpatial.Controls;
using DotSpatial.Data;
using System.Collections.Generic;

namespace GIS.AddIns.Legend
{
    public class ZoomToMapFrameCammand : AbstractCommand
    {
        public override void Run()
        {
            ILegendItem selectedItem = (GIS.FrameWork.Application.App.Legend as GIS.Common.Dialogs.Legend).SelectedLegendMenuItem;
            if (selectedItem != null && selectedItem is MapFrame)
            {
                //get max extent from maplayercollection
                IMapLayerCollection _layers = GIS.FrameWork.Application.App.Map.Layers;
                Extent ext = null;
                IList<ILayer> layers = _layers.Cast<ILayer>().ToList();
                if (layers != null)
                {
                    foreach (ILayer layer in layers)
                    {
                        if (layer.Extent != null && !layer.Extent.IsEmpty()) // changed by jany (2015-07-17) don't add extents of empty layers, because they cause a wrong overall extent 
                        {
                            if (ext == null)
                                ext = (Extent)layer.Extent.Clone();
                            else
                                ext.ExpandToInclude(layer.Extent);
                        }
                    }
                }
                //calculate expands
                double eps = 1e-7;
                var maxExt = ext.Width < eps || ext.Height < eps
                    ? new Extent(ext.MinX - eps, ext.MinY - eps, ext.MaxX + eps, ext.MaxY + eps)
                    : ext;
                maxExt.ExpandBy(maxExt.Width / 10, maxExt.Height / 10);

                GIS.FrameWork.Application.App.Map.ViewExtents = maxExt;

            }
        }
    }
}

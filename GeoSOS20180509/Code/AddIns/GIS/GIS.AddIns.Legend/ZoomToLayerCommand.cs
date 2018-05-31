using System;
using ICSharpCode.Core;
using DotSpatial.Symbology;
using GIS.Common.Dialogs;

namespace GIS.AddIns.Legend
{
    public class ZoomToLayerCommand : AbstractCommand
    {
        public override void Run()
        {
            ILegendItem selectedItem = (GIS.FrameWork.Application.App.Legend as GIS.Common.Dialogs.Legend).SelectedLegendMenuItem;
            if (selectedItem != null)
            {
                selectedItem.ParentMapFrame().ViewExtents = (selectedItem as Layer).Extent;
            }
        }
    }
}

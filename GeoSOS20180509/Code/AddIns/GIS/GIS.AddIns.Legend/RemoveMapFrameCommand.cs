using System;
using ICSharpCode.Core;
using DotSpatial.Symbology;
using GIS.Common.Dialogs;
using DotSpatial.Controls;

namespace GIS.AddIns.Legend
{
    public class RemoveMapFrameCommand : AbstractCommand
    {
        public override void Run()
        {
            ILegendItem selectedItem = (GIS.FrameWork.Application.App.Legend as GIS.Common.Dialogs.Legend).SelectedLegendMenuItem;
            int count = GIS.FrameWork.Application.App.Legend.RootNodes.Count;
            if (selectedItem != null && selectedItem is MapFrame && count > 1)
            {
                GIS.FrameWork.Application.App.Legend.RemoveMapFrame(selectedItem as IFrame, false);
            }
        }
    }
}

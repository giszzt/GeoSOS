using System;
using ICSharpCode.Core;
using DotSpatial.Symbology;
using GIS.Common.Dialogs;
using FrameWork.Gui;
using GIS.FrameWork;

namespace GIS.AddIns.Legend
{
    public class RemoveLayerCommand : AbstractCommand
    {
        public override void Run()
        {
            ILegendItem selectedItem = (GIS.FrameWork.Application.App.Legend as GIS.Common.Dialogs.Legend).SelectedLegendMenuItem;
            GIS.Common.Dialogs.Attributes attPad = WorkbenchSingleton.Workbench.GetPad(typeof(AttributesPad)).PadContent.Control as GIS.Common.Dialogs.Attributes;
            if (selectedItem != null)
            {
                attPad.RemovePage(selectedItem);
                GIS.FrameWork.Application.App.Map.MapFrame.Remove(selectedItem as ILayer);
            }
        }
    }
}

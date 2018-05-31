using System;
using ICSharpCode.Core;
using DotSpatial.Symbology;
using GIS.Common.Dialogs;

namespace GIS.AddIns.Legend
{
    public class SetDynamicVisibilityScaleCommand : AbstractCommand
    {
        public override void Run()
        {
            ILegendItem selectedItem = (GIS.FrameWork.Application.App.Legend as GIS.Common.Dialogs.Legend).SelectedLegendMenuItem;
            if (selectedItem != null)
            {
                LayerActions la = new LayerActions();
                IFrame mapFrame = selectedItem.ParentMapFrame();
                if (la != null && selectedItem != null)
                {
                    la.DynamicVisibility(selectedItem as IDynamicVisibility, mapFrame);
                }
            }
        }
    }
}

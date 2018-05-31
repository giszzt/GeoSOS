using System;
using ICSharpCode.Core;
using DotSpatial.Symbology;
using GIS.Common.Dialogs;

namespace GIS.AddIns.Legend
{
    public class SetLabelVisibilityScaleCommand : AbstractCommand
    {
        public override void Run()
        {

            ILegendItem selectedItem = (GIS.FrameWork.Application.App.Legend as GIS.Common.Dialogs.Legend).SelectedLegendMenuItem;
            if (selectedItem != null)
            {
                LabelSetupCommand labelSetup = new LabelSetupCommand();
                ILabelLayer _labelLayer = labelSetup._LabelLayer;

                if (_labelLayer == null)
                    return;

                FeatureLayerActions fla = new FeatureLayerActions();
                if (fla != null)
                {
                    _labelLayer.DynamicVisibilityWidth = _labelLayer.FeatureLayer.MapFrame.ViewExtents.Width;
                    fla.LabelExtents(_labelLayer);
                }
            }
        }
    }
}

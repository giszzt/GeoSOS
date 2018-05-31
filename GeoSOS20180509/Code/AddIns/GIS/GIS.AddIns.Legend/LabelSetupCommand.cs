using System;
using ICSharpCode.Core;
using DotSpatial.Symbology;
using GIS.Common.Dialogs;
using DotSpatial.Controls;

namespace GIS.AddIns.Legend
{
    public class LabelSetupCommand : AbstractCommand
    {
        private static ILabelLayer _labelLayer;

        public ILabelLayer _LabelLayer
        {
            get
            {
                if (_labelLayer == null)
                {
                    return null;
                }
                else
                    return _labelLayer;
            }
            set { _labelLayer = value; }
        }

        public override void Run()
        {
            ILegendItem selectedItem = (GIS.FrameWork.Application.App.Legend as GIS.Common.Dialogs.Legend).SelectedLegendMenuItem;
            if (selectedItem != null)
            {
                if (_labelLayer == null)
                {
                    _labelLayer = new MapLabelLayer(selectedItem as IFeatureLayer);
                    (selectedItem as FeatureLayer).LabelLayer = _labelLayer;
                    (selectedItem as FeatureLayer).ShowLabels = true;
                }

                _labelLayer.CreateLabels();

                FeatureLayerActions fla = new FeatureLayerActions();
                if (fla != null)
                {
                    fla.LabelSetup(_labelLayer);
                }
            }
        }
    }
}

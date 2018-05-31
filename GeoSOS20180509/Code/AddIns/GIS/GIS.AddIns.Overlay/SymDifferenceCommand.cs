using GIS.GDAL.Overlay;
using ICSharpCode.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GIS.AddIns.Overlay
{
    public class SymDifferenceCommand : AbstractCommand
    {
        public override void Run()
        {
            OverlayType type = OverlayType.SymDifference;
            OverlayDialog frmSymDifference = new OverlayDialog(type);
            frmSymDifference.Text = StringParser.Parse("${res:Ribbon.SpatialAnalysis.Overlay.SymDifference}");
            if (frmSymDifference.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string baseLayerPath = frmSymDifference.baseLayerPath;
                string overlayLayerPath = frmSymDifference.overlayLayerPath;
                string resultLayerPath = frmSymDifference.resultLayerPath;

                OverlayOperate.ProcessOverlay(baseLayerPath, overlayLayerPath, resultLayerPath, type);
            }
        }
    }
}

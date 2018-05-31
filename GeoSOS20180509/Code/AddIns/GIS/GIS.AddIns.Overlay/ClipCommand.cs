using GIS.GDAL.Overlay;
using ICSharpCode.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GIS.AddIns.Overlay
{
    public class ClipCommand : AbstractCommand
    {
        public override void Run()
        {
            OverlayType type = OverlayType.Clip;
            OverlayDialog frmClip = new OverlayDialog(type);
            frmClip.Text = StringParser.Parse("${res:Ribbon.SpatialAnalysis.Overlay.Clip}");
            if (frmClip.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string baseLayerPath = frmClip.baseLayerPath;
                string overlayLayerPath = frmClip.overlayLayerPath;
                string resultLayerPath = frmClip.resultLayerPath;

                OverlayOperate.ProcessOverlay(baseLayerPath, overlayLayerPath, resultLayerPath, type);
            }
        }
    }
}

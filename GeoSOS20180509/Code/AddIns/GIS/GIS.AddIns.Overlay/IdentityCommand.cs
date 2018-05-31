using GIS.GDAL.Overlay;
using ICSharpCode.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GIS.AddIns.Overlay
{
    public class IdentityCommand : AbstractCommand
    {
        public override void Run()
        {
            OverlayType type = OverlayType.Identity;
            OverlayDialog frmIdentity = new OverlayDialog(type);
            frmIdentity.Text = StringParser.Parse("${res:Ribbon.SpatialAnalysis.Overlay.Identity}");
            if (frmIdentity.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string baseLayerPath = frmIdentity.baseLayerPath;
                string overlayLayerPath = frmIdentity.overlayLayerPath;
                string resultLayerPath = frmIdentity.resultLayerPath;

                OverlayOperate.ProcessOverlay(baseLayerPath, overlayLayerPath, resultLayerPath, type);
            }
        }
    }
}

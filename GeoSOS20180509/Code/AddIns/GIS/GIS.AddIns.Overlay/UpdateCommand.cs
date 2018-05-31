using GIS.GDAL.Overlay;
using ICSharpCode.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GIS.AddIns.Overlay
{
    public class UpdateCommand : AbstractCommand
    {
        public override void Run()
        {
            OverlayType type = OverlayType.Update;
            OverlayDialog frmUpdate = new OverlayDialog(type);
            frmUpdate.Text = StringParser.Parse("${res:Ribbon.SpatialAnalysis.Overlay.Update}");
            if (frmUpdate.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string baseLayerPath = frmUpdate.baseLayerPath;
                string overlayLayerPath = frmUpdate.overlayLayerPath;
                string resultLayerPath = frmUpdate.resultLayerPath;

                OverlayOperate.ProcessOverlay(baseLayerPath, overlayLayerPath, resultLayerPath, type);
            }
        }
    }
}

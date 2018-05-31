using GIS.GDAL.Overlay;
using ICSharpCode.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GIS.AddIns.Overlay
{
    public class EraseCommand : AbstractCommand
    {
        public override void Run()
        {
            OverlayType type = OverlayType.Erase;
            OverlayDialog frmErase = new OverlayDialog(type);
            frmErase.Text = StringParser.Parse("${res:Ribbon.SpatialAnalysis.Overlay.Erase}");
            if (frmErase.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string baseLayerPath = frmErase.baseLayerPath;
                string overlayLayerPath = frmErase.overlayLayerPath;
                string resultLayerPath = frmErase.resultLayerPath;

                OverlayOperate.ProcessOverlay(baseLayerPath, overlayLayerPath, resultLayerPath, type);
            }
        }
    }
}

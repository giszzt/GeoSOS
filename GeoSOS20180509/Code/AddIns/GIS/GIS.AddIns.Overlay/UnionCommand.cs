using GIS.GDAL.Overlay;
using ICSharpCode.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GIS.AddIns.Overlay
{
    public class UnionCommand : AbstractCommand
    {
        public override void Run()
        {
            OverlayType type = OverlayType.Union;
            OverlayDialog frmUnion = new OverlayDialog(type);
            frmUnion.Text = StringParser.Parse("${res:Ribbon.SpatialAnalysis.Overlay.Union}");
            if (frmUnion.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string baseLayerPath = frmUnion.baseLayerPath;
                string overlayLayerPath = frmUnion.overlayLayerPath;
                string resultLayerPath = frmUnion.resultLayerPath;

                OverlayOperate.ProcessOverlay(baseLayerPath, overlayLayerPath, resultLayerPath, type);
            }
        }
    }
}

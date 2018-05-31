using DotSpatial.Controls;
using FrameWork.Gui;
using GIS.GDAL.Overlay;
using ICSharpCode.Core;
using OSGeo.OGR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GIS.AddIns.Overlay
{
    public class IntersectionCommand : AbstractCommand
    {

        public override void Run()
        {
            OverlayType type = OverlayType.Intersects;
            OverlayDialog frmIntersection = new OverlayDialog(type);
            frmIntersection.Text = StringParser.Parse("${res:Ribbon.SpatialAnalysis.Overlay.Intersection}");
            if (frmIntersection.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string baseLayerPath = frmIntersection.baseLayerPath;
                string overlayLayerPath = frmIntersection.overlayLayerPath;
                string resultLayerPath = frmIntersection.resultLayerPath;

                OverlayOperate.ProcessOverlay(baseLayerPath, overlayLayerPath, resultLayerPath, type);
            }
        }
    }
}

using System;
using ICSharpCode.Core;
using GIS.FrameWork;
using DotSpatial.Controls;

namespace GIS.AddIns.Common
{
    public class FullExtentCommand: AbstractCommand
    {
        public override void Run()
        {
            AppManager app = GIS.FrameWork.Application.App;
            IMap map = app.Map;
            map.ZoomToMaxExtent();
        }
    }
}

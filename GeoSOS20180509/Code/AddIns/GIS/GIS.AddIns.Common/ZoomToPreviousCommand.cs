using System;
using ICSharpCode.Core;
using GIS.FrameWork;
using DotSpatial.Controls;

namespace GIS.AddIns.Common
{
    public class ZoomToPreviousCommand:AbstractCommand
    {
        public override void Run()
        {
            AppManager app = GIS.FrameWork.Application.App;
            IMap map = app.Map;
            map.ZoomToPrevious();
        }
    }
}

using ICSharpCode.Core;
using DotSpatial.Controls;
using FrameWork.Gui;
using GIS.FrameWork;
using GIS.Common.MapFunctions;

namespace GIS.AddIns.Common
{
    /// <summary>
    /// Description of test.
    /// </summary>
    public class ROIPointCommand : AbstractCommand
    {
        public override void Run()
        {
            IMap _map = GIS.FrameWork.Application.App.Map;

            IMapFunction DrawPoint = new DrawPointFunction(_map);
            _map.ActivateMapFunction(DrawPoint);
            _map.Refresh();

        }
    }
}

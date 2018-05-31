using ICSharpCode.Core;
using DotSpatial.Controls;
using FrameWork.Gui;
using GIS.FrameWork;
using GIS.Common.MapFunctions;
using System.Windows.Forms;

namespace MultiPlan
{
    public class InfoCommand : AbstractCommand
    {
        public override void Run()
        {
            IMap map = GIS.FrameWork.Application.App.Map;
            IMapFunction infoFunction = new InfoFunction(map);
            map.ActivateMapFunction(infoFunction);
            map.Refresh();
        }
    }
}

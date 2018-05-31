/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2016/5/17
 * Time: 14:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
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
    public class ROILineCommand : AbstractCommand
    {
        public override void Run()
        {
            IMap _map = GIS.FrameWork.Application.App.Map;

            IMapFunction DrawLine = new DrawLineFunction(_map);
            _map.ActivateMapFunction(DrawLine);
            _map.Refresh();
        }
    }
}

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
    public class ROIPolygonCommand : AbstractCommand
    {
        public override void Run()
        {
            IMap map = GIS.FrameWork.Application.App.Map;
            DrawPolygonFunction drawPolygon = new DrawPolygonFunction(map);
            drawPolygon.ConfigureParameters(true, false, null);
            map.ActivateMapFunction(drawPolygon);
            map.Refresh();
        }
    }
}

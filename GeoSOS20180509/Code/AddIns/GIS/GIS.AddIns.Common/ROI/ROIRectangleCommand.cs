/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2016/5/17
 * Time: 14:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using ICSharpCode.Core;
using GIS.FrameWork;
using DotSpatial.Controls;
using DotSpatial.Data;
using GIS.Common.MapFunctions;

namespace GIS.AddIns.Common
{
    /// <summary>
    /// Description of test.
    /// </summary>
    public class ROIRectangleCommand : AbstractCommand
    {
        public override void Run()
        {
            IMap _map = GIS.FrameWork.Application.App.Map;
            IMapFunction DrawRectangle = new DrawRectangleFunction(_map);
            _map.ActivateMapFunction(DrawRectangle);
            _map.Refresh();
        }
    }
}
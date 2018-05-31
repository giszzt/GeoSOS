using System;
using ICSharpCode.Core;
using GIS.FrameWork;
using DotSpatial.Controls;
using System.Windows.Forms;

namespace GIS.AddIns.Common
{
    public class MeasureCommand:AbstractCommand
    {
        public override void Run()
        {
            AppManager App = GIS.FrameWork.Application.App;

            MapFunctionMeasure _Painter= new MapFunctionMeasure(App.Map);

            if (!App.Map.MapFunctions.Contains(_Painter))
                App.Map.MapFunctions.Add(_Painter);

            App.Map.FunctionMode = FunctionMode.None;
            App.Map.Cursor = Cursors.Cross;
            _Painter.Activate();
        }
    }
}

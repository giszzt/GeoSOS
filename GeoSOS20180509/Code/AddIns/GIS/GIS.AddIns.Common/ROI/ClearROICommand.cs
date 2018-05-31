using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using FrameWork.Gui;
using DotSpatial.Controls;
using DotSpatial.Symbology;
using GIS.Common.Dialogs;
using System.Windows.Forms;
using GIS.Common;

namespace GIS.AddIns.Common
{
    class ClearROICommand : AbstractCommand
    {         
        public override void Run()
        {
            IMap map = GIS.FrameWork.Application.App.Map;
            map.FunctionMode = FunctionMode.None;
            ROILayer roiLayer = new ROILayer(map);
            roiLayer.Clear();
            map.Refresh();
        }
    }
}

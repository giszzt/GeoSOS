using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using FrameWork.Gui;
using OxyPlot;
using OxyPlot.Reporting;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.WindowsForms;
using GIS.AddIns.Statistic.Dialogue;

namespace GIS.AddIns.Statistic
{
    class AttributeSetting:AbstractCommand
    {
        public override void Run()
        {
            PlotAttribute pa = new PlotAttribute();
            pa.Show();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.Core;
using FrameWork.Gui;
using OxyPlot;
using OxyPlot.Reporting;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.WindowsForms;
using System.Threading;
using GIS.AddIns.Statistic.Dialogue;

namespace GIS.AddIns.Statistic
{
    class ExportImageCommand : AbstractCommand 
    {
        public override void Run()
        {
            ExportImage ex = new ExportImage();
            ex.Show();
        }
    }
}

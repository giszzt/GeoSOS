using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.Core;
using FrameWork.Gui;
using OxyPlot;

namespace GIS.AddIns.Statistic
{
    public class CenterPlotCommand:AbstractCommand
    {
        public override void Run()
        {
            //SetPlotForm fp = new SetPlotForm();
            //fp.Show();
            IViewContent view = WorkbenchSingleton.Workbench.ActiveViewContent;
            (view.Control as MapStatistics)._XAxis.Reset();
            (view.Control as MapStatistics)._YAxis.Reset();
            (view.Control as MapStatistics)._CAxis.Reset();
            (view.Control as MapStatistics).plotView1.Refresh();
        }
    }
}

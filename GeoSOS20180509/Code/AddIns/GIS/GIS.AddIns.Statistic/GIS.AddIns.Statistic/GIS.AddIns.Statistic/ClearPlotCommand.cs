using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.Core;
using FrameWork.Gui;

namespace GIS.AddIns.Statistic
{
   public class ClearPlotCommand : AbstractCommand 
    {
       public override void Run()
       {
           IViewContent view = WorkbenchSingleton.Workbench.ActiveViewContent;
           (view.Control as MapStatistics)._pm.Series.Clear();
           (view.Control as MapStatistics)._pm.Axes.Clear();
           (view.Control as MapStatistics).plotView1.Refresh();
           (view.Control as MapStatistics).plotView1.Invalidate();
       }
    }
}

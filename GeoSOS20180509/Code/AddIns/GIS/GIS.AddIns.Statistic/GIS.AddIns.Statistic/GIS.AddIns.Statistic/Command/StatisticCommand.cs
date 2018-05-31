using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using FrameWork.Gui;

namespace GIS.AddIns.Statistic
{
    public class StatisticCommand : AbstractCommand
    {
        MapsStatisticsView MapStatistic = new MapsStatisticsView("MapStatistic");
        /// <summary>
        /// 
        /// </summary>
        public override void Run()
        {
            WorkbenchSingleton.Workbench.ShowView(MapStatistic);
        }
    }
}

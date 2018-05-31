using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameWork.Gui;

namespace GIS.AddIns.Statistic
{
    public class StatisticPlotPad : AbstractPadContent
    {
        StatisticControl panel;

        public StatisticPlotPad()
        {
            panel = new StatisticControl();
        }

        public override object Control
        {
            get
            {
                return panel;
            }

        }
    }
}

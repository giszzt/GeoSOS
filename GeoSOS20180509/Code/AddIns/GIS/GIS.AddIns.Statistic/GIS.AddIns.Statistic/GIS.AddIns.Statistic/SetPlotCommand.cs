using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.Core;
using FrameWork.Gui;

namespace GIS.AddIns.Statistic
{
    public class SetPlotCommand : AbstractCommand
    {
        public override void Run()
        {

            SetPlotForm fp = new SetPlotForm();
            fp.Show();
        }
    }
}

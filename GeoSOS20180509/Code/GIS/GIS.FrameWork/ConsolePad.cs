using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameWork.Gui;

namespace GIS.FrameWork
{
    public class ConsolePad : AbstractPadContent
    {
        GIS.Common.Dialogs.Console.Console panel;                

        public ConsolePad()
        {
            panel = new GIS.Common.Dialogs.Console.Console();
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

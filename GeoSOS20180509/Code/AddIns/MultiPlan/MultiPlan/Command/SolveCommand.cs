using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using FrameWork.Gui;

namespace MultiPlan
{
    public class SolveCommand : AbstractCommand
    {
        public override void Run()
        {
            Config.Initial();
            //ConflictConsultForm f = new ConflictConsultForm();
            //f.Show()
            ConflictConsultContent content = new ConflictConsultContent("要素级协调");
            WorkbenchSingleton.Workbench.ShowView(content);
            WorkbenchSingleton.StatusBar.SetCaretPosition(0, 0, 0);
        }
    }
}

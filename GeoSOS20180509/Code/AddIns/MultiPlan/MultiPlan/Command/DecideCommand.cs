using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using FrameWork.Gui;

namespace MultiPlan
{
    public class DecideCommand : AbstractCommand
    {
        public override void Run()
        {
            Config.Initial();
            ModelBuilderForm mbf = new ModelBuilderForm();
            mbf.Initial();
            mbf.ShowDialog();
            //selDataControl1.Initial(mbf.address, false);
        }
    }
}

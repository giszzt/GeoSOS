using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;

namespace MultiPlan
{
    public class PlanCommand : AbstractCommand
    {
        public override void Run()
        {
            Config.Initial();
            PlanForm f = new PlanForm();
            f.Show();
        }
    }
}

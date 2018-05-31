using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;

namespace MultiPlan
{
    public class RuleCommand : AbstractCommand
    {
        public override void Run()
        {
            Config.Initial();
            RuleForm f = new RuleForm();
            f.Show();
        }
    }
}

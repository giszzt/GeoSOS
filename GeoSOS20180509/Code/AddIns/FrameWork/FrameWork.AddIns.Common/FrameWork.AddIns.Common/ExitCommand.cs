using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;

namespace FrameWork.AddIns.Common
{
    public class ExitCommand : AbstractCommand
    {
        public override void Run()
        {
            System.Environment.Exit(0);
        }
    }
}

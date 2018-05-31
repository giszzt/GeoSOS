using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using GIS.AddIns.Ca.CaDialog;

namespace GIS.AddIns.Ca
{
    class LogisticPatchCACommand : AbstractCommand
    {
        public override void Run()
        {
            var map = GIS.FrameWork.Application.App.Map;
            LogisticPatchCaDialog form = new LogisticPatchCaDialog(map);             
            form.Show();
        }
    }
}

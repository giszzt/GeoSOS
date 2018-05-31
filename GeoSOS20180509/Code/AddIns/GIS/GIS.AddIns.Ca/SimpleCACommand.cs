using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using System.Windows.Forms;
using GIS.AddIns.Ca.CaDialog;

namespace GIS.AddIns.Ca
{
    class SimpleCACommand: AbstractCommand
    {
        public override void Run()
        {
            var map = GIS.FrameWork.Application.App.Map;
            SimpleCaDialog form = new SimpleCaDialog(map);
            form.Show();
        }
    }
}

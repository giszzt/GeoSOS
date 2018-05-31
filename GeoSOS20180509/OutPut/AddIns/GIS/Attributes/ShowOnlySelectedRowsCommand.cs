using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using FrameWork.Gui;
using GIS.FrameWork;

namespace GIS.AddIns.Attributes
{
    class ShowOnlySelectedRowsCommand : AbstractCommand
    {
        public override void Run()
        {
            GIS.Common.Dialogs.Attributes _attrPad = WorkbenchSingleton.Workbench.GetPad(typeof(AttributesPad)).PadContent.Control as GIS.Common.Dialogs.Attributes;

            _attrPad.ShowOnlySelectedRows();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using ICSharpCode.Core;
using FrameWork;
using FrameWork.Gui;

namespace FrameWork.AddIns.Common
{
    public class OptionsCommand : AbstractCommand
    {
        public static bool? ShowTreeOptions(string dialogTitle, AddInTreeNode node)
        {
            TreeViewOptionsDialog o = new TreeViewOptionsDialog(node.BuildChildItems<IOptionPanelDescriptor>(null));
            WindowInteropHelper helper = new WindowInteropHelper(o);
            o.Title = dialogTitle;
            o.Owner = WorkbenchSingleton.MainWindow;
            return o.ShowDialog();
        }

        public override void Run()
        {
            bool? result = ShowTreeOptions(
    "test",
    AddInTree.GetTreeNode("/FrameWork/OptionPanel/SystemOptions"));
            if (result == true)
            {
                // save properties after changing options
                PropertyService.Save();
            }
        }
    }
}

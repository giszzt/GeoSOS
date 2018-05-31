using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using DotSpatial.Controls;
using ICSharpCode.Core;
using FrameWork.Gui;
using GIS.FrameWork;
using GIS.Common.Dialogs;
using DotSpatial.Controls;
//using GIS.AddIns.Attributes;

namespace GIS.AddIns.Legend
{
    /// <summary>
    /// Description of test.
    /// </summary>
    public class OpenAttributeCommand : AbstractCommand
    {
        public override void Run()
        {

            GIS.Common.Dialogs.Attributes attPad = WorkbenchSingleton.Workbench.GetPad(typeof(AttributesPad)).PadContent.Control as GIS.Common.Dialogs.Attributes;
            GIS.Common.Dialogs.Legend legend = WorkbenchSingleton.Workbench.GetPad(typeof(LegendPad)).PadContent.Control as GIS.Common.Dialogs.Legend;

            if (legend.SelectedLegendMenuItem != null)
            {
                //if(!attPad.)
                //{
                    WorkbenchSingleton.Workbench.GetPad(typeof(AttributesPad)).BringPadToFront();

                //}
                //else
                //{
                    attPad.CreateNewPage(legend.SelectedLegendMenuItem.LegendText);
                //}

            }
            else
            {
                MessageBox.Show("please AddPage");
            }
        }
    }
}

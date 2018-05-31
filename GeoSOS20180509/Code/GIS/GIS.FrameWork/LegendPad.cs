using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using FrameWork.Gui;
using System.Windows.Forms;
using GIS.Common.Dialogs;

namespace GIS.FrameWork
{
    public class LegendPad : AbstractPadContent
    {
        Legend panel;
        bool activeContentChangedEnqueued;

        public LegendPad()
        {            
            panel = new Legend();
            Application.App.Legend = panel;
            Application.App.Map.Legend = panel;
            WorkbenchSingleton.Workbench.ActiveContentChanged += ActiveContentChanged;
            ActiveContentChanged(null, null);
        }

        public override object Control
        {
            get
            {
                return panel;
            }
        }

        void ActiveContentChanged(object sender, EventArgs e)
        {
            // this event may occur several times quickly after another (e.g. when opening or closing multiple files)
            // do the potentially expensive selection of the item in the tree view only once after the last change
            if (!activeContentChangedEnqueued)
            {
                activeContentChangedEnqueued = true;
                WorkbenchSingleton.SafeThreadAsyncCall(ActiveContentChangedInvoked);
            }
        }

        void ActiveContentChangedInvoked()
        {
            activeContentChangedEnqueued = false;
            if (WorkbenchSingleton.Workbench.ActiveContent == this)
            {

            }
            else
            {

            }
        }
    }
}

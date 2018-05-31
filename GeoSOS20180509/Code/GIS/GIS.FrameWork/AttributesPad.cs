using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.Core;
using FrameWork.Gui;
using DotSpatial.Controls;
using GIS.Common.Dialogs;

namespace GIS.FrameWork
{
    public class AttributesPad : AbstractPadContent
    {
        GIS.Common.Dialogs.Attributes panel;
        IMap map = Application.App.Map;
        GIS.Common.Dialogs.Legend legend = Application.App.Legend as GIS.Common.Dialogs.Legend;

        public AttributesPad()
        {
            panel = new Attributes(map, legend);
        }

        public override object Control
        {
            get
            {
                return panel;
            }
        }
    }
}

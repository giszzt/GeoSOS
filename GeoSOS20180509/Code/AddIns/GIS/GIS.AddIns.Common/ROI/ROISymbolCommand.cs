using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using FrameWork.Gui;
using DotSpatial.Controls;
using DotSpatial.Symbology;
using GIS.Common.Dialogs;
using System.Windows.Forms;

namespace GIS.AddIns.Common
{
    class ROISymbolCommand : AbstractCommand
    {         
        public override void Run()
        {
            IMap map = GIS.FrameWork.Application.App.Map;
            if (map.MapFrame.DrawingLayers.Count != 0)
            {
                DetailedSymbolDialog sDialog = new DetailedSymbolDialog();
                sDialog.ShowDialog();
            }
        }
    }
}

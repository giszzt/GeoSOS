using System;
using ICSharpCode.Core;
using DotSpatial.Symbology;
using GIS.Common.Dialogs;
using DotSpatial.Controls;
using System.Windows.Forms;

namespace GIS.AddIns.Legend
{
    public class ProjectionCommand:AbstractCommand
    {
        public override void Run()
        {
            ILegendItem selectedItem = (GIS.FrameWork.Application.App.Legend as GIS.Common.Dialogs.Legend).SelectedLegendMenuItem;
            if (selectedItem != null && selectedItem is MapFrame)
            {
                //Launches a MapFrameProjectionDialog
                using (var dialog = new MapFrameProjectionDialog(selectedItem as IMapFrame))
                {
                    dialog.ShowDialog(GIS.FrameWork.Application.App.Map as Control);
                }
            }
        }
    }
}

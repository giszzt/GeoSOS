using System.Collections.Generic;
using ICSharpCode.Core;
using FrameWork.Gui;
using GIS.FrameWork;
using DotSpatial.Controls;
using GIS.Common.Dialogs;
using DotSpatial.Symbology;
using System.Drawing;
using DotSpatial.Serialization;

namespace GIS.AddIns.Legend
{


    /// <summary>
    /// Description of test.
    /// </summary>
    public class SymbologyCommand : AbstractCommand
    {
        ILegendItem selectedItem = (GIS.FrameWork.Application.App.Legend as GIS.Common.Dialogs.Legend).SelectedLegendMenuItem;

        public override void Run()
        {
            if (selectedItem is IMapLineLayer)
            {
                DetailedLineSymbolDialog lsDialog = new DetailedLineSymbolDialog((selectedItem as IMapLineLayer).Symbolizer);
                lsDialog.ShowDialog();
            }else if(selectedItem is IMapPointLayer)
            {
                DetailedPointSymbolDialog psDialog = new DetailedPointSymbolDialog((selectedItem as IMapPointLayer).Symbolizer);
                psDialog.ShowDialog();
            }else if(selectedItem is IMapPolygonLayer)
            {
                DetailedPolygonSymbolDialog polygonDialog = new DetailedPolygonSymbolDialog((selectedItem as IMapPolygonLayer).Symbolizer);
                polygonDialog.ShowDialog();
            }

        }
    }
}

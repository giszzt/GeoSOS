using ICSharpCode.Core;
using DotSpatial.Symbology;

namespace GIS.AddIns.Legend
{
    class ClearSelectionCommand : AbstractCommand 
    {
        public override void Run()
        {
            ILegendItem selectedItem = (GIS.FrameWork.Application.App.Legend as GIS.Common.Dialogs.Legend).SelectedLegendMenuItem;
            if (selectedItem != null)
            {
                if (selectedItem is FeatureLayer)
                {
                    (selectedItem as IFeatureLayer).ClearSelection();
                }
            }
        }
    }
}

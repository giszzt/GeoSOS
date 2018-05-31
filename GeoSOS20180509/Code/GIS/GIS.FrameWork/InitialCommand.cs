using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using ICSharpCode.Core;
using DotSpatial.Controls;
using DotSpatial.Symbology;
using GeoAPI;
using GeoAPI.Geometries;
using FrameWork.Gui;

namespace GIS.FrameWork
{
    public class InitialCommand : AbstractCommand
    {
        MapView mainMapView = new MapView("MainMap");   

        public override void Run()
        {
            Application.App = new AppManager();
            Application.App.MapChanged+=new EventHandler<MapChangedEventArgs>(App_MapChanged);

            WorkbenchSingleton.Workbench.ShowView(mainMapView);

            WorkbenchSingleton.StatusBar.SetCaretPosition(0, 0, 0);

            Application.App.Map = mainMapView.Control as IMap;

            GIS.GDAL.GdalConfigure.Configure();
        }

        private void App_MapChanged(object sender, MapChangedEventArgs e)
        {
            (e.NewValue as Map).MouseMove += new MouseEventHandler(Map_MouseMove);
            e.NewValue.FunctionMode = FunctionMode.None;
            if (Application.App.Legend != null)
            {
                Application.App.Legend.RootNodes.Clear();
                e.NewValue.Legend = Application.App.Legend;
            }
        }

        private void Map_MouseMove(object sender, MouseEventArgs e)
        {
            Coordinate coordinate = (Application.App.Map as Map).PixelToProj(new Point(e.X, e.Y));
            WorkbenchSingleton.StatusBar.SetCaretPosition(coordinate.X, coordinate.Y, coordinate.Z);
        }

    }
}

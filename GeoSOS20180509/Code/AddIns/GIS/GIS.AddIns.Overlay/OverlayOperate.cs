using DotSpatial.Controls;
using FrameWork.Gui;
using GIS.GDAL.Overlay;
using OSGeo.OGR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GIS.AddIns.Overlay
{
    public class OverlayOperate
    {
        static IProgressMonitor _progressMonitor;
        static CancellationTokenSource _cancellationSource;

        public static void ProcessOverlay(string baseLayerPath, string overlayLayerPath, string resultLayerPath, OverlayType type)
        {
            try
            {
                _cancellationSource = new CancellationTokenSource();
                _progressMonitor = WorkbenchSingleton.StatusBar.CreateProgressMonitor(_cancellationSource.Token);

                Thread thread = new Thread(() => RunThread(baseLayerPath, overlayLayerPath, resultLayerPath, type));
                thread.Name = type.ToString();
                thread.Priority = ThreadPriority.BelowNormal;
                thread.IsBackground = true;
                thread.Start();
            }
            catch
            {

            }
        }

        private static void RunThread(string baseLayerPath, string overlayLayerPath, string resultLayerPath, OverlayType type)
        {
            //get data
            Layer baseLayer = GetLayer(baseLayerPath);
            Layer overlayLayer = GetLayer(overlayLayerPath);

            //create intersected data
            var driver = Ogr.GetDriverByName("ESRI Shapefile");
            if (driver == null) System.Environment.Exit(-1);
            string[] resultPath = resultLayerPath.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            string resultName = resultPath[resultPath.Length - 1];

            using (var dsResult = driver.CreateDataSource(resultLayerPath, new string[] { }))
            {
                if (dsResult == null) throw new Exception("Can't get to the datasoure.");

                Layer resultLayer = null;
                for (int i = 0; i < dsResult.GetLayerCount(); i++)
                {
                    resultLayer = dsResult.GetLayerByIndex(i);
                    if (resultLayer != null && resultLayer.GetLayerDefn().GetName() == resultName)
                    {
                        dsResult.DeleteLayer(i);
                        break;
                    }
                }
                resultLayer = dsResult.CreateLayer("ResultLayer", baseLayer.GetSpatialRef(), baseLayer.GetLayerDefn().GetGeomType(), new string[] { });

                if (resultLayer != null)
                {
                    bool isSuccess  = GIS.GDAL.Overlay.Overlay.OverlayOperate(baseLayer, overlayLayer, ref resultLayer, type, ProgressFunc);
                    if (!isSuccess)
                    {
                       ///Failed
                    }
                }
            }

            _cancellationSource.Cancel();
            _cancellationSource.Dispose();
            _progressMonitor.Dispose();

            //add result layer into map
            AppManager app = GIS.FrameWork.Application.App;
            app.Map.AddLayer(resultLayerPath);

            WorkbenchSingleton.StatusBar.SetMessage("Ready");
        }

        private static int ProgressFunc(double Complete, IntPtr Message, IntPtr Data)
        {
            _progressMonitor.Progress = Complete;
            WorkbenchSingleton.StatusBar.SetMessage("Processing ..." + Complete * 100 + "% Completed.");
            return 1;
        }
        
        /// <summary>
        /// Get layer from Path
        /// </summary>
        /// <param name="path">Path of Layer</param>
        /// <returns>Layer</returns>
        private static Layer GetLayer(string path)
        {
            Layer layer = null;
            if (System.IO.File.Exists(path))
            {
                DataSource dsBaseLayer = Ogr.Open(path, 0);
                layer = dsBaseLayer.GetLayerByIndex(0);
            }
            else
            {
                for (int i = 0; i < GIS.FrameWork.Application.App.Map.Layers.Count; i++)
                {
                    if (path == GIS.FrameWork.Application.App.Map.Layers[i].LegendText)
                    {
                        DotSpatial.Symbology.IFeatureLayer featureLayer = GIS.FrameWork.Application.App.Map.Layers[i] as DotSpatial.Symbology.IFeatureLayer;
                        layer = GIS.GDAL.VectorConverter.DS2OrgLayer(featureLayer.DataSet);
                        break;
                    }
                }
            }
            return layer;
        }
    }
}

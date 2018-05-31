using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using DotSpatial.Controls;
using DotSpatial.Symbology;
using GeoAPI.Geometries;

namespace GIS.Common
{
    /// <summary>
    /// ROI Layer
    /// </summary>
    public class ROILayer
    {
        private string _roiPath = System.Environment.CurrentDirectory + "\\Data\\cache\\ROI.shp";
        private MapPolygonLayer _roiLayer = null;
        private IMap _map = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ROILayer"/> class.
        /// </summary>
        /// <param name="map">Map</param>
        public ROILayer(IMap map)
        {
            _map = map;
            foreach (ILayer item in _map.MapFrame.DrawingLayers)
            {
                if (item.LegendText == "ROI")
                {
                    _roiLayer = item as MapPolygonLayer;
                    break;
                }
            }

            if (_roiLayer == null)
            {
                _roiLayer = new MapPolygonLayer();
                _roiLayer.LegendText = "ROI";
                _map.MapFrame.DrawingLayers.Add(_roiLayer);
            }

            SetSymbol();
        }

        /// <summary>
        /// Add Geometry
        /// </summary>
        /// <param name="geometry">Geometry</param>
        public void AddFeature(IGeometry geometry)
        {
            if (geometry.IsSimple)
            {
                if (NetTopologySuite.Algorithm.CGAlgorithms.IsCCW(geometry.Coordinates))
                {
                    geometry = geometry.Reverse();
                }
            }
            else
            {
                return;
            }

            _roiLayer.DataSet.AddFeature(geometry);
        }

        /// <summary>
        /// Add Geometry with buffer
        /// </summary>
        /// <param name="geometry">Geometry</param>
        /// <param name="distance">Distance</param>
        public void AddFeature(IGeometry geometry, double distance)
        {
            _roiLayer.DataSet.AddFeature(geometry.Buffer(distance));
        }

        /// <summary>
        /// Clear ROI
        /// </summary>
        public void Clear()
        {
            _map.MapFrame.DrawingLayers.Clear();
        }

        /// <summary>
        /// Set symbol of ROI
        /// </summary>
        public void SetSymbol()
        {
            _roiLayer.Symbolizer = new PolygonSymbolizer(Color.FromArgb(120, 44, 255, 144), Color.LightSkyBlue, 1);
        }

        /// <summary>
        /// Load ROI
        /// </summary>
        /// <param name="path">Path of ROI</param>
        public void Load(string path)
        {
            _roiLayer = DotSpatial.Controls.MapPolygonLayer.OpenFile(path) as MapPolygonLayer;
        }

        /// <summary>
        /// Export ROI 
        /// </summary>
        /// <param name="path">Path of ROI</param>
        public void Export(string path)
        {
            _roiLayer.DataSet.SaveAs(path, true);
            _roiLayer.DataSet.Close();
        }
    }
}

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
//using System.Collections.Generic;
using DotSpatial.Symbology;
using GeoAPI.Geometries;
using DotSpatial.Controls;
//using DotSpatial.Data;
using NetTopologySuite.Geometries;
using ICSharpCode.Core;
using GIS.Common.Dialogs;

namespace GIS.Common.MapFunctions
{
    public class DrawPointFunction : MapFunction
    {
        #region Private Variables

        private System.Drawing.Point _currentPoint;
        private bool _isEnabled;
        private System.Drawing.Point _points;
        private Coordinate _coordinatePoints;
        private NetTopologySuite.Geometries.Point _point;
        private IMap _map;

        #endregion

        #region Construct

        public DrawPointFunction(IMap inMap)
            : base(inMap)
        {
            _map = inMap;
            this.YieldStyle = YieldStyles.LeftButton | YieldStyles.Keyboard;
            _map.FunctionMode = FunctionMode.None;
            //_map.ClearSelection();
            //PointCanIn = true;
        }

        #endregion

        #region Properties

        //public bool PointCanIn
        //{
        //    get;
        //    set;
        //}

        #endregion

        #region Method

        protected IMapPointLayer GetPointLayer()
        {
            IMapPointLayer pointLayer = null;
            foreach (ILayer item in this._map.MapFrame.DrawingLayers)
            {
                if (item.LegendText == "Point")
                {
                    pointLayer = item as IMapPointLayer;
                    break;
                }
            }
            if (pointLayer == null)
            {
                pointLayer = new MapPointLayer();
                pointLayer.LegendText = "Point";
                pointLayer.Symbolizer = GIS.FrameWork.ROIConfigure.pointSymbolizer;
                this._map.MapFrame.DrawingLayers.Add(pointLayer);
            }
            return pointLayer;
        }

        #endregion

        #region Event

        protected override void OnMouseMove(GeoMouseArgs e)
        {
            if (_isEnabled)
            {
                _map.Invalidate();
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(GeoMouseArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isEnabled = false;
                _currentPoint = e.Location;
                int x = _currentPoint.X;
                int y = _currentPoint.Y;
                System.Drawing.Point point = new System.Drawing.Point(x, y);
                _points = point;
                _coordinatePoints = this._map.PixelToProj(new System.Drawing.Point(_points.X, _points.Y));

                IMapPointLayer pointLayer = GetPointLayer();
                _point = new NetTopologySuite.Geometries.Point(_coordinatePoints);
                pointLayer.DataSet.AddFeature(_point as IGeometry);

                _map.Refresh();
                _isEnabled = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                this.Enabled = false;
                string bufferType = "PointBuffer";
                Buffer pointBuffer = new Buffer(bufferType);
                pointBuffer.ShowDialog();
                _map.FunctionMode = FunctionMode.Pan;
            }

            base.OnMouseDown(e);
        }

        #endregion
    }
}
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;
using DotSpatial.Symbology;
using GeoAPI.Geometries;
using DotSpatial.Controls;
//using DotSpatial.Data;
using NetTopologySuite.Geometries;
using GIS.Common.Dialogs;

namespace GIS.Common.MapFunctions
{
    public class DrawLineFunction : MapFunction
    {
        #region Private Variables

        private System.Drawing.Point _currentPoint;
        private bool _isEnabled = true;
        private List<System.Drawing.Point> _points = new List<System.Drawing.Point>();
        private List<Coordinate> _coordinatePoints = new List<Coordinate>();
        private LineString _lineString;
        private IMap _map;

        #endregion

        #region Construct

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawLineFunction"/> class
        /// MapFunction to draw line
        /// </summary>
        /// <param name="inMap">Map</param>
        public DrawLineFunction(IMap inMap)
            : base(inMap)
        {
            _map = inMap;
            YieldStyle = YieldStyles.LeftButton | YieldStyles.Keyboard;
            _map.FunctionMode = FunctionMode.None;
            _points.Clear();
            //_map.ClearSelection();
            _lineString = null;
            _coordinatePoints.Clear();
            //LineCanIn = true;
        }

        #endregion

        #region properties

        //public FeatureSet LineFeatureSet
        //{ 
        //    get
        //    {
        //        return _featureSet;
        //    } 
        //}

        //public bool LineCanIn
        //{
        //    get;
        //    set;
        //}

        #endregion

        #region Method

        protected override void OnDraw(MapDrawArgs e)
        {
            if (_isEnabled)
            {
                GraphicsPath gp = new GraphicsPath();
                if (_points.Count >= 1)
                {
                    gp.AddLines(_points.ToArray());
                    gp.AddLine(_points[(_points.Count - 1)], _currentPoint);
                }

                Pen pen = new Pen(Color.CadetBlue);
                pen.Width = 2;
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                e.Graphics.DrawPath(pen, gp);
            }

            base.OnDraw(e);
        }

        protected IMapLineLayer GetLineLayer()
        {
            IMapLineLayer lineLayer = null;
            foreach (ILayer item in _map.MapFrame.DrawingLayers)
            {
                if (item.LegendText == "Line")
                {
                    lineLayer = item as IMapLineLayer;
                    break;
                }
            }
            if (lineLayer == null)
            {
                lineLayer = new MapLineLayer();
                lineLayer.LegendText = "Line";
                lineLayer.Symbolizer = GIS.FrameWork.ROIConfigure.lineSymbolizer;
                if (_map.MapFrame.DrawingLayers.Count > 0 && _map.MapFrame.DrawingLayers[_map.MapFrame.DrawingLayers.Count - 1].LegendText == "Point")
                {
                    _map.MapFrame.DrawingLayers.Insert(_map.MapFrame.DrawingLayers.Count - 1, lineLayer);
                }
                else
                {
                    _map.MapFrame.DrawingLayers.Add(lineLayer);
                }
            }
            return lineLayer;
        }

        #endregion

        #region Event

        protected override void OnMouseDown(GeoMouseArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _currentPoint = e.Location;
                if (_isEnabled == true)
                {
                    int x = _currentPoint.X;
                    int y = _currentPoint.Y;
                    System.Drawing.Point point = new System.Drawing.Point(x, y);
                    _points.Add(point);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                this.Enabled = false;
                string bufferType = "LineBuffer";
                Buffer lineBuffer = new Buffer(bufferType);
                lineBuffer.ShowDialog();
                _map.FunctionMode = FunctionMode.Pan;
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(GeoMouseArgs e)
        {
            _currentPoint = e.Location;

            if (_isEnabled == true /*&& _locker == true*/)
            {
                _map.Invalidate();
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseDoubleClick(GeoMouseArgs e)
        {
            _isEnabled = false;
            _coordinatePoints.Clear();
            for (int i = 0; i <= (_points.Count - 1); i++)
            {
                _coordinatePoints.Add(_map.PixelToProj(new System.Drawing.Point(_points[i].X, _points[i].Y)));
            }
            _lineString = new LineString(_coordinatePoints.ToArray());

            IMapLineLayer lineLayer = GetLineLayer();
            lineLayer.DataSet.AddFeature(_lineString as IGeometry);
                
            _points.Clear();
            _map.Refresh();
            _isEnabled = true;
        }

        #endregion
    }
}
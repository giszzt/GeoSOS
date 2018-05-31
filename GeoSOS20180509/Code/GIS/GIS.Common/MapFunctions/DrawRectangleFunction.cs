using System;
//using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DotSpatial.Symbology;
//using DotSpatial.Data;
using GeoAPI.Geometries;
//using SelectionMode = DotSpatial.Symbology.SelectionMode;
using DotSpatial.Controls;
using System.Collections.Generic;
using GIS.Common.Dialogs;
using NetTopologySuite.Geometries;
//using GdalAddInTest;


namespace GIS.Common.MapFunctions
{
    /// <summary>
    /// SelectTool
    /// </summary>
    public class DrawRectangleFunction : MapFunction
    {
        #region Private Variables

        private IMap _map;
        private readonly Pen _selectionPen;
        private bool _isEnabled = false;
        private System.Drawing.Point _currentPoint;
        private System.Drawing.Point _startPoint;
        private List<Coordinate> _coordinatePoints = new List<Coordinate>();
        private List<System.Drawing.Point> _points = new List<System.Drawing.Point>(2);

        #endregion

        #region Constructors

        public DrawRectangleFunction(IMap inMap)
            : base(inMap)
        {
            _selectionPen = new Pen(Color.Blue) { DashStyle = DashStyle.Solid  };
            YieldStyle = YieldStyles.LeftButton | YieldStyles.Keyboard;
            _map = inMap;
            //PolygonCanIn = true;
        }

        //public bool PolygonCanIn
        //{
        //    get;
        //    set;
        //}

        #endregion

        #region Properties

        //public IGeometry Geometry
        //{
        //    get
        //    {
        //        LinearRing linearRing = new LinearRing(_coordinatePoints.ToArray());
        //        Polygon polygon = new Polygon(linearRing);
        //        return polygon as IGeometry;
        //    }
        //}

        #endregion

        #region Method

        protected override void OnDraw(MapDrawArgs e)
        {
            if (_isEnabled)
            {
                Rectangle r = Opp.RectangleFromPoints(_startPoint, _currentPoint);
                r.Width -= 1;
                r.Height -= 1;

                HatchBrush brush = new HatchBrush(HatchStyle.Percent25, Color.Tomato,Color.SeaGreen);
                Pen pen = new Pen(brush, 2);
                pen.DashStyle = DashStyle.Dot;           
                e.Graphics.DrawRectangle(pen, r);
                e.Graphics.FillRectangle(brush, r);
            }
            base.OnDraw(e);
        }

        protected IMapPolygonLayer GetPolygonLayer()
        {
            IMapPolygonLayer polygonLayer = null;
            foreach (ILayer item in _map.MapFrame.DrawingLayers)
            {
                if (item.LegendText == "Polygon")
                {
                    polygonLayer = item as IMapPolygonLayer;
                    break;
                }
            }
            if (polygonLayer == null)
            {
                polygonLayer = new MapPolygonLayer();
                polygonLayer.LegendText = "Polygon";
                polygonLayer.Symbolizer = GIS.FrameWork.ROIConfigure.polygonSymbolizer;
                if (_map.MapFrame.DrawingLayers.Count > 0 && _map.MapFrame.DrawingLayers[0].LegendText == "ROI")
                {
                    _map.MapFrame.DrawingLayers.Insert(1, polygonLayer);
                }
                else
                {
                    _map.MapFrame.DrawingLayers.Insert(0, polygonLayer);
                }
            }
            return polygonLayer;
        }

        #endregion

        #region Event

        protected override void OnMouseDown(GeoMouseArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _startPoint = e.Location;
                _currentPoint = _startPoint;

                //_map.IsBusy = true;
                if (_points.Count == 1)
                {
                    _isEnabled = false;
                    _points.Add(e.Location);

                    ////write in coordinate points
                    _coordinatePoints.Add(_map.PixelToProj(new System.Drawing.Point(_points[0].X, _points[0].Y)));
                    _coordinatePoints.Add(_map.PixelToProj(new System.Drawing.Point(_points[0].X, _points[1].Y)));
                    _coordinatePoints.Add(_map.PixelToProj(new System.Drawing.Point(_points[1].X, _points[1].Y)));
                    _coordinatePoints.Add(_map.PixelToProj(new System.Drawing.Point(_points[1].X, _points[0].Y)));
                    _coordinatePoints.Add(_map.PixelToProj(new System.Drawing.Point(_points[0].X, _points[0].Y)));

                    LinearRing _linearRing = new LinearRing(_coordinatePoints.ToArray());
                    Polygon polygon = new Polygon(_linearRing);

                    IMapPolygonLayer polygonLayer = GetPolygonLayer();                 
                    polygonLayer.DataSet.AddFeature(polygon as IGeometry);
                             
                    _points.Clear();
                    _coordinatePoints.Clear();
                    _map.Refresh();
                }
                else if (_points.Count == 0)
                {
                    _isEnabled = true;
                    _points.Add(e.Location);
                }               
            }
            else if(e.Button == MouseButtons.Right)
            {
                this.Enabled = false;
                string bufferType = "PolygonBuffer";
                GIS.Common.Dialogs.Buffer polygonBuffer = new GIS.Common.Dialogs.Buffer(bufferType);
                polygonBuffer.ShowDialog();
                _map.FunctionMode = FunctionMode.Pan;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(GeoMouseArgs e)
        {
            int x = Math.Min(Math.Min(_startPoint.X, _currentPoint.X), e.X);
            int y = Math.Min(Math.Min(_startPoint.Y, _currentPoint.Y), e.Y);
            int mx = Math.Max(Math.Max(_startPoint.X, _currentPoint.X), e.X);
            int my = Math.Max(Math.Max(_startPoint.Y, _currentPoint.Y), e.Y);
            _currentPoint = e.Location;
            if (_isEnabled)
            {
                _map.Invalidate();
            }
            base.OnMouseMove(e);
        }

        #endregion
    }
}
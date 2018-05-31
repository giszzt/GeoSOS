using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;
using DotSpatial.Symbology;
using GeoAPI.Geometries;
using DotSpatial.Controls;
using DotSpatial.Data;
using NetTopologySuite.Geometries;
using GIS.Common.Dialogs;

namespace GIS.Common.MapFunctions
{
    /// <summary>
    /// SelectTool
    /// </summary>
    public class DrawPolygonFunction : MapFunction
    {
        #region Delegate

        //声明一个delegate类型
        public delegate void OperatedDelegate(IFeatureLayer layer);

        #endregion

        #region Private Variables

        private IMap _map;
        private System.Drawing.Point _currentPoint;
        private bool _isEnabled = true;
        private List<System.Drawing.Point> _points = new List<System.Drawing.Point>();
        private List<Coordinate> _coordinatePoints = new List<Coordinate>();
        //private IMapPolygonLayer _polygonLayer = new MapPolygonLayer();

        #endregion

        #region Construct

        public DrawPolygonFunction(IMap inMap)
            : base(inMap)
        {
            _map = inMap;
            YieldStyle = YieldStyles.LeftButton | YieldStyles.Keyboard;
            _map.FunctionMode = FunctionMode.None;
            OperatedFunc = null; 
            PolygonCanIn = false;
            WillClearPolygons = true;
            //_polygonLayer = GetPolygonLayer();
        }

        #endregion

        #region properties

        //public IGeometry Geometry
        //{
        //    get
        //    {
        //        LinearRing linearRing = new LinearRing(_coordinatePoints.ToArray());
        //        Polygon polygon = new Polygon(linearRing);
        //        return polygon as IGeometry;
        //    }
        //}

        public bool PolygonCanIn
        {
            get;
            set;
        }

        public bool WillClearPolygons
        {
            get;
            set;
        }

        public OperatedDelegate OperatedFunc
        {
            get;
            set;
        }

        #endregion


        #region Method

        //配置是否添加到已有图层，是否清除已有图层以及委托函数
        public void ConfigureParameters(bool PolygonCanIn, bool WillClearPolygons, OperatedDelegate OperatedFunc)
        {
            this.PolygonCanIn = PolygonCanIn;
            this.WillClearPolygons = WillClearPolygons;
            this.OperatedFunc = OperatedFunc;
        }

        protected override void OnDraw(MapDrawArgs e)
        {
            if (_isEnabled)
            {
                List<System.Drawing.Point> temPoints = _points;
                GraphicsPath gp = new GraphicsPath();
                if (_points.Count > 0)
                {
                    gp.AddLines(_points.ToArray());
                    gp.AddLine(_points[(_points.Count - 1)], _currentPoint);
                    gp.AddLine(_points[0], _currentPoint);
                }

                Pen pen = new Pen(Color.LawnGreen);
                pen.DashStyle = DashStyle.Dash;
                pen.Width = 2;

                e.Graphics.DrawPath(pen, gp);

                //SolidBrush brush = new SolidBrush(Color.FromArgb(50, Color.Gold));

                HatchBrush hatchBruch = new HatchBrush(HatchStyle.Percent90, Color.BlueViolet, Color.Pink);
                temPoints.Add(_currentPoint);
                e.Graphics.FillPolygon(hatchBruch, _points.ToArray(), FillMode.Winding);
                temPoints.RemoveAt((temPoints.Count - 1));
            }

            base.OnDraw(e);
        }

        //获取当前Polygon图层，以防DrawPolygon操作与其他图层操作不同步
        protected IMapPolygonLayer GetPolygonLayer()
        {
            IMapPolygonLayer polygonLayer = null;

            //如果已经存在Polygon图层，则获取
            foreach (ILayer item in _map.MapFrame.DrawingLayers)
            {
                if (item.LegendText == "Polygon")
                {
                    polygonLayer = item as IMapPolygonLayer;
                    break;
                }
            }

            //如果不存在Polygon图层，则新建并插入
            if (polygonLayer == null)
            {
                polygonLayer = new MapPolygonLayer();
                polygonLayer.LegendText = "Polygon";
                polygonLayer.Symbolizer = GIS.FrameWork.ROIConfigure.polygonSymbolizer;

                //保证Polygon图层插入在仅高于ROI图层的位置
                if (_map.MapFrame.DrawingLayers.Count > 0 && _map.MapFrame.DrawingLayers[0].LegendText == "ROI")
                { _map.MapFrame.DrawingLayers.Insert(1, polygonLayer); }
                else
                { _map.MapFrame.DrawingLayers.Insert(0, polygonLayer); }
            }

            return polygonLayer;
        }

        //在绘制结束后，根据配置参数对图层进行相应操作
        protected void ProcessPolygon(IMapPolygonLayer tempLayer)
        {
            if (PolygonCanIn)
            {
                IMapPolygonLayer polygonLayer = GetPolygonLayer();
                if (WillClearPolygons)
                {
                    polygonLayer.DataSet.Features.Clear();
                }
                foreach (IFeature tempFeature in tempLayer.DataSet.Features)
                {
                    polygonLayer.DataSet.Features.Add(tempFeature);
                }
                if (OperatedFunc != null)
                {
                    OperatedFunc(polygonLayer);
                }
            }
            else
            {
                _map.MapFrame.DrawingLayers.Add(tempLayer);
                _map.MapFrame.DrawingLayers.Remove(tempLayer);
                if (OperatedFunc != null)
                {
                    OperatedFunc(tempLayer);
                }
            }
        }

        #endregion

        #region Event

        // Handles the MouseDown
        protected override void OnMouseDown(GeoMouseArgs e)
        {
            if (e.Button == MouseButtons.Left && _isEnabled == true)
            {
                _currentPoint = e.Location;
                System.Drawing.Point point = new System.Drawing.Point(_currentPoint.X, _currentPoint.Y);
                if (!_points.Contains(point))
                {
                    _points.Add(point);
                }
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(GeoMouseArgs e)
        {
            _currentPoint = e.Location;
            if (_isEnabled == true)
            {
                _map.Invalidate();
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseDoubleClick(GeoMouseArgs e)
        {
            _isEnabled = false;
            IMapPolygonLayer tempLayer = new MapPolygonLayer();
            List<List<System.Drawing.Point>> polygons = GIS.Common.ComputationalGeometry.ClipperOperation.DecomposeNonSimplePolygon(_points);
            foreach (List<System.Drawing.Point> polygonPoints in polygons)
            {
                _coordinatePoints.Clear();
                for (int i = 0; i < polygonPoints.Count; i++)
                {
                    _coordinatePoints.Add(_map.PixelToProj(new System.Drawing.Point(polygonPoints[i].X, polygonPoints[i].Y)));
                }
                _coordinatePoints.Add(_map.PixelToProj(new System.Drawing.Point(polygonPoints[0].X, polygonPoints[0].Y)));
                LinearRing linearRing = new LinearRing(_coordinatePoints.ToArray()); ;
                Polygon polygon = new Polygon(linearRing);
                tempLayer.DataSet.Features.Add(polygon as IGeometry);
            }
            ProcessPolygon(tempLayer);

            _points.Clear();
            _map.Refresh();
            _isEnabled = true;
        }

        #endregion
    }
}
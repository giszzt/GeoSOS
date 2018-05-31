using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ClipperLib;

namespace GIS.Common.ComputationalGeometry
{
    using ClipperPolygon = List<IntPoint>;
    using ClipperPolygons = List<List<IntPoint>>;
    public static class ClipperOperation
    {
        public static List<List<Point>> DecomposeNonSimplePolygon(List<Point> points)
        {
            List<List<Point>> result = new List<List<Point>>();
            ClipperPolygon polygon = new ClipperPolygon();
            foreach (Point point in points)
            {
                IntPoint intPoint = new IntPoint(point.X, point.Y);
                polygon.Add(intPoint);
            }
            ClipperPolygons polygons = Clipper.SimplifyPolygon(polygon, PolyFillType.pftEvenOdd);
            foreach (List<IntPoint> item in polygons)
            {
                List<Point> pointList = new List<Point>();
                foreach (IntPoint subitem in item)
                {
                    pointList.Add(new Point((int)subitem.X,(int)subitem.Y));
                }
                result.Add(pointList);
            }
            return result; 
        }
    }
}

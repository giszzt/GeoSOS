using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotSpatial.Symbology;

namespace GIS.FrameWork
{
    public static class ROIConfigure
    {
        #region Properties

        private static IPointSymbolizer _pointSymbolizer = new PointSymbolizer(Color.Pink, DotSpatial.Symbology.PointShape.Ellipse, 16);
        private static ILineSymbolizer _lineSymbolizer = new LineSymbolizer(Color.LightGreen, 2);
        private static IPolygonSymbolizer _polygonSymbolizer = new PolygonSymbolizer(Color.Yellow, Color.LightSkyBlue, 1);
        private static IPolygonSymbolizer _bufferSymbolizer = new PolygonSymbolizer(Color.LightGray, Color.LightSkyBlue, 1);

        public static IPointSymbolizer pointSymbolizer
        {
            get { return _pointSymbolizer; }
            set { _pointSymbolizer = value; }
        }
        public static ILineSymbolizer lineSymbolizer
        {
            get { return _lineSymbolizer; }
            set { _lineSymbolizer = value; }
        }          
        public static IPolygonSymbolizer polygonSymbolizer
        {
            get { return _polygonSymbolizer; }
            set { _polygonSymbolizer = value; }
        } 
        public static IPolygonSymbolizer bufferSymbolizer
        {
            get { return _bufferSymbolizer; }
            set { _bufferSymbolizer = value; }
        }


        #endregion
    }
}

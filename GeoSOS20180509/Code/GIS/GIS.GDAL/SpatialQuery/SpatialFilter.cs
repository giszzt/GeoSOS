using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GIS.GDAL.SpatialQuery
{
    public class SpatialFilter
    {
        #region Private Variable

        private OSGeo.OGR.Geometry _geo;
        private SpatialRel _spatialRel;

        #endregion

        #region Properties

        public OSGeo.OGR.Geometry Geometry
        {
            get
            {
                return _geo;
            }
            set
            {
                _geo = value;
            }
        }

        public SpatialRel SpatialRel
        {
            get
            {
                return _spatialRel;
            }
            set
            {
                _spatialRel = value;
            }
        }

        #endregion

        public SpatialFilter(OSGeo.OGR.Geometry geo, SpatialRel sr)
        {
            _geo = geo;
            _spatialRel = sr;
        }

        /// <summary>
        /// Query by geometry
        /// </summary>
        /// <param name="geo">input geometry</param>
        /// <param name="layer">target layer</param>
        /// <returns></returns>
        public List<int> QueryByGeometry(OSGeo.OGR.Geometry geo, OSGeo.OGR.Layer layer, SpatialRel sr)
        {
            List<int> result = new List<int>();
            try
            {
                OSGeo.OGR.Feature oFeature = null;

                while ((oFeature = layer.GetNextFeature()) != null)
                {
                    OSGeo.OGR.Geometry oGeometry = oFeature.GetGeometryRef();
                    switch (sr)
                    {
                        case SpatialQuery.SpatialRel.Undefined: break;
                        case SpatialQuery.SpatialRel.Intersects:
                            if (geo.Intersect(oGeometry))
                            {
                                result.Add((int)oFeature.GetFID());
                            }
                            break;
                        case SpatialQuery.SpatialRel.Contains:
                            if (oGeometry.Contains(geo))
                            {
                                result.Add((int)oFeature.GetFID());
                            }
                            break;
                        case SpatialQuery.SpatialRel.Crosses:
                            if (geo.Crosses(oGeometry))
                            {
                                result.Add((int)oFeature.GetFID());
                            }
                            break;
                        case SpatialQuery.SpatialRel.Overlaps:
                            if (geo.Overlaps(oGeometry))
                            {
                                result.Add((int)oFeature.GetFID());
                            }
                            break;
                        case SpatialQuery.SpatialRel.Touches:
                            if (geo.Touches(oGeometry))
                            {
                                result.Add((int)oFeature.GetFID());
                            }
                            break;
                        case SpatialQuery.SpatialRel.WithIn:
                            if (oGeometry.Within(geo))
                            {
                                result.Add((int)oFeature.GetFID());
                            }
                            break;
                    }
                }
            }
            catch
            {
                throw;
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotSpatial.Data;
using DotSpatial.Symbology;
using DotSpatial.Serialization;
using GeoAPI.Geometries;
using NetTopologySuite.Operation.Relate;
using NetTopologySuite.Geometries;

namespace GIS.GDAL
{
    public static class OgrQuery
    {
        /// <summary>
        /// Spatial filter using OGR
        /// </summary>
        /// <param name="featureSet">FeatureSet</param>
        /// <param name="region">Spatial region</param>
        /// <returns>Indexs of selected features</returns>
        public static List<int> OgrSpatialFilter(IFeatureSet featureSet, IFeatureSet region)
        {
            List<int> result = new List<int>();
            IGeometry geometry = GetGeometry(region);
            result = OgrSpatialFilter(featureSet, geometry);
            return result;
        }

        /// <summary>
        /// Spatial filter using OGR
        /// </summary>
        /// <param name="featureSet">FeatureSet</param>
        /// <param name="geometry">Spatial geometry</param>
        /// <returns>Indexs of selected features</returns>
        public static List<int> OgrSpatialFilter(IFeatureSet featureSet, IGeometry geometry)
        {
            List<int> result = new List<int>();

            OSGeo.OGR.Layer layer = GIS.GDAL.VectorConverter.DS2OrgLayer(featureSet);
            OSGeo.OGR.Geometry geo = GIS.GDAL.VectorConverter.DS2OgrGeometry(geometry, featureSet);
            layer.SetSpatialFilter(geo);

            OSGeo.OGR.Feature feature = layer.GetNextFeature();
            while (feature != null)
            {
                result.Add(Convert.ToInt32(feature.GetFID()));
                feature = layer.GetNextFeature();
            }

            return result;
        }

        /// <summary>
        /// Spatial query using OGR
        /// </summary>
        /// <param name="featureSet">FeatureSet</param>
        /// <param name="geometry">Spatial geometry</param>
        /// <returns>Selected features</returns>
        public static IFeatureSet OgrSpatialQuery(IFeatureSet featureSet, IGeometry geometry)
        {
            List<int> featureList = OgrSpatialFilter(featureSet, geometry);
            IFeatureSet result = featureSet.CopySubset(featureList);
            return result;
        }

        /// <summary>
        /// Spatial query using OGR
        /// </summary>
        /// <param name="featureSet">FeatureSet</param>
        /// <param name="region">Spatial region</param>
        /// <returns>Selected features</returns>
        public static IFeatureSet OgrSpatialQuery(IFeatureSet featureSet, IFeatureSet region)
        {
            List<int> featureList = OgrSpatialFilter(featureSet, region);
            IFeatureSet result = featureSet.CopySubset(featureList);
            return result;
        }

        /// <summary>
        /// Get Geometry of featureSet
        /// </summary>
        /// <param name="featureSet">FeatureSet</param>
        /// <returns>Geometry</returns>
        public static IGeometry GetGeometry(IFeatureSet featureSet)
        {
            IGeometry[] filterGeom = new IGeometry[featureSet.Features.Count];
            for (int i = 0; i < featureSet.Features.Count; i++)
            {
                filterGeom[i] = featureSet.Features[i].Geometry;
            }
            IGeometryCollection geomColl = new GeometryCollection(filterGeom);
            IGeometry geometry = geomColl as IGeometry;
            return geometry;
        }
    }
}

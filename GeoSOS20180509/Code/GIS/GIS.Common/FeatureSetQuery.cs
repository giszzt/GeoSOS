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

namespace GIS.Common
{
    /// <summary>
    /// Query Common Methods of FeatureSet
    /// </summary>
    public static class FeatureSetQuery
    {
        /// <summary>
        /// Spatial filter using DotSpatial
        /// </summary>
        /// <param name="featureSet">FeatureSet</param>
        /// <param name="region">Spatial region</param>
        /// <returns>Indexs of selected features</returns>
        public static List<int> SpatialFilter(IFeatureSet featureSet, IFeatureSet region)
        {
            IGeometry geometry = GetGeometry(region);
            return SpatialFilter(featureSet, geometry);
        }

        /// <summary>
        /// Spatial filter using DotSpatial
        /// </summary>
        /// <param name="featureSet">FeatureSet</param>
        /// <param name="geometry">Spatial geometry</param>
        /// <returns>Indexs of selected features</returns>
        public static List<int> SpatialFilter(IFeatureSet featureSet, IGeometry geometry)
        {
            List<int> indexList = new List<int>();

            for (int i = 0; i < featureSet.Features.Count; i++)
            {               
                if (RelateOp.Relate(featureSet.Features[i].Geometry, geometry).IsIntersects())
                {
                    indexList.Add(i);
                }
            }    
       
            return indexList;
        }




        /// <summary>
        /// Spatial query using DotSpatial
        /// </summary>
        /// <param name="featureSet">FeatureSet</param>
        /// <param name="region">Spatial region</param>
        /// <returns>Selected features</returns>
        public static IFeatureSet SpatialQuery(IFeatureSet featureSet, IFeatureSet region)
        {
            List<int> indexList = SpatialFilter(featureSet, region);
            IFeatureSet resultSet = featureSet.CopySubset(indexList);
            return resultSet;
        }

        /// <summary>
        /// Spatial query using DotSpatial
        /// </summary>
        /// <param name="featureSet">FeatureSet</param>
        /// <param name="geometry">Spatial geometry</param>
        /// <returns>Selected features</returns>
        public static IFeatureSet SpatialQuery(IFeatureSet featureSet, IGeometry geometry)
        {
            List<IFeature> features = new List<IFeature>();

            for (int i = 0; i < featureSet.Features.Count; i++)
            {
                if (RelateOp.Relate(featureSet.Features[i].Geometry, geometry).IsIntersects())
                {
                    features.Add(featureSet.Features[i]);
                }
            }

            FeatureSet result = new FeatureSet(features);
            result.Projection = CloneableEM.Copy(featureSet.Projection);
            result.InvalidateEnvelope(); // the new set will likely have a different envelope bounds

            return result;
        }

        /// <summary>
        /// Spatial filter using DotSpatial
        /// </summary>
        /// <param name="featureSet">FeatureSet</param>
        /// <param name="region">Spatial region</param>
        /// <param name="filterExpression">SQL expression</param>
        /// <returns>Indexs of selected features</returns>
        public static List<int> Filter(IFeatureSet featureSet, IFeatureSet region, string filterExpression)
        {
            IGeometry geometry = GetGeometry(region);
            return SpatialFilter(featureSet.CopySubset(filterExpression), geometry);
        }

        /// <summary>
        /// Spatial filter using DotSpatial
        /// </summary>
        /// <param name="featureSet">FeatureSet</param>
        /// <param name="geometry">Spatial geometry</param>
        /// <param name="filterExpression">SQL expression</param>
        /// <returns>Indexs of selected features</returns>
        public static List<int> Filter(IFeatureSet featureSet, IGeometry geometry, string filterExpression)
        {
            return SpatialFilter(featureSet.CopySubset(filterExpression), geometry);
        }

        /// <summary>
        /// Spatial query using DotSpatial
        /// </summary>
        /// <param name="featureSet">FeatureSet</param>
        /// <param name="region">Spatial region</param>
        /// <param name="filterExpression">SQL expression</param>
        /// <returns>Selected features</returns>
        public static IFeatureSet Query(IFeatureSet featureSet, IFeatureSet region, string filterExpression)
        {
            return SpatialQuery(featureSet.CopySubset(filterExpression), region);
        }

        /// <summary>
        /// Spatial query using DotSpatial
        /// </summary>
        /// <param name="featureSet">FeatureSet</param>
        /// <param name="geometry">Spatial geometry</param>
        /// <param name="filterExpression">SQL expression</param>
        /// <returns>Selected features</returns>
        public static IFeatureSet Query(IFeatureSet featureSet, IGeometry geometry, string filterExpression)
        {
            return SpatialQuery(featureSet.CopySubset(filterExpression), geometry);
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

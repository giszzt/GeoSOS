using FrameWork.Gui;
using OSGeo.OGR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GIS.GDAL.Overlay
{
    public class Overlay
    {
        #region Overlay options

        /// <summary>
        /// Overlay options 
        /// </summary>
        /// <param name="baseLayerPath">the complete path of the base layer</param>
        /// <param name="overlayLayerPath">the complete path of the overlay layer</param>
        /// <param name="resultLayerPath">the complete path of result layer</param>
        /// <param name="type">overlay option type</param>
        public static bool OverlayOperate(OSGeo.OGR.Layer baseLayer, OSGeo.OGR.Layer overlayLayer,
                                          ref OSGeo.OGR.Layer resultLayer, OverlayType type, Ogr.GDALProgressFuncDelegate callback)
        {
            try
            {
                string[] options = new string[] { "SKIP_FAILURES=YES" };

                switch (type)
                {
                    case OverlayType.Intersects:
                        baseLayer.Intersection(overlayLayer, resultLayer, options, callback, "Intersection");
                        break;
                    case OverlayType.Union:
                        baseLayer.Union(overlayLayer, resultLayer, options, callback, "Union");
                        break;
                    case OverlayType.SymDifference:
                        baseLayer.SymDifference(overlayLayer, resultLayer, options, callback, "SymDifference");
                        break;
                    case OverlayType.Identity:
                        baseLayer.Identity(overlayLayer, resultLayer, options, callback, "Identity");
                        break;
                    case OverlayType.Update:
                        baseLayer.Update(overlayLayer, resultLayer, options, callback, "Update");
                        break;
                    case OverlayType.Clip:
                        baseLayer.Clip(overlayLayer, resultLayer, options, callback, "Clip");
                        break;
                    case OverlayType.Erase:
                        baseLayer.Erase(overlayLayer, resultLayer, options, callback, "Erase");
                        break;
                }
         
                return true;
            }
            catch (Exception e)
            {
                //throw e;
                return false;
            }
        }

        public static bool OverlayOperate(string baseLayerPath, string overlayLayerPath, 
                                          ref OSGeo.OGR.Layer resultLayer, OverlayType type, Ogr.GDALProgressFuncDelegate callback)
        {
            OSGeo.OGR.Layer baseLayer = VectorConverter.GetOgrLayer(baseLayerPath);
            OSGeo.OGR.Layer overlayLayer = VectorConverter.GetOgrLayer(overlayLayerPath);
            bool result = OverlayOperate(baseLayer, overlayLayer, ref resultLayer, type, callback);
            return result;
        }

        public static bool OverlayOperate(DotSpatial.Data.IFeatureSet baseFeatureSet, DotSpatial.Data.IFeatureSet overlayFeatureSet,
                                          ref OSGeo.OGR.Layer resultLayer, OverlayType type, Ogr.GDALProgressFuncDelegate callback)
        {
            OSGeo.OGR.Layer baseLayer = VectorConverter.DS2OrgLayer(baseFeatureSet);
            OSGeo.OGR.Layer overlayLayer = VectorConverter.DS2OrgLayer(overlayFeatureSet);
            bool result = OverlayOperate(baseLayer, overlayLayer, ref resultLayer, type, callback);
            return result;
        }


        #endregion
    }
}

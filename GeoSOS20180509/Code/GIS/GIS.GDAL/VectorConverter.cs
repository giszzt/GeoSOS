using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GIS.GDAL
{
    /// <summary>
    /// Converter between DotSpatial and OGR
    /// </summary>
    public static class VectorConverter
    {

        #region Ogr to DotSpatial

        /// <summary>
        /// Ogr to DotSpatial at layer level
        /// </summary>
        /// <param name="layer">Ogr Layer</param>
        /// <returns>DotSpatial IFeatureSet</returns>
        public static DotSpatial.Data.IFeatureSet Ogr2DSLayer(OSGeo.OGR.Layer layer)
        {
            DotSpatial.Data.IFeatureSet featureSet = new DotSpatial.Data.FeatureSet();
            featureSet.FeatureType = Ogr2DSGeometryType(layer.GetGeomType());
            featureSet.DataTable = Ogr2DSDataTable(layer.GetLayerDefn());
            featureSet.Projection = Ogr2DSProjection(layer.GetSpatialRef());

            for (int i = 0; i < layer.GetFeatureCount(0); i++)
            {
                OSGeo.OGR.Geometry geometry = layer.GetFeature(i).GetGeometryRef();
                DotSpatial.Data.IFeature feature = new DotSpatial.Data.Feature(Ogr2DSGeometry(geometry));
                OSGeo.OGR.Feature ogrFeature = layer.GetFeature(i);
                feature.DataRow = featureSet.DataTable.NewRow();
                for (int j = 0; j < layer.GetLayerDefn().GetFieldCount(); j++)
                {
                    object value = GetOgrValue(ogrFeature, j);
                    if (value == null)
                    {
                        value = DBNull.Value;
                    }
                    feature.DataRow[j] = value;
                }
                featureSet.Features.Add(feature);
            }

            return featureSet;
        }

        /// <summary>
        /// Get value from Ogr feature
        /// </summary>
        /// <param name="feature">Ogr feature</param>
        /// <param name="i">Field index</param>
        /// <returns>Value</returns>
        public static object GetOgrValue(OSGeo.OGR.Feature feature, int i)
        {
            switch (feature.GetFieldDefnRef(i).GetFieldType())
            {
                case OSGeo.OGR.FieldType.OFTString:
                    return feature.GetFieldAsString(i);

                case OSGeo.OGR.FieldType.OFTInteger:
                    return feature.GetFieldAsInteger(i);

                case OSGeo.OGR.FieldType.OFTDateTime:
                    return GetFieldAsDateTime(feature, i);

                case OSGeo.OGR.FieldType.OFTReal:
                    return feature.GetFieldAsDouble(i);

                default:
                    return null;
            }
        }

        /// <summary>
        /// Get date and time from Ogr feature
        /// </summary>
        /// <param name="feature">Ogr feature</param>
        /// <param name="i">Field index</param>
        /// <returns>DateTime value</returns>
        public static DateTime GetFieldAsDateTime(OSGeo.OGR.Feature feature, int i)
        {
            int year;
            int month;
            int day;

            int h;
            int m;
            float s;
            int flag;

            feature.GetFieldAsDateTime(i, out year, out month, out day, out h, out m, out s, out flag);
            try
            {
                return new DateTime(year, month, day, h, m, (int)s);
            }
            catch (ArgumentOutOfRangeException)
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Ogr to DotSpatial at geometry level
        /// </summary>
        /// <param name="geometry">Ogr Geometry</param>
        /// <returns>GeoAPI/DotSpatial IGeometry</returns>
        public static GeoAPI.Geometries.IGeometry Ogr2DSGeometry(OSGeo.OGR.Geometry geometry)
        {
            geometry.FlattenTo2D();
            Byte[] wkbGeometry = new Byte[geometry.WkbSize()];
            geometry.ExportToWkb(wkbGeometry, OSGeo.OGR.wkbByteOrder.wkbXDR);

            NetTopologySuite.IO.WKBReader wkbReader = new NetTopologySuite.IO.WKBReader();
            return wkbReader.Read(wkbGeometry);
        }

        /// <summary>
        /// Ogr to DotSpatial at projection level
        /// </summary>
        /// <param name="spatialReference">Ogr SpatialReference</param>
        /// <returns>DotSpatial ProjectionInfo</returns>
        public static DotSpatial.Projections.ProjectionInfo Ogr2DSProjection(OSGeo.OSR.SpatialReference spatialReference)
        {
            string proj4 = string.Empty;
            int i = spatialReference.ExportToProj4(out proj4);
            DotSpatial.Projections.ProjectionInfo projectionInfo = DotSpatial.Projections.ProjectionInfo.FromProj4String(proj4);
            return projectionInfo;
        }

        /// <summary>
        /// Ogr to DotSpatial at feature fields level
        /// </summary>
        /// <param name="featureDefn">Ogr FeatureDefn</param>
        /// <returns>DataTable of FeatureSet</returns>
        public static DataTable Ogr2DSDataTable(OSGeo.OGR.FeatureDefn featureDefn)
        {
            DataTable dataTable = new DataTable();

            for (int i = 0; i < featureDefn.GetFieldCount(); i++)
            {
                OSGeo.OGR.FieldDefn fieldDefn = featureDefn.GetFieldDefn(i);
                DataColumn dataColumn = Ogr2DSField(fieldDefn);
                dataTable.Columns.Add(dataColumn);
            }
            return dataTable;
        }

        /// <summary>
        /// Ogr to DotSpatial at feature field level
        /// </summary>
        /// <param name="fieldDefn">Ogr FieldDefn</param>
        /// <returns>DataColumn of DataTable of FeatureSet</returns>
        public static DataColumn Ogr2DSField(OSGeo.OGR.FieldDefn fieldDefn)
        {
            DataColumn dataColumn = new DataColumn();
            dataColumn.ColumnName = fieldDefn.GetName();
            dataColumn.Caption = fieldDefn.GetName();//column title
            OSGeo.OGR.FieldType fieldType = fieldDefn.GetFieldType();
            switch (fieldType)
            {
                case OSGeo.OGR.FieldType.OFTInteger:
                    dataColumn.DataType = typeof(int);
                    break;
                case OSGeo.OGR.FieldType.OFTString:
                    dataColumn.DataType = typeof(string);
                    break;
                case OSGeo.OGR.FieldType.OFTDate:
                    dataColumn.DataType = typeof(DateTime);
                    break;
                case OSGeo.OGR.FieldType.OFTReal:
                    dataColumn.DataType = typeof(double);
                    break;
                default:
                    dataColumn.DataType = typeof(string);
                    break;
            }
            return dataColumn;
        }

        /// <summary>
        /// Ogr to DotSpatial at geometry type level
        /// </summary>
        /// <param name="ogrType">Ogr wkbGeometryType</param>
        /// <returns>DotSpatial FeatureType</returns>
        public static DotSpatial.Data.FeatureType Ogr2DSGeometryType(OSGeo.OGR.wkbGeometryType ogrType)
        {
            switch (ogrType)
            {
                case OSGeo.OGR.wkbGeometryType.wkbLineString:
                    return DotSpatial.Data.FeatureType.Line;

                case OSGeo.OGR.wkbGeometryType.wkbPolygon:
                    return DotSpatial.Data.FeatureType.Polygon;

                case OSGeo.OGR.wkbGeometryType.wkbPoint:
                    return DotSpatial.Data.FeatureType.Point;

                case OSGeo.OGR.wkbGeometryType.wkbMultiPoint:
                    return DotSpatial.Data.FeatureType.MultiPoint;

                default:
                    return DotSpatial.Data.FeatureType.Unspecified;
            }
        }

        #endregion

        #region DotSpatial to Ogr

        /// <summary>
        /// DotSpatial to Ogr at layer level
        /// </summary>
        /// <param name="featureSet">DotSpatial IFeatureSet</param>
        /// <returns>Ogr Layer</returns>
        public static OSGeo.OGR.Layer DS2OrgLayer(DotSpatial.Data.IFeatureSet featureSet)
        {
            using (OSGeo.OGR.Driver driver = OSGeo.OGR.Ogr.GetDriverByName("Memory"))
            {
                OSGeo.OGR.DataSource dataSource = driver.CreateDataSource(featureSet.Name, null);
                OSGeo.OGR.Layer layer = dataSource.CreateLayer("Result", DS2OgrProjection(featureSet.Projection), DS2OgrGeometryType(featureSet.FeatureType), new string[] { });
                OSGeo.OGR.FeatureDefn featureDefn = DS2OgrDataTable(featureSet.DataTable);

                for (int k = 0; k < featureDefn.GetFieldCount(); k++)
                {
                    layer.CreateField(featureDefn.GetFieldDefn(k), 0);
                }

                for (int i = 0; i < featureSet.NumRows(); i++)
                {
                    DotSpatial.Data.IFeature feature = featureSet.GetFeature(i);
                    OSGeo.OGR.Feature ogrFeature = new OSGeo.OGR.Feature(featureDefn);
                    ogrFeature.SetGeometry(DS2OgrGeometry(feature.Geometry, featureSet));
                    for (int j = 0; j < feature.DataRow.ItemArray.Length; j++)
                    {
                        #region Set Value to Feature

                        object value = feature.DataRow.ItemArray.GetValue(j);
                        if (value is int)
                        {
                            ogrFeature.SetField(j, (int)value);
                        }
                        else if (value is double)
                        {
                            ogrFeature.SetField(j, (double)value);
                        }
                        else if (value is string)
                        {
                            ogrFeature.SetField(j, (string)value);
                        }
                        else if (value is DateTime)
                        {
                            DateTime dateTime = (DateTime)value;
                            ogrFeature.SetField(j, dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, (float)dateTime.Second, 0);
                        }

                        #endregion
                    }
                    layer.CreateFeature(ogrFeature);
                }
                dataSource.FlushCache();
                return layer;
            }
        }

        public static OSGeo.OGR.Layer DS2OrgLayer(DotSpatial.Data.IFeatureSet featureSet, OSGeo.OGR.DataSource dataSource)
        {
            OSGeo.OGR.Layer layer = dataSource.CreateLayer("Result", DS2OgrProjection(featureSet.Projection), DS2OgrGeometryType(featureSet.FeatureType), new string[] { });
            OSGeo.OGR.FeatureDefn featureDefn = DS2OgrDataTable(featureSet.DataTable);
            for (int i = 0; i < featureSet.NumRows(); i++)
            {
                DotSpatial.Data.IFeature feature = featureSet.GetFeature(i);
                OSGeo.OGR.Feature ogrFeature = new OSGeo.OGR.Feature(featureDefn);
                ogrFeature.SetGeometry(DS2OgrGeometry(feature.Geometry, featureSet));
                for (int j = 0; j < feature.DataRow.ItemArray.Length; j++)
                {
                    #region Set Value to Feature

                    object value = feature.DataRow.ItemArray.GetValue(j);
                    if (value is int)
                    {
                        ogrFeature.SetField(j, (int)value);
                    }
                    else if (value is double)
                    {
                        ogrFeature.SetField(j, (double)value);
                    }
                    else if (value is string)
                    {
                        ogrFeature.SetField(j, (string)value);
                    }
                    else if (value is DateTime)
                    {
                        DateTime dateTime = (DateTime)value;
                        ogrFeature.SetField(j, dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, (float)dateTime.Second, 0);
                    }

                    #endregion
                }
                layer.CreateFeature(ogrFeature);
            }
            dataSource.FlushCache();
            return layer;
        }

        public static OSGeo.OGR.Layer DS2OrgLayer(DotSpatial.Data.IFeatureSet featureSet, OSGeo.OGR.Layer layer)
        {
            using (OSGeo.OGR.FeatureDefn featureDefn = DS2OgrDataTable(featureSet.DataTable))
            {
                for (int k = 0; k < featureDefn.GetFieldCount(); k++)
                {
                    layer.CreateField(featureDefn.GetFieldDefn(k), 0);
                }

                for (int i = 0; i < featureSet.NumRows(); i++)
                {
                    DotSpatial.Data.IFeature feature = featureSet.GetFeature(i);
                    using (OSGeo.OGR.Feature ogrFeature = new OSGeo.OGR.Feature(featureDefn))
                    {
                        ogrFeature.SetGeometry(DS2OgrGeometry(feature.Geometry, featureSet));
                        for (int j = 0; j < feature.DataRow.ItemArray.Length; j++)
                        {
                            #region Set Value to Feature

                            object value = feature.DataRow.ItemArray.GetValue(j);
                            if (value is int)
                            {
                                ogrFeature.SetField(j, (int)value);
                            }
                            else if (value is double)
                            {
                                ogrFeature.SetField(j, (double)value);
                            }
                            else if (value is string)
                            {
                                ogrFeature.SetField(j, (string)value);
                            }
                            else if (value is DateTime)
                            {
                                DateTime dateTime = (DateTime)value;
                                ogrFeature.SetField(j, dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, (float)dateTime.Second, 0);
                            }

                            #endregion
                        }
                        layer.CreateFeature(ogrFeature);
                    }
                }
            }
            return layer;
        }

        /// <summary>
        /// DotSpatial to Ogr at geometry level
        /// </summary>
        /// <param name="geometry">GeoAPI/DotSpatial IGeometry</param>
        /// <param name="featureSet">DotSpatial IFeatureSet</param>
        /// <returns>Ogr Geometry</returns>
        public static OSGeo.OGR.Geometry DS2OgrGeometry(GeoAPI.Geometries.IGeometry geometry, DotSpatial.Data.IFeatureSet featureSet)
        {
            string wkbGeometry = geometry.AsText();
            OSGeo.OGR.Geometry result = OSGeo.OGR.Ogr.CreateGeometryFromWkt(ref wkbGeometry, DS2OgrProjection(featureSet.Projection));
            return result;
        }

        /// <summary>
        /// DotSpatial to Ogr at projection level
        /// </summary>
        /// <param name="projectionInfo">DotSpatial ProjectionInfo</param>
        /// <returns>Ogr SpatialReference</returns>
        public static OSGeo.OSR.SpatialReference DS2OgrProjection(DotSpatial.Projections.ProjectionInfo projectionInfo)
        {
            string proj4 = projectionInfo.ToProj4String();
            OSGeo.OSR.SpatialReference spatialReference = new OSGeo.OSR.SpatialReference("");
            spatialReference.ImportFromProj4(proj4);
            return spatialReference;
        }

        /// <summary>
        /// DotSpatial to Ogr at feature fields level
        /// </summary>
        /// <param name="dataTable">DataTable of FeatureSet</param>
        /// <returns>Ogr FeatureDefn</returns>
        public static OSGeo.OGR.FeatureDefn DS2OgrDataTable(DataTable dataTable)
        {
            OSGeo.OGR.FeatureDefn featureDefn = new OSGeo.OGR.FeatureDefn(dataTable.TableName);
            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                OSGeo.OGR.FieldDefn fieldDefn = DS2OgrField(dataColumn);
                featureDefn.AddFieldDefn(fieldDefn);
            }
            return featureDefn;
        }

        /// <summary>
        /// DotSpatial to Ogr at feature field level
        /// </summary>
        /// <param name="dataColumn">DataColumn of DataTable of FeatureSet</param>
        /// <returns>Ogr FieldDefn</returns>
        public static OSGeo.OGR.FieldDefn DS2OgrField(DataColumn dataColumn)
        {
            OSGeo.OGR.FieldType fieldType = new OSGeo.OGR.FieldType();
            string type = dataColumn.DataType.Name.ToUpper();
            switch (type)
            {
                case "INT":
                    fieldType = OSGeo.OGR.FieldType.OFTInteger;
                    break;
                case "STRING":
                    fieldType = OSGeo.OGR.FieldType.OFTString;
                    break;
                case "DATETIME":
                    fieldType = OSGeo.OGR.FieldType.OFTDate;
                    break;
                case "DOUBLE":
                    fieldType = OSGeo.OGR.FieldType.OFTReal;
                    break;
                default:
                    fieldType = OSGeo.OGR.FieldType.OFTString;
                    break;
            }
            return new OSGeo.OGR.FieldDefn(dataColumn.Caption, fieldType);
        }

        /// <summary>
        /// DotSpatial to Ogr at geometry type level
        /// </summary>
        /// <param name="featureType">DotSpatial FeatureType</param>
        /// <returns>Ogr wkbGeometryType</returns>
        public static OSGeo.OGR.wkbGeometryType DS2OgrGeometryType(DotSpatial.Data.FeatureType featureType)
        {
            switch (featureType)
            {
                case DotSpatial.Data.FeatureType.Line:
                    return OSGeo.OGR.wkbGeometryType.wkbLineString;

                case DotSpatial.Data.FeatureType.Polygon:
                    return OSGeo.OGR.wkbGeometryType.wkbPolygon;

                case DotSpatial.Data.FeatureType.Point:
                    return OSGeo.OGR.wkbGeometryType.wkbPoint;

                case DotSpatial.Data.FeatureType.MultiPoint:
                    return OSGeo.OGR.wkbGeometryType.wkbMultiPoint;

                default:
                    return OSGeo.OGR.wkbGeometryType.wkbUnknown;
            }
        }

        #endregion

        #region Ogr I/O

        /// <summary>
        /// Get Layer from Path
        /// </summary>
        /// <param name="path">Path of Layer</param>
        /// <returns>Layer</returns>
        public static OSGeo.OGR.Layer GetOgrLayer(string path)
        {
            OSGeo.OGR.Layer layer = null;
            if (System.IO.File.Exists(path))
            {
                OSGeo.OGR.DataSource dsBaseLayer = OSGeo.OGR.Ogr.Open(path, 0);
                layer = dsBaseLayer.GetLayerByIndex(0);
            }
            return layer;
        }

        #endregion

    }
}

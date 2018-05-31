using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace GIS.GDAL
{
    /// <summary>
    /// Converter between DotSpatial and OGR
    /// </summary>
    public static class RasterConverter
    {
        #region Gdal to DotSpatial

        /// <summary>
        /// Gdal to DotSpatial at Raster Level
        /// </summary>
        /// <param name="dataset">Gdal Dataset</param>
        /// <param name="bandIndex">Index of Band</param>
        /// <returns>Dotspatial Raster</returns>
        public static DotSpatial.Data.IRaster Gdal2DSRaster(OSGeo.GDAL.Dataset dataset, int bandIndex)
        {
            DotSpatial.Data.IRaster result = null;
            OSGeo.GDAL.Band band = dataset.GetRasterBand(bandIndex);
            if (dataset != null)
            {
                switch (band.DataType)
                {
                    case OSGeo.GDAL.DataType.GDT_Byte:
                        result = new GdalRaster<byte>(dataset, band);
                        break;
                    case OSGeo.GDAL.DataType.GDT_CFloat32:
                    case OSGeo.GDAL.DataType.GDT_CFloat64:
                    case OSGeo.GDAL.DataType.GDT_CInt16:
                    case OSGeo.GDAL.DataType.GDT_CInt32:
                        break;
                    case OSGeo.GDAL.DataType.GDT_Float32:
                        result = new GdalRaster<float>(dataset, band);
                        break;
                    case OSGeo.GDAL.DataType.GDT_Float64:
                        result = new GdalRaster<double>(dataset, band);
                        break;
                    case OSGeo.GDAL.DataType.GDT_Int16:
                        result = new GdalRaster<short>(dataset, band);
                        break;
                    case OSGeo.GDAL.DataType.GDT_UInt16:
                    case OSGeo.GDAL.DataType.GDT_Int32:
                        result = new GdalRaster<int>(dataset, band);
                        break;
                    case OSGeo.GDAL.DataType.GDT_TypeCount:
                        break;
                    case OSGeo.GDAL.DataType.GDT_UInt32:
                        result = new GdalRaster<long>(dataset, band);
                        break;
                    case OSGeo.GDAL.DataType.GDT_Unknown:
                        break;
                    default:
                        break;
                }
            }

            if (result != null)
            {
                (result as DotSpatial.Data.Raster).Open();
            }

            return result;
        }

        #endregion

        #region DotSpatial to Gdal

        /// <summary>
        /// DotSpatial to Gdal at Raster Level
        /// </summary>
        /// <param name="raster">Dotspatial Raster</param>
        /// <param name="options">Set Options When Gdal Create Dataset. Default is null.</param>
        /// <param name="bandMap">Gdal WriteRaster Method Need to Set BandMap</param>
        /// <returns>Gdal Dataset</returns>
        public static OSGeo.GDAL.Dataset Ds2GdalRaster(DotSpatial.Data.IRaster raster, string[] options, int[] bandMap)
        {
            OSGeo.GDAL.Dataset result = null;
            OSGeo.GDAL.Driver driver = OSGeo.GDAL.Gdal.GetDriverByName("MEM");

            OSGeo.GDAL.DataType dataType;
            string rasterDataType = raster.DataType.ToString().ToUpper();
            switch (rasterDataType)
            {
                case "SYSTEM.INT32":
                case "SYSTEM.LONG":
                    dataType = OSGeo.GDAL.DataType.GDT_Int32;
                    break;
                case "SYSTEM.SHORT":
                    dataType = OSGeo.GDAL.DataType.GDT_Int16;
                    break;
                case "SYSTEM.DOUBLE":
                case "SYSTEM.SINGLE":
                    dataType = OSGeo.GDAL.DataType.GDT_Float64;
                    break;
                case "SYSTEM.FLOAT":
                    dataType = OSGeo.GDAL.DataType.GDT_Float32;
                    break;
                case "SYSTEM.BYTE":
                    dataType = OSGeo.GDAL.DataType.GDT_Byte;
                    break;
                default:
                    dataType = OSGeo.GDAL.DataType.GDT_Int16;
                    break;
            }

            result = driver.Create("", raster.NumColumnsInFile, raster.NumRowsInFile, 1, dataType, options);

            ///地理坐标信息
            result.SetGeoTransform(raster.Bounds.AffineCoefficients);
            ///投影设置 Set Projection
            result.SetProjection(raster.Projection.ToProj4String());
            ///波段个数
            int bandNum = raster.NumBands;
            if (bandNum < 1)
            {
                bandNum = 1;
            }

            ///赋值        
            switch (rasterDataType)
            {
                case "SYSTEM.INT32":
                case "SYSTEM.LONG":
                    result.WriteRaster(0, 0, raster.NumColumns, raster.NumRows, DS2GdalValue<int>(raster), raster.NumColumns, raster.NumRows, bandNum, bandMap, 0, 0, 0);
                    break;
                case "SYSTEM.SHORT":
                    result.WriteRaster(0, 0, raster.NumColumns, raster.NumRows, DS2GdalValue<short>(raster), raster.NumColumns, raster.NumRows, bandNum, bandMap, 0, 0, 0);
                    break;
                case "SYSTEM.DOUBLE":
                case "SYSTEM.SINGLE":
                    result.WriteRaster(0, 0, raster.NumColumns, raster.NumRows, DS2GdalValue<double>(raster), raster.NumColumns, raster.NumRows, bandNum, bandMap, 0, 0, 0);
                    break;
                case "SYSTEM.FLOAT":
                    result.WriteRaster(0, 0, raster.NumColumns, raster.NumRows, DS2GdalValue<float>(raster), raster.NumColumns, raster.NumRows, bandNum, bandMap, 0, 0, 0);
                    break;
                case "SYSTEM.BYTE":
                    result.WriteRaster(0, 0, raster.NumColumns, raster.NumRows, DS2GdalValue<byte>(raster), raster.NumColumns, raster.NumRows, bandNum, bandMap, 0, 0, 0);
                    break;
                default:
                    result.WriteRaster(0, 0, raster.NumColumns, raster.NumRows, DS2GdalValue<int>(raster), raster.NumColumns, raster.NumRows, bandNum, bandMap, 0, 0, 0);
                    break;
            }

            return result;
        }

        /// <summary>
        /// DotSpatial to Gdal at Value Level
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="raster">Dotspatial Raster</param>
        /// <returns>Value of Raster</returns>
        private static T[] DS2GdalValue<T>(DotSpatial.Data.IRaster raster)
        {
            T[] result = new T[raster.NumRows * raster.NumColumns];
            Object[] objects = new Object[raster.NumRows * raster.NumColumns];
            int i = 0;
            string rasterDataType = raster.DataType.ToString().ToUpper();
            switch (rasterDataType)
            {
                case "SYSTEM.INT32":
                case "SYSTEM.LONG":
                    for (int row = 0; row < raster.NumRows; row++)
                    {
                        for (int column = 0; column < raster.NumColumns; column++)
                        {
                            result[i] = (T)((object)(int)raster.Value[row, column]);
                            i++;
                        }
                    }
                    break;
                case "SYSTEM.SHORT":
                    for (int row = 0; row < raster.NumRows; row++)
                    {
                        for (int column = 0; column < raster.NumColumns; column++)
                        {
                            result[i] = (T)((object)(short)raster.Value[row, column]);
                            i++;
                        }
                    }
                    break;
                case "SYSTEM.DOUBLE":
                case "SYSTEM.SINGLE":
                    for (int row = 0; row < raster.NumRows; row++)
                    {
                        for (int column = 0; column < raster.NumColumns; column++)
                        {
                            result[i] = (T)((object)(double)raster.Value[row, column]);
                            i++;
                        }
                    }
                    break;
                case "SYSTEM.FLOAT":
                    for (int row = 0; row < raster.NumRows; row++)
                    {
                        for (int column = 0; column < raster.NumColumns; column++)
                        {
                            result[i] = (T)((object)(float)raster.Value[row, column]);
                            i++;
                        }
                    }
                    break;
                case "SYSTEM.BYTE":
                    for (int row = 0; row < raster.NumRows; row++)
                    {
                        for (int column = 0; column < raster.NumColumns; column++)
                        {
                            result[i] = (T)((object)(byte)raster.Value[row, column]);
                            i++;
                        }
                    }
                    break;
                default:
                    for (int row = 0; row < raster.NumRows; row++)
                    {
                        for (int column = 0; column < raster.NumColumns; column++)
                        {
                            result[i] = (T)((object)(int)raster.Value[row, column]);
                            i++;
                        }
                    }
                    break;
            }

            return result;
        }

        #endregion
    }
}

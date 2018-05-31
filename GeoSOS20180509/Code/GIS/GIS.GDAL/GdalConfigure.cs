using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace GIS.GDAL
{
    public class GdalConfigure
    {
        private static volatile bool _configuredOgr;
        private static volatile bool _configuredGdal;

        public static void Configure()
        {
            string gdalPath = AppDomain.CurrentDomain.BaseDirectory +"Library\\GDAL";
            string nativePath = Path.Combine(gdalPath, GetPlatform());
            string pathEnv = Environment.GetEnvironmentVariable("PATH");

            if (Directory.Exists(gdalPath))
            {
                string[] paths = new string[] 
                { 
                    nativePath,
                    Path.Combine(nativePath, @"gdal\plugins"),
                    Path.Combine(nativePath, @"gdal\csharp"),
                };

                foreach (string path in paths)
                {
                    if (!pathEnv.Contains(path))
                    {
                        if (Directory.Exists(path))
                        {
                            pathEnv = path + ";" + pathEnv;
                            Environment.SetEnvironmentVariable("PATH", pathEnv);
                        }
                        else
                        {
                            throw new DirectoryNotFoundException(string.Format("'{0}' directory not found.", path));
                        }
                    }
                }

                // Path to directory containing various GDAL data files
                //   (EPSG CSV files, S-57 definition files, DXF header and footer files, ...).
                // This option is read by the GDAL and OGR driver registration functions. 
                // It is used to expand EPSG codes into their description in the OSR model (WKT based).
                string GDAL_DATA = Path.Combine(nativePath, "gdal-data");
                Environment.SetEnvironmentVariable("GDAL_DATA", GDAL_DATA);
                OSGeo.GDAL.Gdal.SetConfigOption("GDAL_DATA", GDAL_DATA);

                // Path to directory containing driver files that start with the prefix "gdal_X.dll".
                string GDAL_DRIVER_PATH = Path.Combine(nativePath, @"gdal\plugins");
                Environment.SetEnvironmentVariable("GDAL_DRIVER_PATH", GDAL_DRIVER_PATH);
                OSGeo.GDAL.Gdal.SetConfigOption("GDAL_DRIVER_PATH", GDAL_DRIVER_PATH);

                // Path to directory containing EPSG files for the Proj.4 library.
                string PROJ_LIB = Path.Combine(nativePath, @"proj\SHARE");
                Environment.SetEnvironmentVariable("PROJ_LIB", PROJ_LIB);
                OSGeo.GDAL.Gdal.SetConfigOption("PROJ_LIB", PROJ_LIB);

                //RegisterOGROCI();

                // 为了支持中文路径，请添加下面这句代码  
                OSGeo.GDAL.Gdal.SetConfigOption("GDAL_FILENAME_IS_UTF8", "NO");
                // 为了使属性表字段支持中文，请添加下面这句  
                OSGeo.GDAL.Gdal.SetConfigOption("SHAPE_ENCODING", "");
                // This line throws an exception if the the wrong version of GDal was found in the path somewhere,
                // or the path didn't point to GDal correctly.
                OSGeo.OGR.Ogr.RegisterAll();

            }
            else
            {
                throw new DirectoryNotFoundException(string.Format("'{0}' directory not found.", gdalPath));
            }
        }

        /// <summary>  
        /// Function to determine which platform we're on  
        /// </summary>  
        private static string GetPlatform()
        {
            return IntPtr.Size == 4 ? "x86" : "x64";
        }

        /// <summary>
        /// Method to ensure the static constructor is being called.
        /// </summary>
        /// <remarks>Be sure to call this function before using Gdal/Ogr/Osr</remarks>
        public static void ConfigureOgr()
        {
            if (_configuredOgr) return;
            // Register drivers
            OSGeo.OGR.Ogr.RegisterAll();
            _configuredOgr = true;
        }

        /// <summary>
        /// Method to ensure the static constructor is being called.
        /// </summary>
        /// <remarks>Be sure to call this function before using Gdal/Ogr/Osr</remarks>
        public static void ConfigureGdal()
        {
            if (_configuredGdal) return;

            // Register drivers
            OSGeo.GDAL.Gdal.AllRegister();
            _configuredGdal = true;
        }

    }
}


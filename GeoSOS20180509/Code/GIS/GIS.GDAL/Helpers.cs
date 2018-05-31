using System;
using OSGeo.GDAL;

namespace GIS.GDAL
{
    static class Helpers
    {
        public static Dataset Open(string fileName)
        {
            try
            {
                return Gdal.Open(fileName, Access.GA_Update);
            }
            catch
            {
                try
                {
                    return Gdal.Open(fileName, Access.GA_ReadOnly);
                }
                catch (Exception ex)
                {
                    throw new GdalException(ex.ToString());
                }
            }
        }
    }
}
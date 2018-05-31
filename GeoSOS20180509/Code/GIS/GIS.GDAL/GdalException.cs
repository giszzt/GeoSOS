using System;
using OSGeo.GDAL;

namespace GIS.GDAL
{
    /// <summary>
    /// GDalException
    /// </summary>
    public class GdalException : ApplicationException
    {
        #region Constructors

        /// <summary>
        /// Creates a new Exception using Gdal.GetLastErrorMsg
        /// </summary>
        public GdalException()
            : base(Gdal.GetLastErrorMsg())
        {
        }

        /// <summary>
        /// Creates a new instance of GDalException
        /// </summary>
        public GdalException(string message)
            : base(message)
        {
        }

        ///<summary>
        /// Creates a new instance of GDalException with inner exception
        ///</summary>
        ///<param name="message"></param>
        ///<param name="innerException"></param>
        public GdalException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion
    }
}
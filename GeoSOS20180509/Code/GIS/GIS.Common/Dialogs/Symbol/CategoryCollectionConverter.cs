using System;
using System.ComponentModel;
using System.Globalization;
using DotSpatial.Data;
using DotSpatial.Symbology;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// CategoryCollectionConverter
    /// </summary>
    public class CategoryCollectionConverter : CollectionConverter
    {
        #region Private Variables

        #endregion

        #region Methods

        /// <summary>
        /// Converts the collection to a string
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return "Collection";
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// Determines how to convert from an interface
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(IChangeEventList<IPointCategory>))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        #endregion

        #region Properties

        #endregion
    }
}
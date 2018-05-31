using System;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// This class in an EventArgs that also supports a filter expression.
    /// </summary>
    public class FilterEventArgs : EventArgs
    {
        private readonly string _filterExpression;

        /// <summary>
        /// Initializes a new instance of the FilterEventArgs class.
        /// </summary>
        /// <param name="filterExpression">String, the filter expression to add.</param>
        public FilterEventArgs(string filterExpression)
        {
            _filterExpression = filterExpression;
        }

        /// <summary>
        /// Gets the string filter expression.
        /// </summary>
        public string FilterExpression
        {
            get
            {
                return _filterExpression;
            }
        }
    }
}
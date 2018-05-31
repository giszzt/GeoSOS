using System;
using DotSpatial.Symbology;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// StatisticalEventArgs
    /// </summary>
    public class StatisticalEventArgs : EventArgs
    {
        #region Private Variables

        private Statistics _stats;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of StatisticalEventArgs
        /// </summary>
        public StatisticalEventArgs(Statistics statistics)
        {
            _stats = statistics;
        }

        #endregion

        #region Methods

        #endregion

        #region Properties

        /// <summary>
        /// Gets the set of statistics related to this event.
        /// </summary>
        public Statistics Statistics
        {
            get { return _stats; }
            protected set { _stats = value; }
        }

        #endregion
    }
}
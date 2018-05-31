using System;
using DotSpatial.Symbology;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// PointSizeRangeEventArgs
    /// </summary>
    public class SizeRangeEventArgs : EventArgs
    {
        #region Private Variables

        private double _endSize;
        private double _startSize;
        private IFeatureSymbolizer _template;
        private bool _useSizeRange;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of PointSizeRangeEventArgs
        /// </summary>
        public SizeRangeEventArgs(double startSize, double endSize, IFeatureSymbolizer template, bool useSizeRange)
        {
            _startSize = startSize;
            _endSize = endSize;
            _template = template;
            _useSizeRange = useSizeRange;
        }

        /// <summary>
        /// Creates a new instance of the PointSizeRangeEventArgs derived from a PointSizeRange
        /// </summary>
        /// <param name="range"></param>
        public SizeRangeEventArgs(FeatureSizeRange range)
        {
            _startSize = range.Start;
            _endSize = range.End;
            _template = range.Symbolizer;
            _useSizeRange = range.UseSizeRange;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the start size of the size range
        /// </summary>
        public double StartSize
        {
            get { return _startSize; }
            protected set { _startSize = value; }
        }

        /// <summary>
        /// Gets the end size of the range
        /// </summary>
        public double EndSize
        {
            get { return _endSize; }
            protected set { _endSize = value; }
        }

        /// <summary>
        /// Gets a boolean indicating whether the size range should be used
        /// </summary>
        public bool UseSizeRange
        {
            get { return _useSizeRange; }
            protected set { _useSizeRange = value; }
        }

        /// <summary>
        /// Gets the symbolizer template that describes everything not covered by a range parameter
        /// </summary>
        public IFeatureSymbolizer Template
        {
            get { return _template; }
            protected set { _template = value; }
        }

        #endregion
    }
}
using System;
using System.Drawing;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// ColorRangeEventArgs
    /// </summary>
    public class ColorRangeEventArgs : EventArgs
    {
        #region Private Variables

        private Color _endColor;
        private bool _hsl;
        private int _hueShift;
        private Color _startColor;
        private bool _useColorRange;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of ColorRangeEventArgs
        /// </summary>
        public ColorRangeEventArgs(Color startColor, Color endColor, int hueShift, bool hsl, bool useRange)
        {
            _startColor = startColor;
            _endColor = endColor;
            _hueShift = hueShift;
            _hsl = hsl;
            _useColorRange = useRange;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the start color
        /// </summary>
        public Color StartColor
        {
            get { return _startColor; }
            protected set { _startColor = value; }
        }

        /// <summary>
        /// Gets the end color
        /// </summary>
        public Color EndColor
        {
            get { return _endColor; }
            protected set { _endColor = value; }
        }

        /// <summary>
        /// Gets or sets the hue shift
        /// </summary>
        public int HueShift
        {
            get { return _hueShift; }
            protected set { _hueShift = value; }
        }

        /// <summary>
        /// Gets a boolean.  If true, the ramp of colors should
        /// be built using the HSL characteristics of the start and
        /// end colors rather than the RGB characteristics
        /// </summary>
        public bool HSL
        {
            get { return _hsl; }
            protected set { _hsl = value; }
        }

        /// <summary>
        /// Gets a boolean, true if this color range should be used.
        /// </summary>
        public bool UseColorRange
        {
            get { return _useColorRange; }
            set { _useColorRange = value; }
        }

        #endregion
    }
}
using System;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// BreakSliderEventArgs
    /// </summary>
    public class BreakSliderEventArgs : EventArgs
    {
        #region Private Variables

        private BreakSlider _slider;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of BreakSliderEventArgs
        /// </summary>
        public BreakSliderEventArgs(BreakSlider slider)
        {
            _slider = slider;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the protected break slider
        /// </summary>
        public BreakSlider Slider
        {
            get { return _slider; }
            protected set { _slider = value; }
        }

        #endregion
    }
}
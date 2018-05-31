using System;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// ButtonState
    /// </summary>
    [Flags]
    public enum ButtonStates
    {
        /// <summary>
        /// This is the default case, wher the button is neither depressed nor illuminated
        /// </summary>
        None = 0,
        /// <summary>
        /// The Button is depressed or pressed down
        /// </summary>
        Depressed = 0x1,
        /// <summary>
        /// The Button is illuminated or lit up
        /// </summary>
        Illuminated = 0x2
    }
}
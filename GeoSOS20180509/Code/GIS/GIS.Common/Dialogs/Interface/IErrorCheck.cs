namespace GIS.Common.Dialogs
{
    public interface IErrorCheck
    {
        #region Properties

        /// <summary>
        /// Boolean, true if there is an error on this device.
        /// </summary>
        bool HasError
        {
            get;
        }

        /// <summary>
        /// Specifies the current error message.
        /// </summary>
        string ErrorMessage
        {
            get;
        }

        /// <summary>
        /// Gets the cleanly formatted name for this control for an error message
        /// </summary>
        string MessageName
        {
            get;
        }

        #endregion
    }
}
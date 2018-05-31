using System.Windows.Forms;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// Contains IWin32Window Owner
    /// </summary>
    public interface IIWin32WindowOwner
    {
        /// <summary>
        /// Allows setting the owner for any dialogs that need to be launched.
        /// </summary>
        IWin32Window Owner { get; set; }  
    }
}
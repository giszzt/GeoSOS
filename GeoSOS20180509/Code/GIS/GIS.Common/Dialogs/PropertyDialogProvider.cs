using System;
using DotSpatial.Symbology;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// PropertyDialogProvider
    /// </summary>
    public class PropertyDialogProvider : IPropertyDialogProvider
    {
        #region Events

        /// <summary>
        /// Fires an event signifying that the item has been updated
        /// </summary>
        public event EventHandler<ChangedObjectEventArgs> ChangesApplied;

        #endregion

        #region Private Variables

        private PropertyDialog _frmDialog;

        #endregion

        #region Constructors

        #endregion

        #region Methods

        /// <summary>
        /// Shows a PropertyGrid Dialog and uses the specified object as the edit copy.
        /// </summary>
        /// <param name="editCopy"></param>
        public void ShowDialog(object editCopy)
        {
            _frmDialog = new PropertyDialog();
            _frmDialog.PropertyGrid.SelectedObject = editCopy;
            _frmDialog.ChangesApplied += frmDialog_ChangesApplied;
            _frmDialog.ShowDialog();
        }

        private void frmDialog_ChangesApplied(object sender, EventArgs e)
        {
            OnChangesApplied(_frmDialog.PropertyGrid.SelectedObject);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the item that was changed by this operation.
        /// </summary>
        public object ChangeItem
        {
            get
            {
                return _frmDialog.PropertyGrid.SelectedObject;
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Fires a the ChangesApplied event
        /// </summary>
        /// <param name="changedItem"></param>
        protected virtual void OnChangesApplied(object changedItem)
        {
            if (ChangesApplied != null) ChangesApplied(this, new ChangedObjectEventArgs(changedItem));
        }

        #endregion
    }
}
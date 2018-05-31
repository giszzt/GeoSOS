using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// This will Pop when user want to rename the field
    /// </summary>
    public partial class RenameFieldDialog : Form
    {
        private readonly List<string> _field = new List<string>();
        private string[] _compinationName = { string.Empty, string.Empty };

        /// <summary>
        /// Constructs a new instance of the form for renaming a field
        /// </summary>
        public RenameFieldDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates a new instance of the form for renaming a field
        /// </summary>
        /// <param name="field"></param>
        public RenameFieldDialog(List<string> field)
        {
            InitializeComponent();
            _field = field;
            foreach (string st in _field)
                cmbField.Items.Add(st);
        }

        /// <summary>
        /// get or set ResultCombination
        /// </summary>
        public string[] ResultCombination
        {
            get { return _compinationName; }
            set { _compinationName = value; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _compinationName[0] = cmbField.SelectedItem as string;
            _compinationName[1] = txtName.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
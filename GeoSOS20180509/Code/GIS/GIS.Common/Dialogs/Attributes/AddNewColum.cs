using System;
using System.Windows.Forms;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// A Dialog for adding new column information to a data table.
    /// </summary>
    public partial class AddNewColum : Form
    {
        /// <summary>
        /// This form will display when the user want to add new fiels in the Table
        /// </summary>
        ///
        private string _name;

        private int _size;
        private Type _type;

        #region properties

        /// <summary>
        /// set or get the Name of the field.
        /// </summary>
        public new string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// set or get the type of the field.
        /// </summary>
        public Type Type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// set or get the size of the field.
        /// </summary>
        public new int Size
        {
            get { return _size; }
            set { _size = value; }
        }

        #endregion

        /// <summary>
        /// A public constructor for creating a new column
        /// </summary>
        public AddNewColum()
        {
            InitializeComponent();
            dialogButtons1.OkClicked += btnOk_Click;
            dialogButtons1.CancelClicked += btnCancel_Click;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            _name = txtName.Text;
            string type = Convert.ToString(cmbType.SelectedItem);
            switch (type)
            {
                case ("Double"):
                    _type = typeof(double);
                    break;
                case ("String"):
                    _type = typeof(string);
                    break;
                case ("int"):
                    _type = typeof(int);
                    break;
                default:
                    _type = typeof(double);
                    break;
            }
            _size = Convert.ToInt32(nudSize.Value);
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }
    }
}
using System;
using System.Windows.Forms;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// This form is strictly designed to show some helpful information about using a calculator.
    /// </summary>
    public partial class CalculatorUserGuide : Form
    {
        /// <summary>
        /// Creates a new instance of the calculator user guide form.
        /// </summary>
        public CalculatorUserGuide()
        {
            InitializeComponent();
        }

        private void CalculatorUserGuideLoad(object sender, EventArgs e)
        {
            richTextBox1.Text = "\n1) use always double click on the Fields or Function or opertationtion in order to add that in the Expression." +
                    "when you clik, It will put space front and back automaticaly( ).\n\n" + "2) use space to use constant or number ex. _3_  or  (_3_*_4_)_-_2  \n\n" +
                    "3) when you use function, you do need to close the bracket.\n\n" +
                    "eg. Abs(-222.34 )\n" + "or  3 * pow(Area, 2 )  this is equal to 3*pow(Area, 2)\n";
        }

        private void BtnCloseClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
using System;
using System.Windows.Forms;
using DotSpatial.Symbology;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// Atrribute Table editor form
    /// </summary>
    public partial class AttributeDialog : Form
    {
       #region Constructor

        /// <summary>
        /// Creates a new instance of the attribute Table editor form
        /// <param name="featureLayer">The feature layer associated with
        /// this instance and displayed in the editor</param>
        /// </summary>
        public AttributeDialog(IFeatureLayer featureLayer)
        {
            InitializeComponent();
            if (featureLayer != null)
            {
                //tableEditorControl1.FeatureLayer = featureLayer;
            }
        }

        #endregion

        #region Event Handlers

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
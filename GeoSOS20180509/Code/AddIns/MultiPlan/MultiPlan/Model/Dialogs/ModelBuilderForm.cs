using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using ESRI.ArcGIS.esriSystem;

namespace MultiPlan
{
    public partial class ModelBuilderForm : Form
    {
        public ModelBuilderForm()
        {
            InitializeComponent();
        }

        public string modelName = string.Empty;
        public string address = string.Empty;

        public void Initial()
        {
            modelBuilderControl1.Initial();
        }

        private void ModelBuilderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            address = modelBuilderControl1.output;
        }


    }
}

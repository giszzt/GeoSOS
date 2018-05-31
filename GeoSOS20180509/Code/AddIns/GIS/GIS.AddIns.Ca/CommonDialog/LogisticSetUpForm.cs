using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GIS.AddIns.Ca.CommonClass;

namespace GIS.AddIns.Ca.CommonDialog
{
    public partial class LogisticSetUpForm : Form
    {
        #region properties

        public LandUseClassificationInfo LandUse { get; set; }
        public string BeginLayerName { get; set; }
        public string EndLayerName { get; set; }
        public string SavePathName { get; set; }
        public List<string> DriveLayerNames { get; set; }
        public int NumberOfSample { get; set; }

        #endregion

        #region constructor
        public LogisticSetUpForm()
        {
            InitializeComponent();
        }
        #endregion


        private void buttonSavePath_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                this.textBoxSavePath.Text = fileDialog.FileName;
            }
        }

        private void buttonOpenStart_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                this.textBoxStartPath.Text = fileDialog.FileName;
            }
        }

        private void buttonOpenEnd_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                this.textBoxEndPath.Text = fileDialog.FileName;
            }
        }

        private void buttonAddDrive_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string filename in fileDialog.FileNames)
                {
                    this.listBoxDriveLayerNames.Items.Add(filename);
                }
            }
        }

        private void buttonRemoveDrive_Click(object sender, EventArgs e)
        {
            int selected = this.listBoxDriveLayerNames.SelectedIndex;
            if (selected != -1)
            {
                this.listBoxDriveLayerNames.Items.RemoveAt(selected);
            }
        }

        private void buttonLandUse_Click(object sender, EventArgs e)
        {
            var form = new LandUseSetUpForm();
            form.ShowDialog();
            this.LandUse = form.LandUse;
        }



        private void buttonSetProperties_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            try
            {
                this.NumberOfSample = int.Parse(this.textBoxNumOfSample.Text);
                this.BeginLayerName = this.textBoxStartPath.Text;
                this.EndLayerName = this.textBoxEndPath.Text;
                this.SavePathName = this.textBoxSavePath.Text;
                string[] list = new string[this.listBoxDriveLayerNames.Items.Count];
                this.listBoxDriveLayerNames.Items.CopyTo(list, 0);
                this.DriveLayerNames = list.ToList<string>();

                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show("异常" + ex.ToString());
                this.DialogResult = DialogResult.No;
            }
        }
    }
}

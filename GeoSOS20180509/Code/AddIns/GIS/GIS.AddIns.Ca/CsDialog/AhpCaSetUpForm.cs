using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GIS.AddIns.Ca.CommonClass;
using GIS.AddIns.Ca.CommonDialog;

namespace GIS.AddIns.Ca.CsDialog
{
    public partial class AhpCaSetUpForm : Form
    {
        #region fields

        private Dictionary<string, string> dictionaryDatasNameAndPath;

        #endregion

        #region properties
        public string BeginLayerName { get; set; }
        public string EndLayerName { get; set; }
        public List<string> DriveLayerNames { get; set; }
        public LandUseClassificationInfo LandUseInfo { get; set; }
        public float[] Weights { get; set; }
        public int SizeOfNeighbour { get; set; }
        public double GlobalFactor { get; set; }
        public double LocalFactor { get; set; }
        public double Alpha { get; set; }
        public int CountOfCity { get; set;}

        #endregion

        #region constructor
        public AhpCaSetUpForm()
        {
            InitializeComponent();
            dictionaryDatasNameAndPath = new Dictionary<string, string>();
        }
        #endregion

        #region event handlers
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
                    string shortFileName = filename.Substring(filename.LastIndexOf('\\') + 1);
                    dictionaryDatasNameAndPath.Add(shortFileName.Substring(0, shortFileName.IndexOf('.')), filename);
                }                
            }
        }

        private void buttonRemoveDrive_Click(object sender, EventArgs e)
        {
            int selected = this.listBoxDriveLayerNames.SelectedIndex;
            if (selected != -1)
            {
                
                string filename = (string)this.listBoxDriveLayerNames.Items[selected];
                string shortFileName = filename.Substring(filename.LastIndexOf('\\') + 1);
                string key = shortFileName.Substring(0, shortFileName.IndexOf('.'));
                dictionaryDatasNameAndPath.Remove(key);
                this.listBoxDriveLayerNames.Items.RemoveAt(selected);

            }
        }

        private void buttonSet_Click(object sender, EventArgs e)
        {
            var form = new LandUseSetUpForm();
            form.ShowDialog();
            this.LandUseInfo = form.LandUse;
        }

        private void buttonAhpMatrix_Click(object sender, EventArgs e)
        {
            
            
            var fileNames = (from pair in this.dictionaryDatasNameAndPath
                                 select pair.Key.ToString()).ToList<string>();
            var form = new AhpSetUpForm(fileNames);
            form.ShowDialog();            
            this.Weights = form.Weights;
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSetProperties_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            try
            {
                this.BeginLayerName = this.textBoxStartPath.Text;
                this.EndLayerName = this.textBoxEndPath.Text;
                this.DriveLayerNames = (from pair in this.dictionaryDatasNameAndPath
                                        select pair.Value.ToString()).ToList<string>();

                this.SizeOfNeighbour = int.Parse(this.textBoxSizeOfNeighbour.Text);
                this.GlobalFactor = double.Parse(this.textBoxGlobalFactor.Text);
                this.LocalFactor = double.Parse(this.textBoxLocalFactor.Text);
                this.Alpha = double.Parse(this.textBoxAlpha.Text);
                this.CountOfCity = int.Parse(this.textBoxCountOfCity.Text);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置不正确\n" + ex.ToString());
                this.DialogResult = DialogResult.No;
            }
            

            
        }
        #endregion
    }
}

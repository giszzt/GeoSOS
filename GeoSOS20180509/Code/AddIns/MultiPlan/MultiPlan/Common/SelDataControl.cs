using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotSpatial.Data;
using DotSpatial.Data.Forms;
using DotSpatial.Controls;
using GeoAPI.Geometries;

namespace MultiPlan
{
    public partial class SelDataControl : UserControl
    {
        string _address;
        IFeatureSet _featureSet;

        /// <summary>
        /// 数据路径
        /// </summary>
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                textBoxAdd.Text = _address;
            }
        }

        /// <summary>
        /// 数据
        /// </summary>
        public IFeatureSet FeatureSet
        {
            get
            {
                return _featureSet;
            }
            set
            {
                _featureSet = value;
            }
        }

        public SelDataControl()
        {
            InitializeComponent();
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.Title = "请选择数据";
            dlg.Filter = "Shp文件(*.shp)|*.shp";
            if (DialogResult.OK != dlg.ShowDialog(this)) return;
            string extName = System.IO.Path.GetExtension(dlg.FileNames[0]).ToLower();
            if (extName == ".shp")
            {
                textBoxAdd.Text = System.IO.Path.GetDirectoryName(dlg.FileNames[0]) + "\\" + System.IO.Path.GetFileName(dlg.FileNames[0]);
            }
        }

        private void textBoxAdd_TextChanged(object sender, EventArgs e)
        {
            _address = textBoxAdd.Text;
        }
    }
}

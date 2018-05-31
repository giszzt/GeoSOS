using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GIS.FrameWork;
using DotSpatial.Controls;
using DotSpatial.Data;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using OxyPlot.Annotations;
using DotSpatial.Symbology;
using ICSharpCode.Core.WinForms;
using FrameWork.Gui;

namespace GIS.AddIns.Statistic.Dialogue
{
    public partial class PlotAttribute : Form
    {
        public PlotAttribute()
        {
            InitializeComponent();
            cmb_tfont.Items.AddRange(new object[] { "Arial", "Segoe UI" ,"Calibri","Times New Roman","Meiryo UI","宋体"});
            cmb_size.Items.AddRange(new object[] {  10, 12, 14, 16, 18, 20,22,24,26,28 });
        }
        IViewContent view1 = WorkbenchSingleton.Workbench.ActiveViewContent;
        
        public void btn_Apply_Click(object sender, EventArgs e)
        {
             
            (view1.Control as MapStatistics)._pm.Title = tbx_title.Text;

            if (tbx_MinX.Text != "")
            {
                (view1.Control as MapStatistics)._XAxis.Minimum = Convert.ToDouble(tbx_MinX.Text);
            }
            if (tbx_MaxX.Text != "")
            {
                (view1.Control as MapStatistics)._XAxis.Maximum = Convert.ToDouble(tbx_MaxX.Text);
            }
            if (tbx_MinY.Text != "")
            {
                (view1.Control as MapStatistics)._YAxis.Minimum = Convert.ToDouble(tbx_MinY.Text);
            }
            if (tbx_MaxY.Text != "")
            {
                (view1.Control as MapStatistics)._YAxis.Maximum = Convert.ToDouble(tbx_MaxY.Text);
            }
            

            (view1.Control as MapStatistics)._XAxis.Title = tbx_axisx.Text;
            (view1.Control as MapStatistics)._YAxis.Title = tbx_axisy.Text;

            (view1.Control as MapStatistics)._pm.InvalidatePlot(true);
            (view1.Control as MapStatistics).plotView1.Refresh();
        }

        //修改标题颜色
        private void btn_color_Click(object sender, EventArgs e)
        {
            
            ColorDialog _cd = new ColorDialog();
            _cd.ShowDialog();
            (view1.Control as MapStatistics)._pm.TitleColor = OxyColor.FromArgb(_cd.Color.A, _cd.Color.R, _cd.Color.G, _cd.Color.B);
        }

        //修改字体
        private void cmb_tfont_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmb_tfont.SelectedIndex)
            {
                case 0:
                    (view1.Control as MapStatistics)._pm.TitleFont = cmb_tfont.Items[0].ToString();
                    break;
                case 1:
                    (view1.Control as MapStatistics)._pm.TitleFont = cmb_tfont.Items[1].ToString();
                    break;
                case 2:
                    (view1.Control as MapStatistics)._pm.TitleFont = cmb_tfont.Items[3].ToString();
                    break;
                case 3:
                    (view1.Control as MapStatistics)._pm.TitleFont = cmb_tfont.Items[3].ToString();
                    break;
                case 4:
                    (view1.Control as MapStatistics)._pm.TitleFont = cmb_tfont.Items[4].ToString();
                    break;
                case 5:
                    (view1.Control as MapStatistics)._pm.TitleFont = cmb_tfont.Items[5].ToString();
                    break;
            }
        }

        //修改字符大小
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmb_size.SelectedIndex)
            {
                case 0:
                    (view1.Control as MapStatistics)._pm.TitleFontSize = Convert.ToDouble (cmb_size.Items[0]);
                    break;
                case 1:
                    (view1.Control as MapStatistics)._pm.TitleFontSize = Convert.ToDouble(cmb_size.Items[1]);
                    break;
                case 2:
                    (view1.Control as MapStatistics)._pm.TitleFontSize = Convert.ToDouble(cmb_size.Items[2]);
                    break;
                case 3:
                    (view1.Control as MapStatistics)._pm.TitleFontSize = Convert.ToDouble(cmb_size.Items[3]);
                    break;
                case 4:
                    (view1.Control as MapStatistics)._pm.TitleFontSize = Convert.ToDouble(cmb_size.Items[4]);
                    break;
                case 5:
                    (view1.Control as MapStatistics)._pm.TitleFontSize = Convert.ToDouble(cmb_size.Items[5]);
                    break;
                case 6:
                    (view1.Control as MapStatistics)._pm.TitleFontSize = Convert.ToDouble(cmb_size.Items[6]);
                    break;
                case 7:
                    (view1.Control as MapStatistics)._pm.TitleFontSize = Convert.ToDouble(cmb_size.Items[7]);
                    break;
                case 8:
                    (view1.Control as MapStatistics)._pm.TitleFontSize = Convert.ToDouble(cmb_size.Items[8]);
                    break;
                case 9:
                    (view1.Control as MapStatistics)._pm.TitleFontSize = Convert.ToDouble(cmb_size.Items[9]);
                    break;
            }
        }


    }
}

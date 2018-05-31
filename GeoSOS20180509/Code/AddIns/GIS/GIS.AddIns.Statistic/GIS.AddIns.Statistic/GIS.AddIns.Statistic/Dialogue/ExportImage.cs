using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.Core;
using FrameWork.Gui;
using OxyPlot;
using OxyPlot.Reporting;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.WindowsForms;
using System.Threading;


namespace GIS.AddIns.Statistic.Dialogue
{
    public partial class ExportImage : Form
    {
        public ExportImage()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(new object[] { 400,600,800,1000,1200,1400,1600});
            comboBox2.Items.AddRange(new object[] { 400, 600, 800, 1000, 1200, 1400, 1600 });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IViewContent view = WorkbenchSingleton.Workbench.ActiveViewContent;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @"C:\";
            saveFileDialog1.Title = "Save Plot Files";
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "png";
            saveFileDialog1.Filter = "PNG Image|*.png|JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            var pngExporter = new PngExporter { Width = Convert.ToInt32(comboBox1.Text), Height = Convert.ToInt32(comboBox2.Text), Background = OxyColors.White };


            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                string fileName = saveFileDialog1.FileName;
                pngExporter.ExportToFile((view.Control as MapStatistics)._pm, fileName);
            }
        }

    }
}

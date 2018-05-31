using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GIS.Common.Dialogs.Console
{
    public partial class Console : UserControl
    {
        public Console()
        {
            InitializeComponent();
        }


        
        private StringBuilder info = new StringBuilder();
        public void addLineToInfo(string line)
        {
            this.info.AppendLine(line);
            this.textBoxInfo.Text = this.info.ToString();
            this.textBoxInfo.SelectionStart = this.textBoxInfo.Text.Length;
            this.textBoxInfo.ScrollToCaret();
        }

        private void toolStripButtonClear_Click(object sender, EventArgs e)
        {
            this.textBoxInfo.Clear();
        }

        private void toolStripButtonExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "文本文件(*.txt)|*.txt";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    TextWriter tw = new StreamWriter(saveFileDialog.FileName);
                    tw.Write(this.textBoxInfo.Text);
                    tw.Close();
                    MessageBox.Show("已完全保存文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }
    }
}

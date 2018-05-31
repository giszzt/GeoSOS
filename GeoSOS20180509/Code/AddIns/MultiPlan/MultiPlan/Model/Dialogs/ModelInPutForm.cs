using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiPlan
{
    public partial class ModelInPutForm : Form
    {
        public ModelParameter para = new ModelParameter();

        public ModelInPutForm()
        {
            InitializeComponent();
            selDataControl.label1.Text = "参数路径";
        }

        public void Initial(string paraInfo, bool flag)
        {
            para.Initial(paraInfo);
            textBox1.Text = para.label;
            selDataControl.Address = para.value;
            //selDataControl.Initial(para.value, false);
        }

        public string GetInfo(ref string label)
        {
            label = para.label;
            return para.label + "|" + para.value;
        }

        private void button1_Click(object sender, EventArgs e)
        {           
            para.label = textBox1.Text;
            para.value = selDataControl.Address;
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }




    }
}

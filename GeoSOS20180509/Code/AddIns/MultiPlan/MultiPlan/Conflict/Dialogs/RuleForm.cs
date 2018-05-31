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
    public partial class RuleForm : Form
    {       
        public RuleForm()
        {
            InitializeComponent();
        }

        private void ConflictForm_Load(object sender, EventArgs e)
        {            
            conflictControl1.Initial();            
        }

    }
}

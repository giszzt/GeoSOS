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
    public partial class PlanForm : Form
    {
        public PlanForm()
        {
            InitializeComponent();
        }

        private void PlanTreeForm_Load(object sender, EventArgs e)
        {
            planControl1.Initial();
        }
    }
}

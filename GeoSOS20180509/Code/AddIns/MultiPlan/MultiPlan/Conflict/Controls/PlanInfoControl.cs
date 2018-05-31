using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiPlan
{
    public partial class PlanInfoControl : UserControl
    {
        public Plan Plan
        {
            get
            {
                _plan.TransField = textBox2.Text;
                _plan.TransFieldAlias = textBox3.Text;
                _plan.Address = selData1.Address;
                _plan.FeatureSet = selData1.FeatureSet;
                return _plan;
            }
            set
            {
                _plan = value;
                textBox2.Text = _plan.TransField;
                textBox3.Text = _plan.TransFieldAlias;
                selData1.Address = _plan.Address;
            }

        }


        Plan _plan = new Plan();


        public PlanInfoControl()
        {
            InitializeComponent();
        }
    }
}

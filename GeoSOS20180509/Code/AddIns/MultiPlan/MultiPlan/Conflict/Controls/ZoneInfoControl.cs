using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GIS.Common.Dialogs;

namespace MultiPlan
{
    public partial class ZoneInfoControl : UserControl
    {
        ControlZone _controlZone = new ControlZone();
        Plan _plan = new Plan();
        PlanTree _planTree = new PlanTree();

        public ZoneInfoControl()
        {
            InitializeComponent();
            selDataControl1.label1.Text = "空间过滤";
            _planTree.Initial();
        }

        public ControlZone Get()
        {
            _controlZone.TransValue = textBox2.Text;
            _controlZone.SQL = textBox3.Text;
            _controlZone.Address = textBox1.Text;
            _controlZone.BSM = textBox4.Text;
            _controlZone.SpatialRegion = selDataControl1.Address;
            _controlZone.Plan = _plan;
            _controlZone.UniqueField = comboBox1.Text;
            return _controlZone;
        }

        public void Set(ControlZone controlZone, Plan plan)
        {
            _plan = plan;
            _controlZone = controlZone;
            textBox2.Text = _controlZone.TransValue;
            textBox3.Text = _controlZone.SQL;
            textBox4.Text = _controlZone.BSM;
            comboBox1.Text = _controlZone.UniqueField;
            selDataControl1.Address = _controlZone.SpatialRegion;
            if (_controlZone.Address != "")
            {
                textBox1.Text = _controlZone.Address;
            }
            else
            {
                textBox1.Text = Config.GetDefaultSet("GKQSet");
            }
           
            List<string> fields = CommonMethod.GetFields(plan.FeatureSet);
            foreach (string item in fields)
            {
                comboBox1.Items.Add(item);
            }

        }

        public void SetAddress(string add)
        {
            textBox1.Text = _controlZone.Address;
        }

        /// <summary>
        /// 模型
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            //ModelBuilderForm mbf = new ModelBuilderForm();
            //mbf.Initial();
            //mbf.ShowDialog();
            //selDataControl1.Initial(mbf.address, false);
        }

        /// <summary>
        /// SQL设置
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            SQLExpressionDialog sqlDialog = new SQLExpressionDialog();
            sqlDialog.Table = _plan.FeatureSet.DataTable;
            sqlDialog.ShowDialog();
            if (sqlDialog.DialogResult == DialogResult.OK)
            {
                textBox3.Text = sqlDialog.Expression;
                _controlZone.SQL = sqlDialog.Expression;
            }
        }

    }
}

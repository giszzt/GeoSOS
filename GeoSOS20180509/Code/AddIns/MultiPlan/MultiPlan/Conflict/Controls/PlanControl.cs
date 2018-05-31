using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DotSpatial.Data;


namespace MultiPlan
{
    public partial class PlanControl : UserControl
    {
        PlanTree _planTree = new PlanTree();
        string dir;
        ControlZone _zone;
        Plan plan;
        string query_Sql = "";
        string nodeLabel;
        string GKQpath = Config.dataDir + "\\MultiPlan.gdb";

        public PlanControl()
        {
            InitializeComponent();
        }

        #region 树操作

        /// <summary>
        /// 单击树节点
        /// </summary>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.panel2.Controls.Clear();
            object o = _planTree.GetSelectedNode(e.Node);
            if (o is Plan)
            {
                PlanInfoControl planInfo = new PlanInfoControl();
                planInfo.Plan = o as Plan;
                this.panel2.Controls.Add(planInfo);
                planInfo.Dock = DockStyle.Fill;
            }
            else
            {
                ZoneInfoControl controlZoneInfo = new ZoneInfoControl();
                controlZoneInfo.Set(o as ControlZone, _planTree.GetSelectedNode(e.Node.Parent) as Plan);
                this.panel2.Controls.Add(controlZoneInfo);
                controlZoneInfo.Dock = DockStyle.Fill;
            }
        }

        /// <summary>
        /// 操作购选设置窗口传来的值
        /// </summary>
        private void SendValue(object sender, EventArgs e)
        {
            query_Sql = sender as string;
        }

        /// <summary>
        /// 树节点双击
        /// </summary>
        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //如果是管控区节点，弹出对话框
            object o = _planTree.GetSelectedNode(e.Node);
            if (o is ControlZone)
            {
                foreach (Control control in this.panel2.Controls)
                {
                    if (control is ZoneInfoControl)
                    {
                        this.panel2.Controls.Remove(control);
                    }
                }
                ZoneInfoControl controlZoneInfo = new ZoneInfoControl();
                controlZoneInfo.Set(o as ControlZone, _planTree.GetSelectedNode(e.Node.Parent) as Plan);

                this.panel2.Controls.Add(controlZoneInfo);
                controlZoneInfo.Dock = DockStyle.Fill;

                ControlZone oo = o as ControlZone;

                Plan plan = _planTree.GetSelectedNode(e.Node.Parent) as Plan;                
                //SQLForm sqlform = new SQLForm();             
                //sqlform.sqlControl1.Load(plan.address,plan.isSDE);
                //sqlform.ShowDialog();
                //if (sqlform.DialogResult == DialogResult.OK)
                //{
                //    oo.sql = sqlform.sql;
                //}

                ////选择类别构建SQL语句
                //configTreeView configForm = new configTreeView(oo.sql);
                //configForm._fieldName = (o as ControlZone).BSM;
                //configForm.GetValue += new EventHandler(SendValue);
                //configForm.ShowDialog(this);
                //oo.sql = query_Sql;

                controlZoneInfo.Set(oo, _planTree.GetSelectedNode(e.Node.Parent) as Plan);
            }
        }

        /// <summary>
        /// 树节点编辑后
        /// </summary>
        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            e.Node.EndEdit(false);
            object o = _planTree.GetSelectedNode(e.Node);
            if (o is Plan)
            {
                if (e.Label != null)
                {
                    _planTree.ModifyPlanName(o as Plan, e.Label);
                }
            }
            else
            {
                if (e.Label != null)
                {
                    _planTree.ModifyZoneName(o as ControlZone, _planTree.GetSelectedNode(e.Node.Parent) as Plan, e.Label);
                }
            }
        }

        /// <summary>
        /// 新增规划节点
        /// </summary>
        private void NewPlanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = "新建规划";
            int i = 1;
            while (CheckNodeText(treeView1.Nodes, name))
            {
                name = "新建规划" + i.ToString();
                i++;
            }

            TreeNode node = new TreeNode();
            node.Text = name;
            treeView1.Nodes.Add(node);

            Plan plan = new Plan();
            plan.Name = name;
            _planTree.AddPlan(plan);
        }

        /// <summary>
        /// 新增管控区节点
        /// </summary>
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = "新建管控区";
            int i = 1;
            while (CheckNodeText(treeView1.Nodes, name))
            {
                name = "新建管控区" + i.ToString();
                i++;
            }

            Plan plan = new Plan();
            ControlZone zone = new ControlZone();

            TreeNode node = new TreeNode();
            node.Text = name;
            if (treeView1.SelectedNode.Parent != null)
            {
                treeView1.SelectedNode.Parent.Nodes.Add(node);
                plan = _planTree.GetSelectedNode(treeView1.SelectedNode.Parent) as Plan;
            }
            else
            {
                treeView1.SelectedNode.Nodes.Add(node);
                plan = _planTree.GetSelectedNode(treeView1.SelectedNode) as Plan;
            }

            zone.Name = name;
            _planTree.AddZone(zone, plan);
        }

        /// <summary>
        /// 检查树中是否存在相同节点
        /// </summary>
        private bool CheckNodeText(TreeNodeCollection nodes, string text)
        {
            bool result = false;

            foreach (TreeNode node in nodes)
            {
                if (result)
                {
                    break;
                }
                else
                {
                    if (string.Equals(node.Text, text))
                    {
                        result = true;
                    }
                    else if (node.Nodes.Count > 0)
                    {
                        result = CheckNodeText(node.Nodes, text);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 注册管控区
        /// </summary>
        private void ResgisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FormDataGet frm = new FormDataGet();
            //SelectDataControl control = new SelectDataControl();
            //control._TreeDoubleClickHander += new TreeDoubleClickHandler(DataTreeDoubleClick);
            //frm.Controls.Add(control);
            //control.Dock = DockStyle.Fill;
            //frm.Show();
        }

        /// <summary>
        /// 编辑节点
        /// </summary>
        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //树节点编辑
            this.treeView1.LabelEdit = true;
            treeView1.SelectedNode.BeginEdit();
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Parent != null)
            {
                _planTree.RemoveZone(_planTree.GetSelectedNode(treeView1.SelectedNode) as ControlZone, _planTree.GetSelectedNode(treeView1.SelectedNode.Parent) as Plan);
                treeView1.Nodes.Remove(treeView1.SelectedNode);
            }
            else
            {
                _planTree.RemovePlan(_planTree.GetSelectedNode(treeView1.SelectedNode) as Plan);
                treeView1.Nodes.Remove(treeView1.SelectedNode);
            }
        }

        #endregion

        /// <summary>
        /// 保存修改
        /// </summary>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                object o = _planTree.GetSelectedNode(treeView1.SelectedNode);
                if (o is Plan)
                {
                    PlanInfoControl planInfo = this.panel2.Controls[0] as PlanInfoControl;
                    Plan plan = planInfo.Plan;
                    _planTree.ModifyPlan(plan);
                }
                else
                {
                    ZoneInfoControl controlZoneInfo = this.panel2.Controls[0] as ZoneInfoControl;
                    ControlZone zone = controlZoneInfo.Get();
                    Plan plan = _planTree.GetSelectedNode(treeView1.SelectedNode.Parent) as Plan;
                    _planTree.ModifyZone(zone, plan);
                }
                _planTree.SaveTree();
                MessageBox.Show("保存成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败！");
            }
        }

        /// <summary>
        /// 生成管控区
        /// </summary>
        private void buttonConvert_Click(object sender, EventArgs e)
        {            
            try
            {
                object o = _planTree.GetSelectedNode(treeView1.SelectedNode);
                if (o is Plan)
                {
                    MessageBox.Show("请选择管控区！");
                }
                else
                {
                    ZoneInfoControl controlZoneInfo = this.panel2.Controls[0] as ZoneInfoControl;                   
                    _zone = controlZoneInfo.Get();
                    _zone.CreateControlZone();
                }
            }
            catch (Exception ex)
            {             
                MessageBox.Show(ex.Message + "创建失败！");
            }
        }

        /// <summary>
        /// 加载XML配置文件
        /// </summary>
        public void Initial()
        {
            _planTree.BuildTree(treeView1);
        }

        /// <summary>
        /// 保存XML配置文件
        /// </summary>
        public void SaveConfig()
        {
            _planTree.SaveTree();
        }

    }
}

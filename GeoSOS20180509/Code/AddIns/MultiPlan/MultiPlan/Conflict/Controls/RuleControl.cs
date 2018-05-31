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
    public partial class RuleControl : UserControl
    {
        RuleTree _ruleTree = new RuleTree();
        PlanTree _planTree = new PlanTree();
        string nodeLabel;
        string CTTBpath = Config.dataDir + "\\MultiPlan.gdb";

        public RuleControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载XML配置文件
        /// </summary>
        public void Initial()
        {
            _ruleTree.BuildTree(treeView1);

            _planTree.Initial();
            //comboBoxPlanA.Items.Clear();
            //comboBoxPlanB.Items.Clear();
            foreach (string item in _planTree.GetAllPlans())
            {
                comboBoxPlanA.Items.Add(item);
                comboBoxPlanB.Items.Add(item);
            }
        }

        /// <summary>
        /// 保存XML配置文件
        /// </summary>
        /// <param Name="sender"></param>
        /// <param Name="e"></param>
        public void SaveConfig()
        {
            _ruleTree.SaveTree();
        }

        #region 树操作

        /// <summary>
        /// 单击树节点
        /// </summary>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                TreeNode node = e.Node;
                if (node.Parent != null)
                {
                    Conflict conflict = _ruleTree.GetSelectedNode(e.Node) as Conflict;
                    if (conflict != null)
                    {
                        if (conflict.ConflictType != string.Empty)
                        {
                            textBoxType.Text = conflict.ConflictType;
                        }
                        else
                        {
                            textBoxType.Text = e.Node.Text;
                        }

                        if ((conflict.ZoneA.Name != null) && (conflict.ZoneA.Name != ""))
                        {
                            comboBoxPlanA.Text = conflict.ZoneA.Name.Split('.')[0];
                            comboBoxZoneA.Text = conflict.ZoneA.Name.Split('.')[1];
                        }
                        if ((conflict.ZoneB.Name != null) && (conflict.ZoneB.Name != ""))
                        {
                            comboBoxPlanB.Text = conflict.ZoneB.Name.Split('.')[0];
                            comboBoxZoneB.Text = conflict.ZoneB.Name.Split('.')[1];
                        }

                        if (conflict.Address != "")
                        {
                            textBox3.Text = conflict.Address;
                        }
                        else
                        {
                            textBox3.Text = Config.GetDefaultSet("CTTBSet");
                        }
                    }
                }
                else
                {
                    comboBoxPlanA.Text = "";
                    comboBoxZoneA.Text = "";
                    comboBoxPlanB.Text = "";
                    comboBoxZoneB.Text = "";
                }
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// 树节点编辑后
        /// </summary>
        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            e.Node.EndEdit(false);
            object o = _ruleTree.GetSelectedNode(e.Node);
            if (o is Rule)
            {
                if (e.Label != null)
                {
                    _ruleTree.ModifyRuleName(o as Rule, e.Label);
                }
            }
            else
            {
                if (e.Label != null)
                {
                    _ruleTree.ModifyConflictName(o as Conflict, _ruleTree.GetSelectedNode(e.Node.Parent) as Rule, e.Label);
                }
            }
        }

        /// <summary>
        /// 新增规划冲突节点
        /// </summary>
        private void NewRuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Name = "新建规划冲突";
            int i = 1;
            while (CheckNodeText(treeView1.Nodes, Name))
            {
                Name = "新建规划冲突" + i.ToString();
                i++;
            }

            TreeNode node = new TreeNode();
            node.Text = Name;
            treeView1.Nodes.Add(node);

            Rule rule = new Rule();
            rule.Name = Name;
            _ruleTree.AddRule(rule);
        }

        /// <summary>
        /// 新增冲突节点
        /// </summary>
        private void NewConflictToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Name = "新建冲突";
            int i = 1;
            while (CheckNodeText(treeView1.Nodes, Name))
            {
                Name = "新建冲突" + i.ToString();
                i++;
            }

            Rule rule = new Rule();
            Conflict conflict = new Conflict();

            TreeNode node = new TreeNode();
            node.Text = Name;
            if (treeView1.SelectedNode.Parent != null)
            {
                treeView1.SelectedNode.Parent.Nodes.Add(node);
                rule = _ruleTree.GetSelectedNode(treeView1.SelectedNode.Parent) as Rule;
            }
            else
            {
                treeView1.SelectedNode.Nodes.Add(node);
                rule = _ruleTree.GetSelectedNode(treeView1.SelectedNode) as Rule;
            }

            conflict.Name = Name;
            _ruleTree.AddConflict(conflict, rule);
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
        /// 注册冲突
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
            this.treeView1.LabelEdit = true;
            treeView1.SelectedNode.BeginEdit();
            nodeLabel = treeView1.SelectedNode.Name;
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Parent != null)
            {
                _ruleTree.RemoveConflict(_ruleTree.GetSelectedNode(treeView1.SelectedNode) as Conflict, _ruleTree.GetSelectedNode(treeView1.SelectedNode.Parent) as Rule);
                treeView1.Nodes.Remove(treeView1.SelectedNode);
            }
            else
            {
                _ruleTree.RemoveRule(_ruleTree.GetSelectedNode(treeView1.SelectedNode) as Rule);
                treeView1.Nodes.Remove(treeView1.SelectedNode);
            }
        }

        #endregion

        /// <summary>
        /// 冲突分析
        /// </summary>
        private void buttonConflict_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            if (node.Parent != null)
            {
                #region 采用系统自带对话框，保存选择路径
               
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Shp files|*.shp";
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Conflict conflict = _ruleTree.GetSelectedNode(node) as Conflict;

                    ///获取Zone
                    PlanTree planTree = new PlanTree();
                    string planNameA = conflict.ZoneA.Name.Split('.')[0];
                    string zoneNameA = conflict.ZoneA.Name.Split('.')[1];
                    conflict.ZoneA = planTree.GetZone(planNameA, zoneNameA);

                    string planNameB = conflict.ZoneB.Name.Split('.')[0];
                    string zoneNameB = conflict.ZoneB.Name.Split('.')[1];
                    conflict.ZoneB = planTree.GetZone(planNameB, zoneNameB);

                    textBox3.Text = dialog.FileName;
                    conflict.Address = dialog.FileName;
                    conflict.ConflictAnalysis();
                    conflict.Name = conflict.Name.Replace(".shp", "");
                    _ruleTree.SaveConflictPath(dialog.FileName, node.Parent.Text, conflict.Name);
                }
                
                #endregion
            }
            else
            {
                MessageBox.Show("请选择冲突！");
            }
        }

        /// <summary>
        /// 保存修改
        /// </summary>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                object o = _ruleTree.GetSelectedNode(treeView1.SelectedNode);
                if (o is Rule)
                {
                }
                else
                {
                    Conflict conflict = _ruleTree.GetSelectedNode(treeView1.SelectedNode) as Conflict;
                    conflict.ZoneA.Name = comboBoxPlanA.Text + "." + comboBoxZoneA.Text;
                    conflict.ZoneB.Name = comboBoxPlanB.Text + "." + comboBoxZoneB.Text;
                    conflict.ConflictType = textBoxType.Text;
                    conflict.Address = textBox3.Text;
                    Rule rule = _ruleTree.GetSelectedNode(treeView1.SelectedNode.Parent) as Rule;
                    _ruleTree.ModifyConflict(conflict, rule);
                }
                _ruleTree.SaveTree();
                MessageBox.Show("保存成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败！");
            }
        }

        /// <summary>
        /// 加载规划A下的管控区
        /// </summary>
        /// <param Name="sender"></param>
        /// <param Name="e"></param>
        private void comboBoxPlanA_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxZoneA.Items.Clear();
            List<string> zones = _planTree.GetAllZonesByPlan(comboBoxPlanA.SelectedItem.ToString());
            foreach (string zone in zones)
            {
                comboBoxZoneA.Items.Add(zone);
            }
        }

        /// <summary>
        /// 加载规划B下的管控区
        /// </summary>
        /// <param Name="sender"></param>
        /// <param Name="e"></param>
        private void comboBoxPlanB_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxZoneB.Items.Clear();
            List<string> zones = _planTree.GetAllZonesByPlan(comboBoxPlanB.SelectedItem.ToString());
            foreach (string zone in zones)
            {
                comboBoxZoneB.Items.Add(zone);
            }
        }

    }
}

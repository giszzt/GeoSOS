namespace MultiPlan
{
    partial class RuleControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewRuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewConflictToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResgisterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxType = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBoxB = new System.Windows.Forms.GroupBox();
            this.comboBoxZoneB = new System.Windows.Forms.ComboBox();
            this.comboBoxPlanB = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBoxA = new System.Windows.Forms.GroupBox();
            this.comboBoxZoneA = new System.Windows.Forms.ComboBox();
            this.comboBoxPlanA = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxBufferA = new System.Windows.Forms.TextBox();
            this.textBoxBSMA = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonConflict = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBoxB.SuspendLayout();
            this.groupBoxA.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.textBoxType);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("宋体", 10F);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(396, 234);
            this.panel1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.treeView1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(396, 192);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "规划冲突";
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("宋体", 10.5F);
            this.treeView1.Location = new System.Drawing.Point(3, 19);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(390, 170);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView1_AfterLabelEdit);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewRuleToolStripMenuItem,
            this.NewConflictToolStripMenuItem,
            this.ResgisterToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.DeleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 114);
            // 
            // NewRuleToolStripMenuItem
            // 
            this.NewRuleToolStripMenuItem.Name = "NewRuleToolStripMenuItem";
            this.NewRuleToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.NewRuleToolStripMenuItem.Text = "新增规划冲突";
            this.NewRuleToolStripMenuItem.Click += new System.EventHandler(this.NewRuleToolStripMenuItem_Click);
            // 
            // NewConflictToolStripMenuItem
            // 
            this.NewConflictToolStripMenuItem.Name = "NewConflictToolStripMenuItem";
            this.NewConflictToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.NewConflictToolStripMenuItem.Text = "新增冲突";
            this.NewConflictToolStripMenuItem.Click += new System.EventHandler(this.NewConflictToolStripMenuItem_Click);
            // 
            // ResgisterToolStripMenuItem
            // 
            this.ResgisterToolStripMenuItem.Name = "ResgisterToolStripMenuItem";
            this.ResgisterToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ResgisterToolStripMenuItem.Text = "注册冲突";
            this.ResgisterToolStripMenuItem.Click += new System.EventHandler(this.ResgisterToolStripMenuItem_Click);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.EditToolStripMenuItem.Text = "编辑";
            this.EditToolStripMenuItem.Click += new System.EventHandler(this.EditToolStripMenuItem_Click);
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.DeleteToolStripMenuItem.Text = "删除";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // textBoxType
            // 
            this.textBoxType.Location = new System.Drawing.Point(70, 200);
            this.textBoxType.Name = "textBoxType";
            this.textBoxType.Size = new System.Drawing.Size(314, 23);
            this.textBoxType.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 205);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "冲突类型";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("宋体", 10F);
            this.panel2.Location = new System.Drawing.Point(0, 234);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(396, 113);
            this.panel2.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBoxB);
            this.panel4.Controls.Add(this.groupBoxA);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(396, 72);
            this.panel4.TabIndex = 2;
            // 
            // groupBoxB
            // 
            this.groupBoxB.Controls.Add(this.comboBoxZoneB);
            this.groupBoxB.Controls.Add(this.comboBoxPlanB);
            this.groupBoxB.Controls.Add(this.label7);
            this.groupBoxB.Controls.Add(this.label8);
            this.groupBoxB.Controls.Add(this.label9);
            this.groupBoxB.Controls.Add(this.textBox1);
            this.groupBoxB.Controls.Add(this.textBox2);
            this.groupBoxB.Controls.Add(this.label10);
            this.groupBoxB.Controls.Add(this.label11);
            this.groupBoxB.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBoxB.Location = new System.Drawing.Point(198, 0);
            this.groupBoxB.Name = "groupBoxB";
            this.groupBoxB.Size = new System.Drawing.Size(198, 72);
            this.groupBoxB.TabIndex = 1;
            this.groupBoxB.TabStop = false;
            this.groupBoxB.Text = "规划";
            // 
            // comboBoxZoneB
            // 
            this.comboBoxZoneB.FormattingEnabled = true;
            this.comboBoxZoneB.Location = new System.Drawing.Point(59, 46);
            this.comboBoxZoneB.Name = "comboBoxZoneB";
            this.comboBoxZoneB.Size = new System.Drawing.Size(118, 21);
            this.comboBoxZoneB.TabIndex = 13;
            // 
            // comboBoxPlanB
            // 
            this.comboBoxPlanB.FormattingEnabled = true;
            this.comboBoxPlanB.Location = new System.Drawing.Point(59, 19);
            this.comboBoxPlanB.Name = "comboBoxPlanB";
            this.comboBoxPlanB.Size = new System.Drawing.Size(118, 21);
            this.comboBoxPlanB.TabIndex = 1;
            this.comboBoxPlanB.SelectedIndexChanged += new System.EventHandler(this.comboBoxPlanB_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 14);
            this.label7.TabIndex = 12;
            this.label7.Text = "规  划";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 14);
            this.label8.TabIndex = 10;
            this.label8.Text = "管控区";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(153, 107);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 14);
            this.label9.TabIndex = 9;
            this.label9.Text = "米";
            this.label9.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(58, 102);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(90, 23);
            this.textBox1.TabIndex = 7;
            this.textBox1.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(58, 73);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(118, 23);
            this.textBox2.TabIndex = 6;
            this.textBox2.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 109);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 14);
            this.label10.TabIndex = 5;
            this.label10.Text = "缓冲区";
            this.label10.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 80);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 14);
            this.label11.TabIndex = 4;
            this.label11.Text = "标识码";
            this.label11.Visible = false;
            // 
            // groupBoxA
            // 
            this.groupBoxA.Controls.Add(this.comboBoxZoneA);
            this.groupBoxA.Controls.Add(this.comboBoxPlanA);
            this.groupBoxA.Controls.Add(this.label6);
            this.groupBoxA.Controls.Add(this.label2);
            this.groupBoxA.Controls.Add(this.label5);
            this.groupBoxA.Controls.Add(this.textBoxBufferA);
            this.groupBoxA.Controls.Add(this.textBoxBSMA);
            this.groupBoxA.Controls.Add(this.label4);
            this.groupBoxA.Controls.Add(this.label3);
            this.groupBoxA.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBoxA.Location = new System.Drawing.Point(0, 0);
            this.groupBoxA.Name = "groupBoxA";
            this.groupBoxA.Size = new System.Drawing.Size(198, 72);
            this.groupBoxA.TabIndex = 0;
            this.groupBoxA.TabStop = false;
            this.groupBoxA.Text = "规划";
            // 
            // comboBoxZoneA
            // 
            this.comboBoxZoneA.FormattingEnabled = true;
            this.comboBoxZoneA.Location = new System.Drawing.Point(59, 46);
            this.comboBoxZoneA.Name = "comboBoxZoneA";
            this.comboBoxZoneA.Size = new System.Drawing.Size(118, 21);
            this.comboBoxZoneA.TabIndex = 13;
            // 
            // comboBoxPlanA
            // 
            this.comboBoxPlanA.FormattingEnabled = true;
            this.comboBoxPlanA.Location = new System.Drawing.Point(59, 19);
            this.comboBoxPlanA.Name = "comboBoxPlanA";
            this.comboBoxPlanA.Size = new System.Drawing.Size(118, 21);
            this.comboBoxPlanA.TabIndex = 1;
            this.comboBoxPlanA.SelectedIndexChanged += new System.EventHandler(this.comboBoxPlanA_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 14);
            this.label6.TabIndex = 12;
            this.label6.Text = "规  划";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 14);
            this.label2.TabIndex = 10;
            this.label2.Text = "管控区";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(153, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 14);
            this.label5.TabIndex = 9;
            this.label5.Text = "米";
            this.label5.Visible = false;
            // 
            // textBoxBufferA
            // 
            this.textBoxBufferA.Location = new System.Drawing.Point(58, 102);
            this.textBoxBufferA.Name = "textBoxBufferA";
            this.textBoxBufferA.Size = new System.Drawing.Size(90, 23);
            this.textBoxBufferA.TabIndex = 7;
            this.textBoxBufferA.Visible = false;
            // 
            // textBoxBSMA
            // 
            this.textBoxBSMA.Location = new System.Drawing.Point(58, 73);
            this.textBoxBSMA.Name = "textBoxBSMA";
            this.textBoxBSMA.Size = new System.Drawing.Size(118, 23);
            this.textBoxBSMA.TabIndex = 6;
            this.textBoxBSMA.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 14);
            this.label4.TabIndex = 5;
            this.label4.Text = "缓冲区";
            this.label4.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "标识码";
            this.label3.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.textBox3);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.buttonSave);
            this.panel3.Controls.Add(this.buttonConflict);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 72);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(396, 41);
            this.panel3.TabIndex = 1;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(72, 8);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(174, 23);
            this.textBox3.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 13);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 14);
            this.label12.TabIndex = 7;
            this.label12.Text = "保存路径";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(254, 8);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(56, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonConflict
            // 
            this.buttonConflict.Font = new System.Drawing.Font("宋体", 10F);
            this.buttonConflict.Location = new System.Drawing.Point(315, 8);
            this.buttonConflict.Name = "buttonConflict";
            this.buttonConflict.Size = new System.Drawing.Size(74, 23);
            this.buttonConflict.TabIndex = 3;
            this.buttonConflict.Text = "冲突分析";
            this.buttonConflict.UseVisualStyleBackColor = true;
            this.buttonConflict.Click += new System.EventHandler(this.buttonConflict_Click);
            // 
            // RuleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "RuleControl";
            this.Size = new System.Drawing.Size(396, 347);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBoxB.ResumeLayout(false);
            this.groupBoxB.PerformLayout();
            this.groupBoxA.ResumeLayout(false);
            this.groupBoxA.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBoxA;
        private System.Windows.Forms.TextBox textBoxBufferA;
        private System.Windows.Forms.TextBox textBoxBSMA;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button buttonConflict;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem NewRuleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewConflictToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.GroupBox groupBoxB;
        private System.Windows.Forms.ComboBox comboBoxZoneB;
        private System.Windows.Forms.ComboBox comboBoxPlanB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBoxZoneA;
        private System.Windows.Forms.ComboBox comboBoxPlanA;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ResgisterToolStripMenuItem;

    }
}

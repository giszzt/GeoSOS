namespace MultiPlan
{
    partial class PlanForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新增规划ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新增管控区ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.生成管控区ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.planControl1 = new MultiPlan.PlanControl();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem,
            this.新增规划ToolStripMenuItem,
            this.新增管控区ToolStripMenuItem,
            this.生成管控区ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 92);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            // 
            // 新增规划ToolStripMenuItem
            // 
            this.新增规划ToolStripMenuItem.Name = "新增规划ToolStripMenuItem";
            this.新增规划ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.新增规划ToolStripMenuItem.Text = "新增规划";
            // 
            // 新增管控区ToolStripMenuItem
            // 
            this.新增管控区ToolStripMenuItem.Name = "新增管控区ToolStripMenuItem";
            this.新增管控区ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.新增管控区ToolStripMenuItem.Text = "新增管控区";
            // 
            // 生成管控区ToolStripMenuItem
            // 
            this.生成管控区ToolStripMenuItem.Name = "生成管控区ToolStripMenuItem";
            this.生成管控区ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.生成管控区ToolStripMenuItem.Text = "生成管控区";
            // 
            // planControl1
            // 
            this.planControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.planControl1.Location = new System.Drawing.Point(0, 0);
            this.planControl1.Name = "planControl1";
            this.planControl1.Size = new System.Drawing.Size(395, 439);
            this.planControl1.TabIndex = 1;
            // 
            // PlanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 439);
            this.Controls.Add(this.planControl1);
            this.Name = "PlanForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "管控模型";
            this.Load += new System.EventHandler(this.PlanTreeForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新增规划ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新增管控区ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 生成管控区ToolStripMenuItem;
        private PlanControl planControl1;
    }
}
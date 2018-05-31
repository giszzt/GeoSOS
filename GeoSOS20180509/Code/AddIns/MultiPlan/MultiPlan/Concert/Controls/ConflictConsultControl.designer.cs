namespace MultiPlan
{
    partial class ConflictConsultControl
    {
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox xmlComboBox;
        private System.Windows.Forms.Label label1;

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.xmlComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBoxA = new System.Windows.Forms.GroupBox();
            this.mapA = new DotSpatial.Controls.Map();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBoxConflict = new System.Windows.Forms.GroupBox();
            this.mapConflict = new DotSpatial.Controls.Map();
            this.panel5 = new System.Windows.Forms.Panel();
            this.groupBoxB = new System.Windows.Forms.GroupBox();
            this.mapB = new DotSpatial.Controls.Map();
            this.panel6 = new System.Windows.Forms.Panel();
            this.groupBoxNow = new System.Windows.Forms.GroupBox();
            this.mapNow = new DotSpatial.Controls.Map();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBoxA.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBoxConflict.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBoxB.SuspendLayout();
            this.panel6.SuspendLayout();
            this.groupBoxNow.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.xmlComboBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(467, 40);
            this.panel1.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(274, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "关联冲突";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // xmlComboBox
            // 
            this.xmlComboBox.FormattingEnabled = true;
            this.xmlComboBox.Location = new System.Drawing.Point(73, 10);
            this.xmlComboBox.Name = "xmlComboBox";
            this.xmlComboBox.Size = new System.Drawing.Size(183, 20);
            this.xmlComboBox.TabIndex = 1;
            this.xmlComboBox.SelectedIndexChanged += new System.EventHandler(this.SelectConflict);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "冲突名称";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel6, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 49);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(458, 197);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBoxA);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(223, 92);
            this.panel2.TabIndex = 0;
            // 
            // groupBoxA
            // 
            this.groupBoxA.Controls.Add(this.mapA);
            this.groupBoxA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxA.Location = new System.Drawing.Point(0, 0);
            this.groupBoxA.Name = "groupBoxA";
            this.groupBoxA.Size = new System.Drawing.Size(223, 92);
            this.groupBoxA.TabIndex = 1;
            this.groupBoxA.TabStop = false;
            this.groupBoxA.Text = "图层A";
            // 
            // mapA
            // 
            this.mapA.AllowDrop = true;
            this.mapA.BackColor = System.Drawing.Color.White;
            this.mapA.CollectAfterDraw = false;
            this.mapA.CollisionDetection = false;
            this.mapA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapA.ExtendBuffer = false;
            this.mapA.FunctionMode = DotSpatial.Controls.FunctionMode.None;
            this.mapA.IsBusy = false;
            this.mapA.IsZoomedToMaxExtent = false;
            this.mapA.Legend = null;
            this.mapA.Location = new System.Drawing.Point(3, 17);
            this.mapA.Name = "mapA";
            this.mapA.ProgressHandler = null;
            this.mapA.ProjectionModeDefine = DotSpatial.Controls.ActionMode.Prompt;
            this.mapA.ProjectionModeReproject = DotSpatial.Controls.ActionMode.Prompt;
            this.mapA.RedrawLayersWhileResizing = false;
            this.mapA.SelectionEnabled = true;
            this.mapA.Size = new System.Drawing.Size(217, 72);
            this.mapA.TabIndex = 0;
            this.mapA.ZoomOutFartherThanMaxExtent = true;
            this.mapA.Click += new System.EventHandler(this.mapA_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBoxConflict);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(232, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(223, 92);
            this.panel3.TabIndex = 1;
            // 
            // groupBoxConflict
            // 
            this.groupBoxConflict.Controls.Add(this.mapConflict);
            this.groupBoxConflict.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxConflict.Location = new System.Drawing.Point(0, 0);
            this.groupBoxConflict.Name = "groupBoxConflict";
            this.groupBoxConflict.Size = new System.Drawing.Size(223, 92);
            this.groupBoxConflict.TabIndex = 2;
            this.groupBoxConflict.TabStop = false;
            this.groupBoxConflict.Text = "冲突图层";
            // 
            // mapConflict
            // 
            this.mapConflict.AllowDrop = true;
            this.mapConflict.BackColor = System.Drawing.Color.White;
            this.mapConflict.CollectAfterDraw = false;
            this.mapConflict.CollisionDetection = false;
            this.mapConflict.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapConflict.ExtendBuffer = false;
            this.mapConflict.FunctionMode = DotSpatial.Controls.FunctionMode.None;
            this.mapConflict.IsBusy = false;
            this.mapConflict.IsZoomedToMaxExtent = false;
            this.mapConflict.Legend = null;
            this.mapConflict.Location = new System.Drawing.Point(3, 17);
            this.mapConflict.Name = "mapConflict";
            this.mapConflict.ProgressHandler = null;
            this.mapConflict.ProjectionModeDefine = DotSpatial.Controls.ActionMode.Prompt;
            this.mapConflict.ProjectionModeReproject = DotSpatial.Controls.ActionMode.Prompt;
            this.mapConflict.RedrawLayersWhileResizing = false;
            this.mapConflict.SelectionEnabled = true;
            this.mapConflict.Size = new System.Drawing.Size(217, 72);
            this.mapConflict.TabIndex = 0;
            this.mapConflict.ZoomOutFartherThanMaxExtent = true;
            this.mapConflict.ViewExtentsChanged += new System.EventHandler<DotSpatial.Data.ExtentArgs>(this.mapConflict_ViewExtentChanged);
            this.mapConflict.Click += new System.EventHandler(this.mapConflict_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.groupBoxB);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 101);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(223, 93);
            this.panel5.TabIndex = 3;
            // 
            // groupBoxB
            // 
            this.groupBoxB.Controls.Add(this.mapB);
            this.groupBoxB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxB.Location = new System.Drawing.Point(0, 0);
            this.groupBoxB.Name = "groupBoxB";
            this.groupBoxB.Size = new System.Drawing.Size(223, 93);
            this.groupBoxB.TabIndex = 4;
            this.groupBoxB.TabStop = false;
            this.groupBoxB.Text = "图层B";
            // 
            // mapB
            // 
            this.mapB.AllowDrop = true;
            this.mapB.BackColor = System.Drawing.Color.White;
            this.mapB.CollectAfterDraw = false;
            this.mapB.CollisionDetection = false;
            this.mapB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapB.ExtendBuffer = false;
            this.mapB.FunctionMode = DotSpatial.Controls.FunctionMode.None;
            this.mapB.IsBusy = false;
            this.mapB.IsZoomedToMaxExtent = false;
            this.mapB.Legend = null;
            this.mapB.Location = new System.Drawing.Point(3, 17);
            this.mapB.Name = "mapB";
            this.mapB.ProgressHandler = null;
            this.mapB.ProjectionModeDefine = DotSpatial.Controls.ActionMode.Prompt;
            this.mapB.ProjectionModeReproject = DotSpatial.Controls.ActionMode.Prompt;
            this.mapB.RedrawLayersWhileResizing = false;
            this.mapB.SelectionEnabled = true;
            this.mapB.Size = new System.Drawing.Size(217, 73);
            this.mapB.TabIndex = 0;
            this.mapB.ZoomOutFartherThanMaxExtent = true;
            this.mapB.Click += new System.EventHandler(this.mapB_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.groupBoxNow);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(232, 101);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(223, 93);
            this.panel6.TabIndex = 4;
            // 
            // groupBoxNow
            // 
            this.groupBoxNow.Controls.Add(this.mapNow);
            this.groupBoxNow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxNow.Location = new System.Drawing.Point(0, 0);
            this.groupBoxNow.Name = "groupBoxNow";
            this.groupBoxNow.Size = new System.Drawing.Size(223, 93);
            this.groupBoxNow.TabIndex = 6;
            this.groupBoxNow.TabStop = false;
            this.groupBoxNow.Text = "现状";
            // 
            // mapNow
            // 
            this.mapNow.AllowDrop = true;
            this.mapNow.BackColor = System.Drawing.Color.White;
            this.mapNow.CollectAfterDraw = false;
            this.mapNow.CollisionDetection = false;
            this.mapNow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapNow.ExtendBuffer = false;
            this.mapNow.FunctionMode = DotSpatial.Controls.FunctionMode.None;
            this.mapNow.IsBusy = false;
            this.mapNow.IsZoomedToMaxExtent = false;
            this.mapNow.Legend = null;
            this.mapNow.Location = new System.Drawing.Point(3, 17);
            this.mapNow.Name = "mapNow";
            this.mapNow.ProgressHandler = null;
            this.mapNow.ProjectionModeDefine = DotSpatial.Controls.ActionMode.Prompt;
            this.mapNow.ProjectionModeReproject = DotSpatial.Controls.ActionMode.Prompt;
            this.mapNow.RedrawLayersWhileResizing = false;
            this.mapNow.SelectionEnabled = true;
            this.mapNow.Size = new System.Drawing.Size(217, 73);
            this.mapNow.TabIndex = 0;
            this.mapNow.ZoomOutFartherThanMaxExtent = true;
            this.mapNow.Click += new System.EventHandler(this.mapNow_Click);
            // 
            // ConflictConsultControl
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Name = "ConflictConsultControl";
            this.Size = new System.Drawing.Size(467, 251);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBoxA.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBoxConflict.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.groupBoxB.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.groupBoxNow.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBoxA;
        private DotSpatial.Controls.Map mapA;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBoxConflict;
        private DotSpatial.Controls.Map mapConflict;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.GroupBox groupBoxB;
        private DotSpatial.Controls.Map mapB;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.GroupBox groupBoxNow;
        private DotSpatial.Controls.Map mapNow;
    }
}

namespace GIS.Common.Dialogs
{
    partial class TwoMapsControl
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.leftMap = new DotSpatial.Controls.Map();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rightMap = new DotSpatial.Controls.Map();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(528, 336);
            this.splitContainer1.SplitterDistance = 257;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.leftMap);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 336);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // leftMap
            // 
            this.leftMap.AllowDrop = true;
            this.leftMap.BackColor = System.Drawing.Color.White;
            this.leftMap.CollectAfterDraw = false;
            this.leftMap.CollisionDetection = false;
            this.leftMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftMap.ExtendBuffer = false;
            this.leftMap.FunctionMode = DotSpatial.Controls.FunctionMode.None;
            this.leftMap.IsBusy = false;
            this.leftMap.IsZoomedToMaxExtent = false;
            this.leftMap.Legend = null;
            this.leftMap.Location = new System.Drawing.Point(3, 17);
            this.leftMap.Name = "leftMap";
            this.leftMap.ProgressHandler = null;
            this.leftMap.ProjectionModeDefine = DotSpatial.Controls.ActionMode.Prompt;
            this.leftMap.ProjectionModeReproject = DotSpatial.Controls.ActionMode.Prompt;
            this.leftMap.RedrawLayersWhileResizing = false;
            this.leftMap.SelectionEnabled = true;
            this.leftMap.Size = new System.Drawing.Size(251, 316);
            this.leftMap.TabIndex = 0;
            this.leftMap.ZoomOutFartherThanMaxExtent = false;
            this.leftMap.ViewExtentsChanged += new System.EventHandler<DotSpatial.Data.ExtentArgs>(this.leftMap_ViewExtentsChanged);
            this.leftMap.Click += new System.EventHandler(this.leftMap_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rightMap);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(267, 336);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // rightMap
            // 
            this.rightMap.AllowDrop = true;
            this.rightMap.BackColor = System.Drawing.Color.White;
            this.rightMap.CollectAfterDraw = false;
            this.rightMap.CollisionDetection = false;
            this.rightMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightMap.ExtendBuffer = false;
            this.rightMap.FunctionMode = DotSpatial.Controls.FunctionMode.None;
            this.rightMap.IsBusy = false;
            this.rightMap.IsZoomedToMaxExtent = false;
            this.rightMap.Legend = null;
            this.rightMap.Location = new System.Drawing.Point(3, 17);
            this.rightMap.Name = "rightMap";
            this.rightMap.ProgressHandler = null;
            this.rightMap.ProjectionModeDefine = DotSpatial.Controls.ActionMode.Prompt;
            this.rightMap.ProjectionModeReproject = DotSpatial.Controls.ActionMode.Prompt;
            this.rightMap.RedrawLayersWhileResizing = false;
            this.rightMap.SelectionEnabled = true;
            this.rightMap.Size = new System.Drawing.Size(261, 316);
            this.rightMap.TabIndex = 0;
            this.rightMap.ZoomOutFartherThanMaxExtent = false;
            this.rightMap.ViewExtentsChanged += new System.EventHandler<DotSpatial.Data.ExtentArgs>(this.rightMap_ViewExtentsChanged);
            this.rightMap.Click += new System.EventHandler(this.rightMap_Click);
            // 
            // TwoMapsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "TwoMapsControl";
            this.Size = new System.Drawing.Size(528, 336);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DotSpatial.Controls.Map leftMap;
        private System.Windows.Forms.GroupBox groupBox2;
        private DotSpatial.Controls.Map rightMap;
    }
}

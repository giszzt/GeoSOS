namespace GIS.AddIns.Statistic
{
    partial class MapStatistics
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
            this.plotView1 = new OxyPlot.WindowsForms.PlotView();
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
            this.splitContainer1.BackColor = System.Drawing.Color.White;
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
            this.splitContainer1.Size = new System.Drawing.Size(1235, 645);
            this.splitContainer1.SplitterDistance = 598;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.leftMap);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(598, 645);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Map";
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
            this.leftMap.Size = new System.Drawing.Size(592, 625);
            this.leftMap.TabIndex = 0;
            this.leftMap.ZoomOutFartherThanMaxExtent = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.plotView1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(633, 645);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PlotView";
            // 
            // plotView1
            // 
            this.plotView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotView1.Location = new System.Drawing.Point(3, 17);
            this.plotView1.Name = "plotView1";
            this.plotView1.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotView1.Size = new System.Drawing.Size(627, 625);
            this.plotView1.TabIndex = 0;
            this.plotView1.Text = "plotView1";
            this.plotView1.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotView1.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotView1.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // MapStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "MapStatistics";
            this.Size = new System.Drawing.Size(1235, 645);
            this.Load += new System.EventHandler(this.MapStatistics_Load);
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
        public OxyPlot.WindowsForms.PlotView plotView1;
        //public OxyPlot.WindowsForms.PlotView plotView1;
     
    }
}

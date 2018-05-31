namespace MultiPlan
{
    partial class ModelBuilderControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelBuilderControl));
            Dataweb.NShape.RoleBasedSecurityManager roleBasedSecurityManager3 = new Dataweb.NShape.RoleBasedSecurityManager();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.display1 = new Dataweb.NShape.WinFormsUI.Display(); 
            this.diagramSetController1 = new Dataweb.NShape.Controllers.DiagramSetController();
            this.project1 = new Dataweb.NShape.Project(this.components);
            this.cachedRepository1 = new Dataweb.NShape.Advanced.CachedRepository();
            this.xmlStore1 = new Dataweb.NShape.XmlStore();
            this.panel2 = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.openButton = new System.Windows.Forms.ToolStripButton();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.newButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.selectButton = new System.Windows.Forms.ToolStripButton();
            this.deleteButton = new System.Windows.Forms.ToolStripButton();
            this.clearButton = new System.Windows.Forms.ToolStripButton();
            this.modifyButton = new System.Windows.Forms.ToolStripButton();
            this.zoomInButton = new System.Windows.Forms.ToolStripButton();
            this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
            this.gridButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.inputButton = new System.Windows.Forms.ToolStripButton();
            this.outputButton = new System.Windows.Forms.ToolStripButton();
            this.operateButton = new System.Windows.Forms.ToolStripButton();
            this.connectButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.validateButton = new System.Windows.Forms.ToolStripButton();
            this.excecuteButton = new System.Windows.Forms.ToolStripButton();
            this.toolSetController1 = new Dataweb.NShape.Controllers.ToolSetController();
            this.toolSetListViewPresenter1 = new Dataweb.NShape.WinFormsUI.ToolSetListViewPresenter(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(553, 489);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 28);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(553, 461);
            this.panel4.TabIndex = 2;
            this.panel4.Controls.Add(this.display1);
            // 
            // display1
            // 
            this.display1.AllowDrop = true;
            this.display1.BackColorGradient = System.Drawing.SystemColors.Control;
            this.display1.DiagramSetController = this.diagramSetController1;
            this.display1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.display1.GridColor = System.Drawing.Color.Gainsboro;
            this.display1.GridSize = 19;
            this.display1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.display1.Location = new System.Drawing.Point(0, 0);
            this.display1.Name = "display1";
            this.display1.PropertyController = null;
            this.display1.SelectionHilightColor = System.Drawing.Color.Firebrick;
            this.display1.SelectionInactiveColor = System.Drawing.Color.Gray;
            this.display1.SelectionInteriorColor = System.Drawing.Color.WhiteSmoke;
            this.display1.SelectionNormalColor = System.Drawing.Color.DarkGreen;
            this.display1.Size = new System.Drawing.Size(392, 447);
            this.display1.TabIndex = 0;
            this.display1.ToolPreviewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(119)))), ((int)(((byte)(136)))), ((int)(((byte)(153)))));
            this.display1.ToolPreviewColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.display1.ShapeDoubleClick += new System.EventHandler<Dataweb.NShape.Controllers.DiagramPresenterShapeClickEventArgs>(this.display1_ShapeDoubleClick);
            this.display1.ShapesInserted += new System.EventHandler<Dataweb.NShape.Controllers.DiagramPresenterShapesEventArgs>(this.display1_ShapesInserted);
            this.display1.ShapesRemoved += new System.EventHandler<Dataweb.NShape.Controllers.DiagramPresenterShapesEventArgs>(this.display1_ShapesRemoved);
            this.display1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.display1_KeyDown);
            this.display1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.display1_MouseDown);
            // 
            // diagramSetController1
            // 
            this.diagramSetController1.ActiveTool = null;
            this.diagramSetController1.Project = this.project1;
            // 
            // project1
            // 
            this.project1.AutoGenerateTemplates = false;
            this.project1.Description = null;
            this.project1.LibrarySearchPaths = ((System.Collections.Generic.IList<string>)(resources.GetObject("project1.LibrarySearchPaths")));
            this.project1.Name = null;
            this.project1.Repository = this.cachedRepository1;
            roleBasedSecurityManager3.CurrentRole = Dataweb.NShape.StandardRole.Administrator;
            roleBasedSecurityManager3.CurrentRoleName = "Administrator";
            this.project1.SecurityManager = roleBasedSecurityManager3;
            // 
            // cachedRepository1
            // 
            this.cachedRepository1.ProjectName = null;
            this.cachedRepository1.Store = this.xmlStore1;
            this.cachedRepository1.Version = 0;
            this.cachedRepository1.ConnectionInserted += new System.EventHandler<Dataweb.NShape.RepositoryShapeConnectionEventArgs>(this.cachedRepository1_ConnectionInserted);
            // 
            // xmlStore1
            // 
            this.xmlStore1.DesignFileName = "";
            this.xmlStore1.DirectoryName = "";
            this.xmlStore1.FileExtension = ".xml";
            this.xmlStore1.ImageLocation = Dataweb.NShape.XmlStore.ImageFileLocation.Directory;
            this.xmlStore1.ProjectFilePath = ".xml";
            this.xmlStore1.ProjectName = "";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.listView1);
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(553, 28);
            this.panel2.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(473, 3);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.ShowItemToolTips = true;
            this.listView1.Size = new System.Drawing.Size(67, 20);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openButton,
            this.saveButton,
            this.newButton,
            this.toolStripSeparator1,
            this.selectButton,
            this.deleteButton,
            this.clearButton,
            this.modifyButton,
            this.zoomInButton,
            this.zoomOutButton,
            this.gridButton,
            this.toolStripSeparator2,
            this.inputButton,
            this.outputButton,
            this.operateButton,
            this.connectButton,
            this.toolStripSeparator3,
            this.validateButton,
            this.excecuteButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(553, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // openButton
            // 
            this.openButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openButton.Image = ((System.Drawing.Image)(resources.GetObject("openButton.Image")));
            this.openButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(23, 22);
            this.openButton.Text = "打开";
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveButton.Image = ((System.Drawing.Image)(resources.GetObject("saveButton.Image")));
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(23, 22);
            this.saveButton.Text = "保存";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // newButton
            // 
            this.newButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newButton.Image = ((System.Drawing.Image)(resources.GetObject("newButton.Image")));
            this.newButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(23, 22);
            this.newButton.Text = "新建";
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // selectButton
            // 
            this.selectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.selectButton.Image = ((System.Drawing.Image)(resources.GetObject("selectButton.Image")));
            this.selectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(23, 22);
            this.selectButton.Text = "选择";
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteButton.Image")));
            this.deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(23, 22);
            this.deleteButton.Text = "删除";
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearButton.Image = ((System.Drawing.Image)(resources.GetObject("clearButton.Image")));
            this.clearButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(23, 22);
            this.clearButton.Text = "清空";
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // modifyButton
            // 
            this.modifyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.modifyButton.Image = ((System.Drawing.Image)(resources.GetObject("modifyButton.Image")));
            this.modifyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.modifyButton.Name = "modifyButton";
            this.modifyButton.Size = new System.Drawing.Size(23, 22);
            this.modifyButton.Text = "修改";
            this.modifyButton.Click += new System.EventHandler(this.modifyButton_Click);
            // 
            // zoomInButton
            // 
            this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomInButton.Image = ((System.Drawing.Image)(resources.GetObject("zoomInButton.Image")));
            this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomInButton.Name = "zoomInButton";
            this.zoomInButton.Size = new System.Drawing.Size(23, 22);
            this.zoomInButton.Text = "放大";
            this.zoomInButton.Click += new System.EventHandler(this.zoomInButton_Click);
            // 
            // zoomOutButton
            // 
            this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomOutButton.Image = ((System.Drawing.Image)(resources.GetObject("zoomOutButton.Image")));
            this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomOutButton.Name = "zoomOutButton";
            this.zoomOutButton.Size = new System.Drawing.Size(23, 22);
            this.zoomOutButton.Text = "缩小";
            this.zoomOutButton.Click += new System.EventHandler(this.zoomOutButton_Click);
            // 
            // gridButton
            // 
            this.gridButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.gridButton.Image = ((System.Drawing.Image)(resources.GetObject("gridButton.Image")));
            this.gridButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.gridButton.Name = "gridButton";
            this.gridButton.Size = new System.Drawing.Size(23, 22);
            this.gridButton.Text = "网格";
            this.gridButton.Click += new System.EventHandler(this.gridButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // inputButton
            // 
            this.inputButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.inputButton.Image = ((System.Drawing.Image)(resources.GetObject("inputButton.Image")));
            this.inputButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.inputButton.Name = "inputButton";
            this.inputButton.Size = new System.Drawing.Size(23, 22);
            this.inputButton.Text = "输入因子";
            this.inputButton.Click += new System.EventHandler(this.inputButton_Click);
            // 
            // outputButton
            // 
            this.outputButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.outputButton.Image = ((System.Drawing.Image)(resources.GetObject("outputButton.Image")));
            this.outputButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.outputButton.Name = "outputButton";
            this.outputButton.Size = new System.Drawing.Size(23, 22);
            this.outputButton.Text = "输出因子";
            this.outputButton.Click += new System.EventHandler(this.outputButton_Click);
            // 
            // operateButton
            // 
            this.operateButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.operateButton.Image = ((System.Drawing.Image)(resources.GetObject("operateButton.Image")));
            this.operateButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.operateButton.Name = "operateButton";
            this.operateButton.Size = new System.Drawing.Size(23, 22);
            this.operateButton.Text = "操作因子";
            this.operateButton.Click += new System.EventHandler(this.operateButton_Click);
            // 
            // connectButton
            // 
            this.connectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.connectButton.Image = ((System.Drawing.Image)(resources.GetObject("connectButton.Image")));
            this.connectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(23, 22);
            this.connectButton.Text = "连接";
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // validateButton
            // 
            this.validateButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.validateButton.Image = ((System.Drawing.Image)(resources.GetObject("validateButton.Image")));
            this.validateButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.validateButton.Name = "validateButton";
            this.validateButton.Size = new System.Drawing.Size(23, 22);
            this.validateButton.Text = "有效性检查";
            this.validateButton.Visible = false;
            // 
            // excecuteButton
            // 
            this.excecuteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.excecuteButton.Image = ((System.Drawing.Image)(resources.GetObject("excecuteButton.Image")));
            this.excecuteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.excecuteButton.Name = "excecuteButton";
            this.excecuteButton.Size = new System.Drawing.Size(23, 22);
            this.excecuteButton.Text = "执行";
            this.excecuteButton.Click += new System.EventHandler(this.excecuteButton_Click);
            // 
            // toolSetController1
            // 
            this.toolSetController1.DiagramSetController = this.diagramSetController1;
            // 
            // toolSetListViewPresenter1
            // 
            this.toolSetListViewPresenter1.HideDeniedMenuItems = false;
            this.toolSetListViewPresenter1.ListView = this.listView1;
            this.toolSetListViewPresenter1.ShowDefaultContextMenu = true;
            this.toolSetListViewPresenter1.ToolSetController = this.toolSetController1;
            // 
            // ModelBuilderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ModelBuilderControl";
            this.Size = new System.Drawing.Size(553, 489);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private Dataweb.NShape.WinFormsUI.Display display1;
        private Dataweb.NShape.Controllers.DiagramSetController diagramSetController1;
        private Dataweb.NShape.Project project1;
        private Dataweb.NShape.Advanced.CachedRepository cachedRepository1;
        private Dataweb.NShape.XmlStore xmlStore1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton openButton;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton zoomInButton;
        private System.Windows.Forms.ToolStripButton zoomOutButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton inputButton;
        private System.Windows.Forms.ToolStripButton outputButton;
        private System.Windows.Forms.ToolStripButton operateButton;
        private System.Windows.Forms.ToolStripButton connectButton;
        private System.Windows.Forms.ToolStripButton newButton;
        private System.Windows.Forms.ListView listView1;
        private Dataweb.NShape.Controllers.ToolSetController toolSetController1;
        private Dataweb.NShape.WinFormsUI.ToolSetListViewPresenter toolSetListViewPresenter1;
        private System.Windows.Forms.ToolStripButton selectButton;
        private System.Windows.Forms.ToolStripButton deleteButton;
        private System.Windows.Forms.ToolStripButton clearButton;
        private System.Windows.Forms.ToolStripButton modifyButton;
        private System.Windows.Forms.ToolStripButton gridButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton validateButton;
        private System.Windows.Forms.ToolStripButton excecuteButton;
    }
}

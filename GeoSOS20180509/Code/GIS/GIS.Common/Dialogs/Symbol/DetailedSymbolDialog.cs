using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Symbology;
using ICSharpCode.Core;

namespace GIS.Common.Dialogs
{
    public partial class DetailedSymbolDialog : Form
    {
        #region Events

        /// <summary>
        /// Occurs whenever the apply changes button is clicked, or else when the ok button is clicked.
        /// </summary>
        public event EventHandler ChangesApplied;

        # endregion

        private IMap _map = GIS.FrameWork.Application.App.Map;
        private Panel pointPanel;
        private Panel linePanel;
        private Panel polygonPanel;
        private Panel bufferPanel;
        private DetailedPointSymbolControl pointSymbolControl;
        private DetailedLineSymbolControl lineSymbolControl;
        private DetailedPolygonSymbolControl polygonSymbolControl;
        private DetailedPolygonSymbolControl bufferSymbolControl;
        private DialogButtons dialogButtons;

        #region Constructors

        public DetailedSymbolDialog()
        {
            InitializeComponent();
            Configure();
        }

        # endregion

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void Configure()
        {
            ConfigureSymbolDialog();
            ConfigureDialogButtons();
            ConfigureTabControl();
        }

        private void ConfigureSymbolDialog()
        {    
            this.ClientSize = new System.Drawing.Size(705, 520);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DetailedSymbolDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
        }

        private void ConfigureTabControl()
        {
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            ConfigurePointPage();
            ConfigureLinePage();
            ConfigurePolygonPage();
            ConfigureBufferPage();
        }

        private void ConfigureDialogButtons()
        {
            this.dialogButtons = new GIS.Common.Dialogs.DialogButtons();
            this.dialogButtons.Name = "dialogButtons";
            this.dialogButtons.TabIndex = 1;
            this.dialogButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Controls.Add(this.dialogButtons);
            dialogButtons.OkClicked += btnOk_Click;
            dialogButtons.CancelClicked += btnCancel_Click;
            dialogButtons.ApplyClicked += btnApply_Click;
        }

        private void ConfigurePointPage()
        {
            foreach (ILayer item in _map.MapFrame.DrawingLayers)
            {
                if (item.LegendText == "Point")
                {
                    string pageName = StringParser.Parse("${res:GIS.Common.Dialogs.Symbol.PointSymbolDialog}");
                    this.pointPanel = new TabPage(pageName);
                    this.pointSymbolControl = new DetailedPointSymbolControl((item as IPointLayer).Symbolizer);
                    this.pointPanel.SuspendLayout();
                    this.SuspendLayout();
                    // 
                    // pointPanel
                    // 
                    this.pointPanel.BackColor = System.Drawing.SystemColors.Control;
                    this.pointPanel.Controls.Add(this.pointSymbolControl);
                    this.pointPanel.Location = new System.Drawing.Point(12, 3);
                    this.pointPanel.Name = "pointPanel";
                    this.pointPanel.Size = new System.Drawing.Size(705, 494);
                    this.pointPanel.TabIndex = 1;
                    // 
                    // pointSymbolControl
                    // 
                    this.pointSymbolControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
                    this.pointSymbolControl.Location = new System.Drawing.Point(14, 12);
                    this.pointSymbolControl.Name = "pointSymbolControl";
                    this.pointSymbolControl.Size = new System.Drawing.Size(681, 445);
                    this.pointSymbolControl.TabIndex = 0;
                    //
                    //
                    //
                    this.tabControl1.Controls.Add(this.pointPanel);
                    this.pointPanel.ResumeLayout(false);
                    this.ResumeLayout(false);
                    break;
                }
            }
            
        }

        private void ConfigureLinePage()
        {
            foreach (ILayer item in _map.MapFrame.DrawingLayers)
            {
                if (item.LegendText == "Line")
                {
                    string pageName = StringParser.Parse("${res:GIS.Common.Dialogs.Symbol.LineSymbolDialog}");
                    this.linePanel = new TabPage(pageName);
                    this.lineSymbolControl = new DetailedLineSymbolControl((item as ILineLayer).Symbolizer);
                    this.linePanel.SuspendLayout();
                    this.SuspendLayout();
                    // 
                    // linePanel
                    // 
                    this.linePanel.Controls.Add(this.lineSymbolControl);
                    this.linePanel.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.linePanel.Location = new System.Drawing.Point(0, 0);
                    this.linePanel.Name = "linePanel";
                    this.linePanel.Size = new System.Drawing.Size(926, 520);
                    this.linePanel.TabIndex = 1;
                    // 
                    // lineSymbolControl
                    // 
                    this.lineSymbolControl.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.lineSymbolControl.Location = new System.Drawing.Point(0, 0);
                    this.lineSymbolControl.Name = "lineSymbolControl";
                    this.lineSymbolControl.Size = new System.Drawing.Size(926, 520);
                    this.lineSymbolControl.TabIndex = 1;
                    // 
                    //
                    //
                    this.tabControl1.Controls.Add(this.linePanel);
                    this.linePanel.ResumeLayout(false);
                    this.ResumeLayout(false);
                    break;
                }
            }
            
        }

        private void ConfigurePolygonPage()
        {
            foreach (ILayer item in _map.MapFrame.DrawingLayers)
            {
                if (item.LegendText == "Polygon")
                {
                    string pageName = StringParser.Parse("${res:GIS.Common.Dialogs.Symbol.PolygonSymbolDialog}");
                    this.polygonPanel = new TabPage(pageName);
                    this.polygonSymbolControl = new DetailedPolygonSymbolControl((item as IPolygonLayer).Symbolizer);
                    this.polygonPanel.SuspendLayout();
                    this.SuspendLayout();
                    // 
                    // polygonPanel
                    // 
                    this.polygonPanel.Controls.Add(this.polygonSymbolControl);
                    this.polygonPanel.Location = new System.Drawing.Point(0, 0);
                    this.polygonPanel.Name = "polygonPanel";
                    this.polygonPanel.Size = new System.Drawing.Size(726, 464);
                    this.polygonPanel.TabIndex = 1;
                    // 
                    // polygonSymbolControl
                    // 
                    this.polygonSymbolControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
                    this.polygonSymbolControl.Location = new System.Drawing.Point(12, 12);
                    this.polygonSymbolControl.Name = "polygonSymbolControl";
                    this.polygonSymbolControl.Size = new System.Drawing.Size(699, 394);
                    this.polygonSymbolControl.TabIndex = 0;
                    // 
                    //
                    // 
                    this.tabControl1.Controls.Add(this.polygonPanel);
                    this.polygonPanel.ResumeLayout(false);
                    this.ResumeLayout(false);
                    break;
                }
            }     
        }

        private void ConfigureBufferPage()
        {
            foreach (ILayer item in _map.MapFrame.DrawingLayers)
            {
                if (item.LegendText == "ROI")
                {
                    //未添加字符串资源
                    string pageName = "Buffer Symbol";
                    this.bufferPanel = new TabPage(pageName);
                    this.bufferSymbolControl = new DetailedPolygonSymbolControl((item as IPolygonLayer).Symbolizer);
                    this.bufferPanel.SuspendLayout();
                    this.SuspendLayout();
                    // 
                    // bufferPanel
                    // 
                    this.bufferPanel.Controls.Add(this.bufferSymbolControl);
                    this.bufferPanel.Location = new System.Drawing.Point(0, 0);
                    this.bufferPanel.Name = "bufferPanel";
                    this.bufferPanel.Size = new System.Drawing.Size(726, 464);
                    this.bufferPanel.TabIndex = 1;
                    // 
                    // bufferSymbolControl
                    // 
                    this.bufferSymbolControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
                    this.bufferSymbolControl.Location = new System.Drawing.Point(12, 12);
                    this.bufferSymbolControl.Name = "bufferSymbolControl";
                    this.bufferSymbolControl.Size = new System.Drawing.Size(699, 394);
                    this.bufferSymbolControl.TabIndex = 0;
                    // 
                    //
                    // 
                    this.tabControl1.Controls.Add(this.bufferPanel);
                    this.bufferPanel.ResumeLayout(false);
                    this.ResumeLayout(false);
                    break;
                }
            }
        }

        #endregion

        #region EventHandlers
        private void btnApply_Click(object sender, EventArgs e)
        {
            OnApplyChanges();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            OnApplyChanges();
            Close();
        }
        #endregion

        #region Protected Methods
        protected virtual void OnApplyChanges()
        {
            if (pointSymbolControl != null) pointSymbolControl.ApplyChanges();
            if (lineSymbolControl != null) lineSymbolControl.ApplyChanges();
            if (polygonSymbolControl != null) polygonSymbolControl.ApplyChanges();
            if (bufferSymbolControl != null) bufferSymbolControl.ApplyChanges();    
            if (ChangesApplied != null) ChangesApplied(this, EventArgs.Empty);
        }
        #endregion
    }
}

using System;
using System.ComponentModel;
using System.Windows.Forms;
using DotSpatial.Symbology;
using ICSharpCode.Core;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// DetailedLineSymbolDialog
    /// </summary>
    public class DetailedLineSymbolDialog : Form
    {
        #region Events

        /// <summary>
        /// Occurs whenever the apply changes button is clicked, or else when the ok button is clicked.
        /// </summary>
        public event EventHandler ChangesApplied;

        #endregion
        private Panel panel1;
        private DialogButtons dialogButtons1;
        private DetailedLineSymbolControl detailedLineSymbolControl;
        private string _lineSymbolDialog = StringParser.Parse("${res:GIS.Common.Dialogs.Symbol.LineSymbolDialog}");

        #region Private Variables

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.detailedLineSymbolControl = new DetailedLineSymbolControl();
            this.dialogButtons1 = new GIS.Common.Dialogs.DialogButtons();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dialogButtons1);
            this.panel1.Controls.Add(this.detailedLineSymbolControl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(926, 520);
            this.panel1.TabIndex = 1;
            // 
            // detailedLineSymbolControl
            // 
            this.detailedLineSymbolControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailedLineSymbolControl.Location = new System.Drawing.Point(0, 0);
            this.detailedLineSymbolControl.Name = "detailedLineSymbolControl";
            this.detailedLineSymbolControl.Size = new System.Drawing.Size(926, 520);
            this.detailedLineSymbolControl.TabIndex = 1;
            // 
            // dialogButtons1
            // 
            this.dialogButtons1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dialogButtons1.Location = new System.Drawing.Point(0, 488);
            this.dialogButtons1.Name = "dialogButtons1";
            this.dialogButtons1.Size = new System.Drawing.Size(926, 32);
            this.dialogButtons1.TabIndex = 0;
            // 
            // DetailedLineSymbolDialog
            // 
            this.ClientSize = new System.Drawing.Size(926, 520);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DetailedLineSymbolDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.Text = _lineSymbolDialog;

        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of CollectionPropertyGrid
        /// </summary>
        public DetailedLineSymbolDialog()
        {
            InitializeComponent();
            Configure();
        }

        /// <summary>
        /// Creates a new line symbol dialog where only the original is specified and the
        /// duplicate is created.
        /// </summary>
        /// <param name="original"></param>
        public DetailedLineSymbolDialog(ILineSymbolizer original)
        {
            InitializeComponent();
            detailedLineSymbolControl.Initialize(original);
            Configure();
        }

        private void Configure()
        {
            //dialogButtons1.OkClicked += btnOk_Click;
            dialogButtons1.OkClicked += btnOk_Click;
            dialogButtons1.CancelClicked += btnCancel_Click;
            dialogButtons1.ApplyClicked += btnApply_Click;
        }

        #endregion

        #region Methods

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the symbolizer being used by this control.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ILineSymbolizer Symbolizer
        {
            get
            {
                if (detailedLineSymbolControl == null) return null;
                return detailedLineSymbolControl.Symbolizer;
            }
            set
            {
                if (detailedLineSymbolControl == null) return;
                detailedLineSymbolControl.Symbolizer = value;
            }
        }

        #endregion

        #region Events

        #endregion

        #region Event Handlers

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

        /// <summary>
        /// Fires the ChangesApplied event
        /// </summary>
        protected virtual void OnApplyChanges()
        {
            detailedLineSymbolControl.ApplyChanges();
            if (ChangesApplied != null) ChangesApplied(this, EventArgs.Empty);
        }

        #endregion
    }
}

using System;
using System.ComponentModel;
using System.Windows.Forms;
using DotSpatial.Symbology;
using ICSharpCode.Core;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// DetailedPointSymbolDialog
    /// </summary>
    public class DetailedPointSymbolDialog : Form
    {
        #region Events

        /// <summary>
        /// Occurs whenever the apply changes button is clicked, or else when the ok button is clicked.
        /// </summary>
        public event EventHandler ChangesApplied;

        #endregion

        private DetailedPointSymbolControl detailedPointSymbolControl1;
        private Panel panel1;
        private DialogButtons dialogButtons1;
        private string _pointSymbolDialog = StringParser.Parse("${res:GIS.Common.Dialogs.Symbol.PointSymbolDialog}");

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
            this.detailedPointSymbolControl1 = new DetailedPointSymbolControl();
            this.dialogButtons1 = new GIS.Common.Dialogs.DialogButtons();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.dialogButtons1);
            this.panel1.Controls.Add(this.detailedPointSymbolControl1);
            this.panel1.Location = new System.Drawing.Point(12, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(705, 494);
            this.panel1.TabIndex = 1;
            // 
            // detailedPointSymbolControl1
            // 
            this.detailedPointSymbolControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detailedPointSymbolControl1.Location = new System.Drawing.Point(14, 12);
            this.detailedPointSymbolControl1.Name = "detailedPointSymbolControl1";
            this.detailedPointSymbolControl1.Size = new System.Drawing.Size(681, 445);
            this.detailedPointSymbolControl1.TabIndex = 0;
            // 
            // dialogButtons1
            // 
            this.dialogButtons1.Location = new System.Drawing.Point(451, 459);
            this.dialogButtons1.Name = "dialogButtons1";
            this.dialogButtons1.Size = new System.Drawing.Size(244, 32);
            this.dialogButtons1.TabIndex = 1;
            // 
            // DetailedPointSymbolDialog
            // 
            this.ClientSize = new System.Drawing.Size(719, 509);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DetailedPointSymbolDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.Text = _pointSymbolDialog;

        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of CollectionPropertyGrid
        /// </summary>
        public DetailedPointSymbolDialog()
        {
            InitializeComponent();
            Configure();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="original"></param>
        public DetailedPointSymbolDialog(IPointSymbolizer original)
        {
            InitializeComponent();
            detailedPointSymbolControl1.Initialize(original);
            Configure();
        }

        private void Configure()
        {
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
        public IPointSymbolizer Symbolizer
        {
            get
            {
                if (detailedPointSymbolControl1 == null) return null;
                return detailedPointSymbolControl1.Symbolizer;
            }
            set
            {
                if (detailedPointSymbolControl1 == null) return;
                detailedPointSymbolControl1.Symbolizer = value;
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
            detailedPointSymbolControl1.ApplyChanges();
            if (ChangesApplied != null) ChangesApplied(this, EventArgs.Empty);
        }

        #endregion
    }
}
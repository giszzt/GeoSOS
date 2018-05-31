using System;
using System.Windows.Forms;
using DotSpatial.Symbology;
using ICSharpCode.Core;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// DetailedPolygonSymbolDialog
    /// </summary>
    public class DetailedPolygonSymbolDialog : Form
    {
        private DialogButtons dialogButtons1;
        private DetailedPolygonSymbolControl _detailedPolygonSymbolControl1;
        private string _polygonSymbolDialog = StringParser.Parse("${res:GIS.Common.Dialogs.Symbol.PolygonSymbolDialog}");
        #region Events

        /// <summary>
        /// Occurs whenever the apply changes button is clicked, or else when the ok button is clicked.
        /// </summary>
        public event EventHandler ChangesApplied;

        #endregion
        private Panel _panel1;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._panel1 = new System.Windows.Forms.Panel();
            this._detailedPolygonSymbolControl1 = new DetailedPolygonSymbolControl();
            this.dialogButtons1 = new GIS.Common.Dialogs.DialogButtons();
            this._panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _panel1
            // 
            this._panel1.Controls.Add(this.dialogButtons1);
            this._panel1.Controls.Add(this._detailedPolygonSymbolControl1);
            this._panel1.Location = new System.Drawing.Point(0, 0);
            this._panel1.Name = "_panel1";
            this._panel1.Size = new System.Drawing.Size(726, 464);
            this._panel1.TabIndex = 1;
            // 
            // _detailedPolygonSymbolControl1
            // 
            this._detailedPolygonSymbolControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._detailedPolygonSymbolControl1.Location = new System.Drawing.Point(12, 12);
            this._detailedPolygonSymbolControl1.Name = "_detailedPolygonSymbolControl1";
            this._detailedPolygonSymbolControl1.Size = new System.Drawing.Size(699, 394);
            this._detailedPolygonSymbolControl1.TabIndex = 0;
            // 
            // dialogButtons1
            // 
            this.dialogButtons1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dialogButtons1.Location = new System.Drawing.Point(467, 421);
            this.dialogButtons1.Name = "dialogButtons1";
            this.dialogButtons1.Size = new System.Drawing.Size(244, 32);
            this.dialogButtons1.TabIndex = 1;
            // 
            // DetailedPolygonSymbolDialog
            // 
            this.ClientSize = new System.Drawing.Size(723, 465);
            this.Controls.Add(this._panel1);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DetailedPolygonSymbolDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this._panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.Text = _polygonSymbolDialog;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of CollectionPropertyGrid
        /// </summary>
        public DetailedPolygonSymbolDialog()
        {
            InitializeComponent();
            Configure();
        }

        /// <summary>
        /// Creates a new DetailedPolygonSymbolControl using the specified
        /// </summary>
        /// <param name="original"></param>
        public DetailedPolygonSymbolDialog(IPolygonSymbolizer original)
        {
            InitializeComponent();
            _detailedPolygonSymbolControl1.Initialize(original);
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
        public IPolygonSymbolizer Symbolizer
        {
            get
            {
                if (_detailedPolygonSymbolControl1 == null) return null;
                return _detailedPolygonSymbolControl1.Symbolizer;
            }
            set
            {
                if (_detailedPolygonSymbolControl1 == null) return;
                _detailedPolygonSymbolControl1.Symbolizer = value;
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
        /// Fires the ChangesApplied event
        /// </summary>
        protected virtual void OnApplyChanges()
        {
            _detailedPolygonSymbolControl1.ApplyChanges();

            if (ChangesApplied != null) ChangesApplied(this, EventArgs.Empty);
        }

        #endregion
    }
}
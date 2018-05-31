using System;
using System.ComponentModel;
using System.Windows.Forms;
using ICSharpCode.Core;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// DialogButtons
    /// </summary>
    [DefaultEvent("OkClicked"), ToolboxItem(true)]
    public class DialogButtons : UserControl
    {
        #region Events

        /// <summary>
        /// The OK button was clicked
        /// </summary>
        public event EventHandler OkClicked;
        /// <summary>
        /// The Apply button was clicked
        /// </summary>
        public event EventHandler ApplyClicked;
        /// <summary>
        /// The Cancel button was clicked
        /// </summary>
        public event EventHandler CancelClicked;

        #endregion

        private Button btnApply;
        private Button btnCancel;
        private Button btnOK;
        private HelpProvider helpProvider1;

        #region Private Variables

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of DialogButtons
        /// </summary>
        public DialogButtons()
        {
            InitializeComponent();
        }

        #endregion

        private void InitializeComponent()
        {
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(84, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = StringParser.Parse("${res:UI.OK}");
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(3, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = StringParser.Parse("${res:UI.Cancel}");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += btnCancel_Click;
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(165, 3);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 0;
            this.btnApply.Text = StringParser.Parse("${res:UI.Apply}");
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += btnApply_Click;
            // 
            // DialogButtons
            // 
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Name = "DialogButtons";
            this.Size = new System.Drawing.Size(244, 32);
            this.ResumeLayout(false);

        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            OnOKClicked();
            GIS.FrameWork.Application.App.Map.Refresh();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            OnCancelClicked();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            OnApplyClicked();
            GIS.FrameWork.Application.App.Map.Refresh();
        }

        /// <summary>
        /// Fires the ok clicked event
        /// </summary>
        protected virtual void OnOKClicked()
        {
            if (OkClicked != null) OkClicked(this, EventArgs.Empty);
        }

        /// <summary>
        /// Fires the Cancel Clicked event
        /// </summary>
        protected virtual void OnCancelClicked()
        {
            if (CancelClicked != null) CancelClicked(this, EventArgs.Empty);
        }

        /// <summary>
        /// Fires the Apply Clicked event
        /// </summary>
        protected virtual void OnApplyClicked()
        {
            if (ApplyClicked != null) ApplyClicked(this, EventArgs.Empty);
        }
    }
}
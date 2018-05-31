namespace GIS.AddIns.Overlay
{
    partial class OverlayDialog
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
            this.lblBaseLayer = new System.Windows.Forms.Label();
            this.btnBaseLayer = new System.Windows.Forms.Button();
            this.btnOverlayLayer = new System.Windows.Forms.Button();
            this.lblOverlayLayer = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnResult = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.cmbBase = new System.Windows.Forms.ComboBox();
            this.cmbOverlay = new System.Windows.Forms.ComboBox();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblBaseLayer
            // 
            this.lblBaseLayer.AutoSize = true;
            this.lblBaseLayer.Location = new System.Drawing.Point(8, 18);
            this.lblBaseLayer.Name = "lblBaseLayer";
            this.lblBaseLayer.Size = new System.Drawing.Size(71, 12);
            this.lblBaseLayer.TabIndex = 0;
            this.lblBaseLayer.Text = "Base Layer:";
            // 
            // btnBaseLayer
            // 
            this.btnBaseLayer.Location = new System.Drawing.Point(424, 13);
            this.btnBaseLayer.Name = "btnBaseLayer";
            this.btnBaseLayer.Size = new System.Drawing.Size(31, 23);
            this.btnBaseLayer.TabIndex = 2;
            this.btnBaseLayer.Text = "...";
            this.btnBaseLayer.UseVisualStyleBackColor = true;
            this.btnBaseLayer.Click += new System.EventHandler(this.btnBaseLayer_Click);
            // 
            // btnOverlayLayer
            // 
            this.btnOverlayLayer.Location = new System.Drawing.Point(424, 55);
            this.btnOverlayLayer.Name = "btnOverlayLayer";
            this.btnOverlayLayer.Size = new System.Drawing.Size(31, 23);
            this.btnOverlayLayer.TabIndex = 5;
            this.btnOverlayLayer.Text = "...";
            this.btnOverlayLayer.UseVisualStyleBackColor = true;
            this.btnOverlayLayer.Click += new System.EventHandler(this.btnOverlayLayer_Click);
            // 
            // lblOverlayLayer
            // 
            this.lblOverlayLayer.AutoSize = true;
            this.lblOverlayLayer.Location = new System.Drawing.Point(7, 59);
            this.lblOverlayLayer.Name = "lblOverlayLayer";
            this.lblOverlayLayer.Size = new System.Drawing.Size(89, 12);
            this.lblOverlayLayer.TabIndex = 3;
            this.lblOverlayLayer.Text = "Overlay Layer:";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(310, 140);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(64, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(391, 140);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnResult
            // 
            this.btnResult.Location = new System.Drawing.Point(424, 100);
            this.btnResult.Name = "btnResult";
            this.btnResult.Size = new System.Drawing.Size(31, 23);
            this.btnResult.TabIndex = 10;
            this.btnResult.Text = "...";
            this.btnResult.UseVisualStyleBackColor = true;
            this.btnResult.Click += new System.EventHandler(this.btnResult_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(8, 104);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(83, 12);
            this.lblResult.TabIndex = 8;
            this.lblResult.Text = "Result Layer:";
            // 
            // cmbBase
            // 
            this.cmbBase.FormattingEnabled = true;
            this.cmbBase.Location = new System.Drawing.Point(98, 15);
            this.cmbBase.Name = "cmbBase";
            this.cmbBase.Size = new System.Drawing.Size(320, 20);
            this.cmbBase.TabIndex = 11;
            // 
            // cmbOverlay
            // 
            this.cmbOverlay.FormattingEnabled = true;
            this.cmbOverlay.Location = new System.Drawing.Point(98, 57);
            this.cmbOverlay.Name = "cmbOverlay";
            this.cmbOverlay.Size = new System.Drawing.Size(320, 20);
            this.cmbOverlay.TabIndex = 12;
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(98, 102);
            this.tbResult.Name = "tbResult";
            this.tbResult.Size = new System.Drawing.Size(320, 21);
            this.tbResult.TabIndex = 14;
            // 
            // OverlayDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 175);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.cmbOverlay);
            this.Controls.Add(this.cmbBase);
            this.Controls.Add(this.btnResult);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnOverlayLayer);
            this.Controls.Add(this.lblOverlayLayer);
            this.Controls.Add(this.btnBaseLayer);
            this.Controls.Add(this.lblBaseLayer);
            this.Name = "OverlayDialog";
            this.Text = "Intersection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBaseLayer;
        private System.Windows.Forms.Button btnBaseLayer;
        private System.Windows.Forms.Button btnOverlayLayer;
        private System.Windows.Forms.Label lblOverlayLayer;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnResult;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.ComboBox cmbBase;
        private System.Windows.Forms.ComboBox cmbOverlay;
        private System.Windows.Forms.TextBox tbResult;
    }
}
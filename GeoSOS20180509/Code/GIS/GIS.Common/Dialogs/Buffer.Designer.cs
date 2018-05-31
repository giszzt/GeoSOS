namespace GIS.Common.Dialogs
{
    partial class Buffer
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
            this.lbDistance = new System.Windows.Forms.Label();
            this.disValue = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.lbUnit = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbDistance
            // 
            this.lbDistance.AutoSize = true;
            this.lbDistance.Location = new System.Drawing.Point(21, 56);
            this.lbDistance.Name = "lbDistance";
            this.lbDistance.Size = new System.Drawing.Size(59, 12);
            this.lbDistance.TabIndex = 0;
            this.lbDistance.Text = "distance:";
            // 
            // disValue
            // 
            this.disValue.Location = new System.Drawing.Point(83, 53);
            this.disValue.Name = "disValue";
            this.disValue.Size = new System.Drawing.Size(136, 21);
            this.disValue.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(197, 226);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.ok_Click);
            // 
            // lbUnit
            // 
            this.lbUnit.AutoSize = true;
            this.lbUnit.Location = new System.Drawing.Point(231, 56);
            this.lbUnit.Name = "lbUnit";
            this.lbUnit.Size = new System.Drawing.Size(41, 12);
            this.lbUnit.TabIndex = 3;
            this.lbUnit.Text = "meters";
            // 
            // Buffer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.lbUnit);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.disValue);
            this.Controls.Add(this.lbDistance);
            this.Name = "Buffer";
            this.Text = "Buffer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbDistance;
        private System.Windows.Forms.TextBox disValue;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lbUnit;
    }
}
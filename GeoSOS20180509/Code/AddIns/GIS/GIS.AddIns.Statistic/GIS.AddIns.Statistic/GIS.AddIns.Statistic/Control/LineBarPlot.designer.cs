namespace GIS.AddIns.Statistic
{
    partial class LineBarPlot
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbY = new System.Windows.Forms.ComboBox();
            this.cmbX = new System.Windows.Forms.ComboBox();
            this.Y = new System.Windows.Forms.Label();
            this.X = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbY
            // 
            this.cmbY.FormattingEnabled = true;
            this.cmbY.Location = new System.Drawing.Point(117, 78);
            this.cmbY.Name = "cmbY";
            this.cmbY.Size = new System.Drawing.Size(121, 20);
            this.cmbY.TabIndex = 4;
            // 
            // cmbX
            // 
            this.cmbX.FormattingEnabled = true;
            this.cmbX.Location = new System.Drawing.Point(117, 41);
            this.cmbX.Name = "cmbX";
            this.cmbX.Size = new System.Drawing.Size(121, 20);
            this.cmbX.TabIndex = 5;
            // 
            // Y
            // 
            this.Y.AutoSize = true;
            this.Y.Location = new System.Drawing.Point(35, 81);
            this.Y.Name = "Y";
            this.Y.Size = new System.Drawing.Size(35, 12);
            this.Y.TabIndex = 2;
            this.Y.Text = "DataY";
            // 
            // X
            // 
            this.X.AutoSize = true;
            this.X.Location = new System.Drawing.Point(35, 46);
            this.X.Name = "X";
            this.X.Size = new System.Drawing.Size(35, 12);
            this.X.TabIndex = 3;
            this.X.Text = "DataX";
            // 
            // LineBarPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbY);
            this.Controls.Add(this.cmbX);
            this.Controls.Add(this.Y);
            this.Controls.Add(this.X);
            this.Name = "LineBarPlot";
            this.Size = new System.Drawing.Size(283, 138);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbY;
        private System.Windows.Forms.ComboBox cmbX;
        private System.Windows.Forms.Label Y;
        private System.Windows.Forms.Label X;
    }
}

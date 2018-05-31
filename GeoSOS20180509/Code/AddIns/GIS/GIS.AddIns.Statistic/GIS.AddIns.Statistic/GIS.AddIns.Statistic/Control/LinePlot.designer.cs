namespace GIS.AddIns.Statistic
{
    partial class LinePlot
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
            this.X = new System.Windows.Forms.Label();
            this.Y = new System.Windows.Forms.Label();
            this.cmbX = new System.Windows.Forms.ComboBox();
            this.cmbY = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbStyle = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbAngle = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // X
            // 
            this.X.AutoSize = true;
            this.X.Location = new System.Drawing.Point(35, 12);
            this.X.Name = "X";
            this.X.Size = new System.Drawing.Size(35, 12);
            this.X.TabIndex = 0;
            this.X.Text = "DataX";
            // 
            // Y
            // 
            this.Y.AutoSize = true;
            this.Y.Location = new System.Drawing.Point(35, 44);
            this.Y.Name = "Y";
            this.Y.Size = new System.Drawing.Size(35, 12);
            this.Y.TabIndex = 0;
            this.Y.Text = "DataY";
            // 
            // cmbX
            // 
            this.cmbX.FormattingEnabled = true;
            this.cmbX.Location = new System.Drawing.Point(117, 9);
            this.cmbX.Name = "cmbX";
            this.cmbX.Size = new System.Drawing.Size(121, 20);
            this.cmbX.TabIndex = 1;
            // 
            // cmbY
            // 
            this.cmbY.FormattingEnabled = true;
            this.cmbY.Location = new System.Drawing.Point(117, 41);
            this.cmbY.Name = "cmbY";
            this.cmbY.Size = new System.Drawing.Size(121, 20);
            this.cmbY.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "LineStyle";
            // 
            // cmbStyle
            // 
            this.cmbStyle.FormattingEnabled = true;
            this.cmbStyle.Location = new System.Drawing.Point(117, 76);
            this.cmbStyle.Name = "cmbStyle";
            this.cmbStyle.Size = new System.Drawing.Size(121, 20);
            this.cmbStyle.TabIndex = 1;
            this.cmbStyle.SelectedIndexChanged += new System.EventHandler(this.cmbStyle_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "AngleStyle";
            // 
            // cmbAngle
            // 
            this.cmbAngle.FormattingEnabled = true;
            this.cmbAngle.Location = new System.Drawing.Point(117, 108);
            this.cmbAngle.Name = "cmbAngle";
            this.cmbAngle.Size = new System.Drawing.Size(121, 20);
            this.cmbAngle.TabIndex = 1;
            this.cmbAngle.SelectedIndexChanged += new System.EventHandler(this.cmbAngle_SelectedIndexChanged);
            // 
            // LinePlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbAngle);
            this.Controls.Add(this.cmbStyle);
            this.Controls.Add(this.cmbY);
            this.Controls.Add(this.cmbX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Y);
            this.Controls.Add(this.X);
            this.Name = "LinePlot";
            this.Size = new System.Drawing.Size(283, 138);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label X;
        private System.Windows.Forms.Label Y;
        private System.Windows.Forms.ComboBox cmbX;
        private System.Windows.Forms.ComboBox cmbY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbStyle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbAngle;
    }
}

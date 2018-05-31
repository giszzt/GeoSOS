namespace MultiPlan
{
    partial class ModelBuilderForm
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
            this.modelBuilderControl1 = new MultiPlan.ModelBuilderControl();
            this.SuspendLayout();
            // 
            // modelBuilderControl1
            // 
            this.modelBuilderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modelBuilderControl1.Location = new System.Drawing.Point(0, 0);
            this.modelBuilderControl1.Name = "modelBuilderControl1";
            this.modelBuilderControl1.Size = new System.Drawing.Size(778, 556);
            this.modelBuilderControl1.TabIndex = 0;
            // 
            // ModelBuilderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 556);
            this.Controls.Add(this.modelBuilderControl1);
            this.Name = "ModelBuilderForm";
            this.Text = "规则图谱";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModelBuilderForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private ModelBuilderControl modelBuilderControl1;
    }
}
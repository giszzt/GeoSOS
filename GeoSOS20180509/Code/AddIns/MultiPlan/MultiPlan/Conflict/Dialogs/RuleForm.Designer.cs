namespace MultiPlan
{
    partial class RuleForm
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
            this.conflictControl1 = new MultiPlan.RuleControl();
            this.SuspendLayout();
            // 
            // conflictControl1
            // 
            this.conflictControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conflictControl1.Location = new System.Drawing.Point(0, 0);
            this.conflictControl1.Name = "conflictControl1";
            this.conflictControl1.Size = new System.Drawing.Size(400, 347);
            this.conflictControl1.TabIndex = 0;
            // 
            // RuleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 347);
            this.Controls.Add(this.conflictControl1);
            this.Name = "RuleForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "冲突规则";
            this.Load += new System.EventHandler(this.ConflictForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private RuleControl conflictControl1;
    }
}
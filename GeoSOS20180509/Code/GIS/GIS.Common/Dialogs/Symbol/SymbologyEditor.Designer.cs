namespace GIS.Common.Dialogs
{
    partial class SymbologyEditor
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
            this.btnBlueLine = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBlueLine
            // 
            this.btnBlueLine.Location = new System.Drawing.Point(134, 203);
            this.btnBlueLine.Name = "btnBlueLine";
            this.btnBlueLine.Size = new System.Drawing.Size(138, 46);
            this.btnBlueLine.TabIndex = 1;
            this.btnBlueLine.Text = "blueLine";
            this.btnBlueLine.UseVisualStyleBackColor = true;
            this.btnBlueLine.Click += new System.EventHandler(this.btnBlueLine_Click);
            // 
            // SymbologyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnBlueLine);
            this.Name = "SymbologyEditor";
            this.Text = "Symbology";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBlueLine;
    }
}
namespace GIS.AddIns.Ca.CaDialog
{
    partial class SimpleCaDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxInitialUrbanImage = new System.Windows.Forms.ComboBox();
            this.comboBoxPg = new System.Windows.Forms.ComboBox();
            this.comboBoxLandSuitable = new System.Windows.Forms.ComboBox();
            this.textBoxThreshold = new System.Windows.Forms.TextBox();
            this.textBoxTimes = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "initial urban image";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "pg";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "land suitable";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "threshold";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 260);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "times";
            // 
            // comboBoxInitialUrbanImage
            // 
            this.comboBoxInitialUrbanImage.FormattingEnabled = true;
            this.comboBoxInitialUrbanImage.Location = new System.Drawing.Point(244, 57);
            this.comboBoxInitialUrbanImage.Name = "comboBoxInitialUrbanImage";
            this.comboBoxInitialUrbanImage.Size = new System.Drawing.Size(121, 20);
            this.comboBoxInitialUrbanImage.TabIndex = 1;
            // 
            // comboBoxPg
            // 
            this.comboBoxPg.FormattingEnabled = true;
            this.comboBoxPg.Location = new System.Drawing.Point(244, 107);
            this.comboBoxPg.Name = "comboBoxPg";
            this.comboBoxPg.Size = new System.Drawing.Size(121, 20);
            this.comboBoxPg.TabIndex = 1;
            // 
            // comboBoxLandSuitable
            // 
            this.comboBoxLandSuitable.FormattingEnabled = true;
            this.comboBoxLandSuitable.Location = new System.Drawing.Point(244, 164);
            this.comboBoxLandSuitable.Name = "comboBoxLandSuitable";
            this.comboBoxLandSuitable.Size = new System.Drawing.Size(121, 20);
            this.comboBoxLandSuitable.TabIndex = 1;
            // 
            // textBoxThreshold
            // 
            this.textBoxThreshold.Location = new System.Drawing.Point(244, 203);
            this.textBoxThreshold.Name = "textBoxThreshold";
            this.textBoxThreshold.Size = new System.Drawing.Size(100, 21);
            this.textBoxThreshold.TabIndex = 2;
            // 
            // textBoxTimes
            // 
            this.textBoxTimes.Location = new System.Drawing.Point(244, 251);
            this.textBoxTimes.Name = "textBoxTimes";
            this.textBoxTimes.Size = new System.Drawing.Size(100, 21);
            this.textBoxTimes.TabIndex = 2;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(244, 320);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(339, 320);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // SimpleCaDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 375);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.textBoxTimes);
            this.Controls.Add(this.textBoxThreshold);
            this.Controls.Add(this.comboBoxLandSuitable);
            this.Controls.Add(this.comboBoxPg);
            this.Controls.Add(this.comboBoxInitialUrbanImage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "SimpleCaDialog";
            this.Text = "SimpleCaDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxInitialUrbanImage;
        private System.Windows.Forms.ComboBox comboBoxPg;
        private System.Windows.Forms.ComboBox comboBoxLandSuitable;
        private System.Windows.Forms.TextBox textBoxThreshold;
        private System.Windows.Forms.TextBox textBoxTimes;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
    }
}
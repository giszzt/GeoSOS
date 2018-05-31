namespace GIS.AddIns.Statistic
{
    partial class Histogram
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
            this.Y = new System.Windows.Forms.Label();
            this.LBox = new System.Windows.Forms.ListBox();
            this.Btn1 = new System.Windows.Forms.Button();
            this.Btn2 = new System.Windows.Forms.Button();
            this.X = new System.Windows.Forms.Label();
            this.catagoryLbx = new System.Windows.Forms.Label();
            this.Btn3 = new System.Windows.Forms.Button();
            this.Btn4 = new System.Windows.Forms.Button();
            this.Cbx_threshold = new System.Windows.Forms.ComboBox();
            this.Cbx_Xname = new System.Windows.Forms.ComboBox();
            this.DataSelection = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LBox2 = new System.Windows.Forms.ListBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbY
            // 
            this.cmbY.FormattingEnabled = true;
            this.cmbY.Location = new System.Drawing.Point(71, 20);
            this.cmbY.Name = "cmbY";
            this.cmbY.Size = new System.Drawing.Size(121, 20);
            this.cmbY.TabIndex = 8;
            // 
            // Y
            // 
            this.Y.AutoSize = true;
            this.Y.Location = new System.Drawing.Point(18, 23);
            this.Y.Name = "Y";
            this.Y.Size = new System.Drawing.Size(41, 12);
            this.Y.TabIndex = 6;
            this.Y.Text = "ValueY";
            // 
            // LBox
            // 
            this.LBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.LBox.FormattingEnabled = true;
            this.LBox.ItemHeight = 14;
            this.LBox.Location = new System.Drawing.Point(236, 20);
            this.LBox.Name = "LBox";
            this.LBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.LBox.Size = new System.Drawing.Size(62, 74);
            this.LBox.TabIndex = 10;
            this.LBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LBox_MouseClick);
            this.LBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LBox_DrawItem);
            this.LBox.DoubleClick += new System.EventHandler(this.LBox_DoubleClick);
            // 
            // Btn1
            // 
            this.Btn1.Location = new System.Drawing.Point(71, 97);
            this.Btn1.Name = "Btn1";
            this.Btn1.Size = new System.Drawing.Size(47, 23);
            this.Btn1.TabIndex = 12;
            this.Btn1.Text = "Add";
            this.Btn1.UseVisualStyleBackColor = true;
            this.Btn1.Click += new System.EventHandler(this.Btn1_Click);
            // 
            // Btn2
            // 
            this.Btn2.Location = new System.Drawing.Point(141, 97);
            this.Btn2.Name = "Btn2";
            this.Btn2.Size = new System.Drawing.Size(51, 23);
            this.Btn2.TabIndex = 12;
            this.Btn2.Text = "Delete";
            this.Btn2.UseVisualStyleBackColor = true;
            this.Btn2.Click += new System.EventHandler(this.Btn2_Click);
            // 
            // X
            // 
            this.X.AutoSize = true;
            this.X.Location = new System.Drawing.Point(18, 65);
            this.X.Name = "X";
            this.X.Size = new System.Drawing.Size(47, 12);
            this.X.TabIndex = 13;
            this.X.Text = "Xlabels";
            // 
            // catagoryLbx
            // 
            this.catagoryLbx.AutoSize = true;
            this.catagoryLbx.Location = new System.Drawing.Point(12, 144);
            this.catagoryLbx.Name = "catagoryLbx";
            this.catagoryLbx.Size = new System.Drawing.Size(53, 12);
            this.catagoryLbx.TabIndex = 14;
            this.catagoryLbx.Text = "Catagory";
            // 
            // Btn3
            // 
            this.Btn3.Location = new System.Drawing.Point(71, 175);
            this.Btn3.Name = "Btn3";
            this.Btn3.Size = new System.Drawing.Size(47, 23);
            this.Btn3.TabIndex = 16;
            this.Btn3.Text = "Add";
            this.Btn3.UseVisualStyleBackColor = true;
            this.Btn3.Click += new System.EventHandler(this.Btn3_Click);
            // 
            // Btn4
            // 
            this.Btn4.Location = new System.Drawing.Point(141, 175);
            this.Btn4.Name = "Btn4";
            this.Btn4.Size = new System.Drawing.Size(51, 23);
            this.Btn4.TabIndex = 16;
            this.Btn4.Text = "Delete";
            this.Btn4.UseVisualStyleBackColor = true;
            // 
            // Cbx_threshold
            // 
            this.Cbx_threshold.FormattingEnabled = true;
            this.Cbx_threshold.Location = new System.Drawing.Point(71, 62);
            this.Cbx_threshold.Name = "Cbx_threshold";
            this.Cbx_threshold.Size = new System.Drawing.Size(121, 20);
            this.Cbx_threshold.TabIndex = 8;
            // 
            // Cbx_Xname
            // 
            this.Cbx_Xname.FormattingEnabled = true;
            this.Cbx_Xname.Location = new System.Drawing.Point(71, 141);
            this.Cbx_Xname.Name = "Cbx_Xname";
            this.Cbx_Xname.Size = new System.Drawing.Size(121, 20);
            this.Cbx_Xname.TabIndex = 8;
            // 
            // DataSelection
            // 
            this.DataSelection.Location = new System.Drawing.Point(3, 3);
            this.DataSelection.Name = "DataSelection";
            this.DataSelection.Size = new System.Drawing.Size(209, 207);
            this.DataSelection.TabIndex = 17;
            this.DataSelection.TabStop = false;
            this.DataSelection.Text = "DataSelection";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LBox2);
            this.groupBox2.Location = new System.Drawing.Point(219, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(97, 207);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DataList";
            // 
            // LBox2
            // 
            this.LBox2.FormattingEnabled = true;
            this.LBox2.ItemHeight = 12;
            this.LBox2.Location = new System.Drawing.Point(17, 106);
            this.LBox2.Name = "LBox2";
            this.LBox2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.LBox2.Size = new System.Drawing.Size(62, 76);
            this.LBox2.TabIndex = 0;
            // 
            // Histogram
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Btn4);
            this.Controls.Add(this.Btn3);
            this.Controls.Add(this.catagoryLbx);
            this.Controls.Add(this.X);
            this.Controls.Add(this.Btn2);
            this.Controls.Add(this.Btn1);
            this.Controls.Add(this.LBox);
            this.Controls.Add(this.Cbx_Xname);
            this.Controls.Add(this.Cbx_threshold);
            this.Controls.Add(this.cmbY);
            this.Controls.Add(this.Y);
            this.Controls.Add(this.DataSelection);
            this.Controls.Add(this.groupBox2);
            this.Name = "Histogram";
            this.Size = new System.Drawing.Size(330, 210);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbY;
        private System.Windows.Forms.Label Y;
        private System.Windows.Forms.ListBox LBox;
        private System.Windows.Forms.Button Btn1;
        private System.Windows.Forms.Button Btn2;
        private System.Windows.Forms.Label X;
        private System.Windows.Forms.Label catagoryLbx;
        private System.Windows.Forms.Button Btn3;
        private System.Windows.Forms.Button Btn4;
        private System.Windows.Forms.ComboBox Cbx_threshold;
        private System.Windows.Forms.ComboBox Cbx_Xname;
        private System.Windows.Forms.GroupBox DataSelection;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox LBox2;
    }
}

namespace GIS.AddIns.Ca.CommonDialog
{
    partial class LogisticSetUpForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonOpenEnd = new System.Windows.Forms.Button();
            this.buttonOpenStart = new System.Windows.Forms.Button();
            this.textBoxEndPath = new System.Windows.Forms.TextBox();
            this.textBoxStartPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonRemoveDrive = new System.Windows.Forms.Button();
            this.buttonAddDrive = new System.Windows.Forms.Button();
            this.listBoxDriveLayerNames = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxNumOfSample = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonLandUse = new System.Windows.Forms.Button();
            this.buttonSetProperties = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxSavePath = new System.Windows.Forms.TextBox();
            this.buttonSavePath = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonSavePath);
            this.groupBox2.Controls.Add(this.buttonOpenEnd);
            this.groupBox2.Controls.Add(this.buttonOpenStart);
            this.groupBox2.Controls.Add(this.textBoxSavePath);
            this.groupBox2.Controls.Add(this.textBoxEndPath);
            this.groupBox2.Controls.Add(this.textBoxStartPath);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(381, 126);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "起始和终止年份数据";
            // 
            // buttonOpenEnd
            // 
            this.buttonOpenEnd.Location = new System.Drawing.Point(306, 62);
            this.buttonOpenEnd.Name = "buttonOpenEnd";
            this.buttonOpenEnd.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenEnd.TabIndex = 2;
            this.buttonOpenEnd.Text = "打开";
            this.buttonOpenEnd.UseVisualStyleBackColor = true;
            this.buttonOpenEnd.Click += new System.EventHandler(this.buttonOpenEnd_Click);
            // 
            // buttonOpenStart
            // 
            this.buttonOpenStart.Location = new System.Drawing.Point(306, 29);
            this.buttonOpenStart.Name = "buttonOpenStart";
            this.buttonOpenStart.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenStart.TabIndex = 2;
            this.buttonOpenStart.Text = "打开";
            this.buttonOpenStart.UseVisualStyleBackColor = true;
            this.buttonOpenStart.Click += new System.EventHandler(this.buttonOpenStart_Click);
            // 
            // textBoxEndPath
            // 
            this.textBoxEndPath.Location = new System.Drawing.Point(126, 62);
            this.textBoxEndPath.Name = "textBoxEndPath";
            this.textBoxEndPath.Size = new System.Drawing.Size(174, 21);
            this.textBoxEndPath.TabIndex = 1;
            // 
            // textBoxStartPath
            // 
            this.textBoxStartPath.Location = new System.Drawing.Point(126, 29);
            this.textBoxStartPath.Name = "textBoxStartPath";
            this.textBoxStartPath.Size = new System.Drawing.Size(174, 21);
            this.textBoxStartPath.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "起始年份影像";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "终止年份影像";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonRemoveDrive);
            this.groupBox1.Controls.Add(this.buttonAddDrive);
            this.groupBox1.Controls.Add(this.listBoxDriveLayerNames);
            this.groupBox1.Location = new System.Drawing.Point(12, 144);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(382, 156);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "驱动数据";
            // 
            // buttonRemoveDrive
            // 
            this.buttonRemoveDrive.Location = new System.Drawing.Point(22, 97);
            this.buttonRemoveDrive.Name = "buttonRemoveDrive";
            this.buttonRemoveDrive.Size = new System.Drawing.Size(75, 23);
            this.buttonRemoveDrive.TabIndex = 2;
            this.buttonRemoveDrive.Text = "移除";
            this.buttonRemoveDrive.UseVisualStyleBackColor = true;
            this.buttonRemoveDrive.Click += new System.EventHandler(this.buttonRemoveDrive_Click);
            // 
            // buttonAddDrive
            // 
            this.buttonAddDrive.Location = new System.Drawing.Point(22, 46);
            this.buttonAddDrive.Name = "buttonAddDrive";
            this.buttonAddDrive.Size = new System.Drawing.Size(75, 23);
            this.buttonAddDrive.TabIndex = 1;
            this.buttonAddDrive.Text = "添加";
            this.buttonAddDrive.UseVisualStyleBackColor = true;
            this.buttonAddDrive.Click += new System.EventHandler(this.buttonAddDrive_Click);
            // 
            // listBoxDriveLayerNames
            // 
            this.listBoxDriveLayerNames.FormattingEnabled = true;
            this.listBoxDriveLayerNames.ItemHeight = 12;
            this.listBoxDriveLayerNames.Location = new System.Drawing.Point(225, 20);
            this.listBoxDriveLayerNames.Name = "listBoxDriveLayerNames";
            this.listBoxDriveLayerNames.Size = new System.Drawing.Size(136, 124);
            this.listBoxDriveLayerNames.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxNumOfSample);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(14, 306);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(380, 77);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "参数";
            // 
            // textBoxNumOfSample
            // 
            this.textBoxNumOfSample.Location = new System.Drawing.Point(91, 32);
            this.textBoxNumOfSample.Name = "textBoxNumOfSample";
            this.textBoxNumOfSample.Size = new System.Drawing.Size(100, 21);
            this.textBoxNumOfSample.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "采样数目";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonLandUse);
            this.groupBox4.Location = new System.Drawing.Point(12, 404);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(381, 70);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "土地利用类型设置";
            // 
            // buttonLandUse
            // 
            this.buttonLandUse.Location = new System.Drawing.Point(24, 33);
            this.buttonLandUse.Name = "buttonLandUse";
            this.buttonLandUse.Size = new System.Drawing.Size(94, 23);
            this.buttonLandUse.TabIndex = 0;
            this.buttonLandUse.Text = "土地利用类型设置";
            this.buttonLandUse.UseVisualStyleBackColor = true;
            this.buttonLandUse.Click += new System.EventHandler(this.buttonLandUse_Click);
            // 
            // buttonSetProperties
            // 
            this.buttonSetProperties.Location = new System.Drawing.Point(297, 481);
            this.buttonSetProperties.Name = "buttonSetProperties";
            this.buttonSetProperties.Size = new System.Drawing.Size(75, 23);
            this.buttonSetProperties.TabIndex = 11;
            this.buttonSetProperties.Text = "确认";
            this.buttonSetProperties.UseVisualStyleBackColor = true;
            this.buttonSetProperties.Click += new System.EventHandler(this.buttonSetProperties_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "输出路径";
            // 
            // textBoxSavePath
            // 
            this.textBoxSavePath.Location = new System.Drawing.Point(126, 89);
            this.textBoxSavePath.Name = "textBoxSavePath";
            this.textBoxSavePath.Size = new System.Drawing.Size(174, 21);
            this.textBoxSavePath.TabIndex = 1;
            // 
            // buttonSavePath
            // 
            this.buttonSavePath.Location = new System.Drawing.Point(306, 87);
            this.buttonSavePath.Name = "buttonSavePath";
            this.buttonSavePath.Size = new System.Drawing.Size(75, 23);
            this.buttonSavePath.TabIndex = 2;
            this.buttonSavePath.Text = "打开";
            this.buttonSavePath.UseVisualStyleBackColor = true;
            this.buttonSavePath.Click += new System.EventHandler(this.buttonSavePath_Click);
            // 
            // LogisticSetUpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 505);
            this.Controls.Add(this.buttonSetProperties);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "LogisticSetUpForm";
            this.Text = "LogisticSetUpForm";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonOpenEnd;
        private System.Windows.Forms.Button buttonOpenStart;
        private System.Windows.Forms.TextBox textBoxEndPath;
        private System.Windows.Forms.TextBox textBoxStartPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonRemoveDrive;
        private System.Windows.Forms.Button buttonAddDrive;
        private System.Windows.Forms.ListBox listBoxDriveLayerNames;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxNumOfSample;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonLandUse;
        private System.Windows.Forms.Button buttonSetProperties;
        private System.Windows.Forms.Button buttonSavePath;
        private System.Windows.Forms.TextBox textBoxSavePath;
        private System.Windows.Forms.Label label4;
    }
}
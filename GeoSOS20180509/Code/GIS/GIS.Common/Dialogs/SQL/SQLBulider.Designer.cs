namespace GIS.Common.Dialogs
{
    partial class SQLBulider
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnkh = new System.Windows.Forms.Button();
            this.btnK = new System.Windows.Forms.Button();
            this.btnN = new System.Windows.Forms.Button();
            this.btnnot = new System.Windows.Forms.Button();
            this.btnor = new System.Windows.Forms.Button();
            this.btnUnderLine = new System.Windows.Forms.Button();
            this.btnMod = new System.Windows.Forms.Button();
            this.btnis = new System.Windows.Forms.Button();
            this.btnunequal = new System.Windows.Forms.Button();
            this.btnge = new System.Windows.Forms.Button();
            this.btnle = new System.Windows.Forms.Button();
            this.uniqueList = new System.Windows.Forms.ListBox();
            this.btnGetUnique = new System.Windows.Forms.Button();
            this.btnClearExpression = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEqual = new System.Windows.Forms.Button();
            this.btnLess = new System.Windows.Forms.Button();
            this.btnGreater = new System.Windows.Forms.Button();
            this.btnQuire = new System.Windows.Forms.Button();
            this.select_expression = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fieldList = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnkh);
            this.groupBox1.Controls.Add(this.btnK);
            this.groupBox1.Controls.Add(this.btnN);
            this.groupBox1.Controls.Add(this.btnnot);
            this.groupBox1.Controls.Add(this.btnor);
            this.groupBox1.Controls.Add(this.btnUnderLine);
            this.groupBox1.Controls.Add(this.btnMod);
            this.groupBox1.Controls.Add(this.btnis);
            this.groupBox1.Controls.Add(this.btnunequal);
            this.groupBox1.Controls.Add(this.btnge);
            this.groupBox1.Controls.Add(this.btnle);
            this.groupBox1.Controls.Add(this.uniqueList);
            this.groupBox1.Controls.Add(this.btnGetUnique);
            this.groupBox1.Controls.Add(this.btnClearExpression);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnEqual);
            this.groupBox1.Controls.Add(this.btnLess);
            this.groupBox1.Controls.Add(this.btnGreater);
            this.groupBox1.Controls.Add(this.btnQuire);
            this.groupBox1.Controls.Add(this.select_expression);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.fieldList);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(387, 396);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fliter by Attributes";
            // 
            // btnkh
            // 
            this.btnkh.Location = new System.Drawing.Point(78, 211);
            this.btnkh.Name = "btnkh";
            this.btnkh.Size = new System.Drawing.Size(69, 23);
            this.btnkh.TabIndex = 25;
            this.btnkh.Text = "( )";
            this.btnkh.UseVisualStyleBackColor = true;
            this.btnkh.Click += new System.EventHandler(this.btnkhClick);
            // 
            // btnK
            // 
            this.btnK.Location = new System.Drawing.Point(153, 123);
            this.btnK.Name = "btnK";
            this.btnK.Size = new System.Drawing.Size(64, 23);
            this.btnK.TabIndex = 24;
            this.btnK.Text = "Like(&K)";
            this.btnK.UseVisualStyleBackColor = true;
            this.btnK.Click += new System.EventHandler(this.btnKClick);
            // 
            // btnN
            // 
            this.btnN.Location = new System.Drawing.Point(153, 152);
            this.btnN.Name = "btnN";
            this.btnN.Size = new System.Drawing.Size(64, 23);
            this.btnN.TabIndex = 23;
            this.btnN.Text = "And(&N)";
            this.btnN.UseVisualStyleBackColor = true;
            this.btnN.Click += new System.EventHandler(this.btnNClick);
            // 
            // btnnot
            // 
            this.btnnot.Location = new System.Drawing.Point(155, 211);
            this.btnnot.Name = "btnnot";
            this.btnnot.Size = new System.Drawing.Size(62, 23);
            this.btnnot.TabIndex = 22;
            this.btnnot.Text = "Not(&T)";
            this.btnnot.UseVisualStyleBackColor = true;
            this.btnnot.Click += new System.EventHandler(this.btnnotClick);
            // 
            // btnor
            // 
            this.btnor.Location = new System.Drawing.Point(153, 181);
            this.btnor.Name = "btnor";
            this.btnor.Size = new System.Drawing.Size(64, 23);
            this.btnor.TabIndex = 21;
            this.btnor.Text = "Or(&R)";
            this.btnor.UseVisualStyleBackColor = true;
            this.btnor.Click += new System.EventHandler(this.btnorClick);
            // 
            // btnUnderLine
            // 
            this.btnUnderLine.Location = new System.Drawing.Point(8, 211);
            this.btnUnderLine.Name = "btnUnderLine";
            this.btnUnderLine.Size = new System.Drawing.Size(29, 23);
            this.btnUnderLine.TabIndex = 20;
            this.btnUnderLine.Text = "_";
            this.btnUnderLine.UseVisualStyleBackColor = true;
            this.btnUnderLine.Click += new System.EventHandler(this.btnUnderLineClick);
            // 
            // btnMod
            // 
            this.btnMod.Location = new System.Drawing.Point(43, 211);
            this.btnMod.Name = "btnMod";
            this.btnMod.Size = new System.Drawing.Size(28, 23);
            this.btnMod.TabIndex = 19;
            this.btnMod.Text = "%";
            this.btnMod.UseVisualStyleBackColor = true;
            this.btnMod.Click += new System.EventHandler(this.btnmoClick);
            // 
            // btnis
            // 
            this.btnis.Location = new System.Drawing.Point(8, 240);
            this.btnis.Name = "btnis";
            this.btnis.Size = new System.Drawing.Size(63, 23);
            this.btnis.TabIndex = 18;
            this.btnis.Text = "Is(&I)";
            this.btnis.UseVisualStyleBackColor = true;
            this.btnis.Click += new System.EventHandler(this.btnisClick);
            // 
            // btnunequal
            // 
            this.btnunequal.Location = new System.Drawing.Point(78, 123);
            this.btnunequal.Name = "btnunequal";
            this.btnunequal.Size = new System.Drawing.Size(69, 23);
            this.btnunequal.TabIndex = 17;
            this.btnunequal.Text = "<>";
            this.btnunequal.UseVisualStyleBackColor = true;
            this.btnunequal.Click += new System.EventHandler(this.btnunequalClick);
            // 
            // btnge
            // 
            this.btnge.Location = new System.Drawing.Point(78, 152);
            this.btnge.Name = "btnge";
            this.btnge.Size = new System.Drawing.Size(69, 23);
            this.btnge.TabIndex = 16;
            this.btnge.Text = ">=";
            this.btnge.UseVisualStyleBackColor = true;
            this.btnge.Click += new System.EventHandler(this.btngeClick);
            // 
            // btnle
            // 
            this.btnle.Location = new System.Drawing.Point(78, 181);
            this.btnle.Name = "btnle";
            this.btnle.Size = new System.Drawing.Size(69, 23);
            this.btnle.TabIndex = 15;
            this.btnle.Text = "<=";
            this.btnle.UseVisualStyleBackColor = true;
            this.btnle.Click += new System.EventHandler(this.btnleClick);
            // 
            // uniqueList
            // 
            this.uniqueList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uniqueList.FormattingEnabled = true;
            this.uniqueList.ItemHeight = 12;
            this.uniqueList.Location = new System.Drawing.Point(223, 122);
            this.uniqueList.Name = "uniqueList";
            this.uniqueList.Size = new System.Drawing.Size(162, 112);
            this.uniqueList.TabIndex = 14;
            this.uniqueList.SelectedIndexChanged += new System.EventHandler(this.SelectUniqueValue);
            // 
            // btnGetUnique
            // 
            this.btnGetUnique.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetUnique.Location = new System.Drawing.Point(310, 240);
            this.btnGetUnique.Name = "btnGetUnique";
            this.btnGetUnique.Size = new System.Drawing.Size(75, 23);
            this.btnGetUnique.TabIndex = 13;
            this.btnGetUnique.Text = "get unique";
            this.btnGetUnique.UseVisualStyleBackColor = true;
            this.btnGetUnique.Click += new System.EventHandler(this.GetUnique);
            // 
            // btnClearExpression
            // 
            this.btnClearExpression.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearExpression.Location = new System.Drawing.Point(313, 368);
            this.btnClearExpression.Name = "btnClearExpression";
            this.btnClearExpression.Size = new System.Drawing.Size(72, 22);
            this.btnClearExpression.TabIndex = 12;
            this.btnClearExpression.Text = "clear";
            this.btnClearExpression.UseVisualStyleBackColor = true;
            this.btnClearExpression.Click += new System.EventHandler(this.ClearExpression);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(142, 283);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "WHERE";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 283);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "label2";
            // 
            // btnEqual
            // 
            this.btnEqual.Location = new System.Drawing.Point(8, 123);
            this.btnEqual.Name = "btnEqual";
            this.btnEqual.Size = new System.Drawing.Size(64, 23);
            this.btnEqual.TabIndex = 5;
            this.btnEqual.Text = "=";
            this.btnEqual.UseVisualStyleBackColor = true;
            this.btnEqual.Click += new System.EventHandler(this.btnEqualClick);
            // 
            // btnLess
            // 
            this.btnLess.Location = new System.Drawing.Point(8, 181);
            this.btnLess.Name = "btnLess";
            this.btnLess.Size = new System.Drawing.Size(63, 23);
            this.btnLess.TabIndex = 3;
            this.btnLess.Text = "<";
            this.btnLess.UseVisualStyleBackColor = true;
            this.btnLess.Click += new System.EventHandler(this.btnLessClick);
            // 
            // btnGreater
            // 
            this.btnGreater.Location = new System.Drawing.Point(8, 152);
            this.btnGreater.Name = "btnGreater";
            this.btnGreater.Size = new System.Drawing.Size(64, 23);
            this.btnGreater.TabIndex = 2;
            this.btnGreater.Text = ">";
            this.btnGreater.UseVisualStyleBackColor = true;
            this.btnGreater.Click += new System.EventHandler(this.btnGreatClick);
            // 
            // btnQuire
            // 
            this.btnQuire.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuire.Location = new System.Drawing.Point(223, 368);
            this.btnQuire.Name = "btnQuire";
            this.btnQuire.Size = new System.Drawing.Size(75, 23);
            this.btnQuire.TabIndex = 9;
            this.btnQuire.Text = "quire";
            this.btnQuire.UseVisualStyleBackColor = true;
            this.btnQuire.Click += new System.EventHandler(this.Quire);
            // 
            // select_expression
            // 
            this.select_expression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.select_expression.Location = new System.Drawing.Point(8, 305);
            this.select_expression.Multiline = true;
            this.select_expression.Name = "select_expression";
            this.select_expression.Size = new System.Drawing.Size(377, 57);
            this.select_expression.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 283);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "SELECT * FROM";
            // 
            // fieldList
            // 
            this.fieldList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fieldList.FormattingEnabled = true;
            this.fieldList.ItemHeight = 12;
            this.fieldList.Location = new System.Drawing.Point(6, 20);
            this.fieldList.Name = "fieldList";
            this.fieldList.Size = new System.Drawing.Size(371, 88);
            this.fieldList.TabIndex = 1;
            this.fieldList.SelectedIndexChanged += new System.EventHandler(this.SelectField);
            // 
            // SQLBulider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 396);
            this.Controls.Add(this.groupBox1);
            this.Name = "SQLBulider";
            this.Text = "Filter";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClearExpression;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnEqual;
        private System.Windows.Forms.Button btnLess;
        private System.Windows.Forms.Button btnGreater;
        private System.Windows.Forms.Button btnQuire;
        private System.Windows.Forms.TextBox select_expression;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox fieldList;
        private System.Windows.Forms.Button btnGetUnique;
        private System.Windows.Forms.ListBox uniqueList;
        private System.Windows.Forms.Button btnkh;
        private System.Windows.Forms.Button btnK;
        private System.Windows.Forms.Button btnN;
        private System.Windows.Forms.Button btnnot;
        private System.Windows.Forms.Button btnor;
        private System.Windows.Forms.Button btnUnderLine;
        private System.Windows.Forms.Button btnMod;
        private System.Windows.Forms.Button btnis;
        private System.Windows.Forms.Button btnunequal;
        private System.Windows.Forms.Button btnge;
        private System.Windows.Forms.Button btnle;
    }
}
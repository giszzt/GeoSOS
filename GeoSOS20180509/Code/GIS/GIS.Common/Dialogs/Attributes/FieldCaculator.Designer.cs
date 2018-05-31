namespace GIS.Common.Dialogs
{
    /// <summary>
    /// 设计器生成部分类
    /// </summary>
    partial class FieldCaculator
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.newColumnText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fieldList = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.symbol = new System.Windows.Forms.Label();
            this.var2Text = new System.Windows.Forms.TextBox();
            this.var1Text = new System.Windows.Forms.TextBox();
            this.caculateMode = new System.Windows.Forms.ListBox();
            this.btnCaculate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // newColumnText
            // 
            this.newColumnText.Location = new System.Drawing.Point(5, 233);
            this.newColumnText.Name = "newColumnText";
            this.newColumnText.Size = new System.Drawing.Size(117, 21);
            this.newColumnText.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 218);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "New Column Name:";
            // 
            // fieldList
            // 
            this.fieldList.FormattingEnabled = true;
            this.fieldList.ItemHeight = 12;
            this.fieldList.Location = new System.Drawing.Point(5, 12);
            this.fieldList.Name = "fieldList";
            this.fieldList.Size = new System.Drawing.Size(216, 184);
            this.fieldList.TabIndex = 5;
            this.fieldList.SelectedIndexChanged += new System.EventHandler(this.SelectField);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(128, 236);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "=";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(147, 218);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "var1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(270, 218);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "var2";
            // 
            // symbol
            // 
            this.symbol.AutoSize = true;
            this.symbol.Location = new System.Drawing.Point(251, 236);
            this.symbol.Name = "symbol";
            this.symbol.Size = new System.Drawing.Size(11, 12);
            this.symbol.TabIndex = 9;
            this.symbol.Text = "+";
            // 
            // var2Text
            // 
            this.var2Text.Location = new System.Drawing.Point(268, 233);
            this.var2Text.Name = "var2Text";
            this.var2Text.Size = new System.Drawing.Size(100, 21);
            this.var2Text.TabIndex = 10;
            // 
            // var1Text
            // 
            this.var1Text.Location = new System.Drawing.Point(145, 233);
            this.var1Text.Name = "var1Text";
            this.var1Text.Size = new System.Drawing.Size(100, 21);
            this.var1Text.TabIndex = 11;
            // 
            // caculateMode
            // 
            
            this.caculateMode.FormattingEnabled = true;
            this.caculateMode.Items.AddRange(new object[] {
            "+",
            "-",
            "*",
            "/"});
            this.caculateMode.Location = new System.Drawing.Point(227, 131);
            this.caculateMode.Name = "caculateMode";
            this.caculateMode.Size = new System.Drawing.Size(47, 68);
            this.caculateMode.TabIndex = 12;
            this.caculateMode.SelectedValueChanged += new System.EventHandler(this.ChangeCaculateMode);
            // 
            // btnCaculate
            // 
            this.btnCaculate.Location = new System.Drawing.Point(286, 260);
            this.btnCaculate.Name = "btnCaculate";
            this.btnCaculate.Size = new System.Drawing.Size(73, 21);
            this.btnCaculate.TabIndex = 13;
            this.btnCaculate.Text = "Caculate";
            this.btnCaculate.UseVisualStyleBackColor = true;
            this.btnCaculate.Click += new System.EventHandler(this.Caculate);
            // 
            // FieldCaculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 293);
            this.Controls.Add(this.btnCaculate);
            this.Controls.Add(this.caculateMode);
            this.Controls.Add(this.var1Text);
            this.Controls.Add(this.var2Text);
            this.Controls.Add(this.symbol);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.fieldList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.newColumnText);
            this.Name = "FieldCaculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox newColumnText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox fieldList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label symbol;
        private System.Windows.Forms.TextBox var2Text;
        private System.Windows.Forms.TextBox var1Text;
        private System.Windows.Forms.ListBox caculateMode;
        private System.Windows.Forms.Button btnCaculate;
    }
}

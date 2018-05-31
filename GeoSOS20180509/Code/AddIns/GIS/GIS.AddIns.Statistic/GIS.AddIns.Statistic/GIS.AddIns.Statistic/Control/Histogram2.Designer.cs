namespace GIS.AddIns.Statistic.Control
{
    partial class Histogram2
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
            this.DataSelect = new System.Windows.Forms.GroupBox();
            this.ChooseInterval = new System.Windows.Forms.Label();
            this.FieldDatatype = new System.Windows.Forms.Label();
            this.cmbInterval = new System.Windows.Forms.ComboBox();
            this.Cbx_datatype = new System.Windows.Forms.ComboBox();
            this.cmbStasticField = new System.Windows.Forms.ComboBox();
            this.StatistField = new System.Windows.Forms.Label();
            this.DataSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataSelect
            // 
            this.DataSelect.Controls.Add(this.ChooseInterval);
            this.DataSelect.Controls.Add(this.FieldDatatype);
            this.DataSelect.Controls.Add(this.cmbInterval);
            this.DataSelect.Controls.Add(this.Cbx_datatype);
            this.DataSelect.Controls.Add(this.cmbStasticField);
            this.DataSelect.Controls.Add(this.StatistField);
            this.DataSelect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.DataSelect.Location = new System.Drawing.Point(1, 10);
            this.DataSelect.Name = "DataSelect";
            this.DataSelect.Size = new System.Drawing.Size(321, 209);
            this.DataSelect.TabIndex = 21;
            this.DataSelect.TabStop = false;
            this.DataSelect.Text = "DataSelect";
            // 
            // ChooseInterval
            // 
            this.ChooseInterval.AutoSize = true;
            this.ChooseInterval.Location = new System.Drawing.Point(9, 157);
            this.ChooseInterval.Name = "ChooseInterval";
            this.ChooseInterval.Size = new System.Drawing.Size(89, 12);
            this.ChooseInterval.TabIndex = 5;
            this.ChooseInterval.Text = "ChooseInterval";
            // 
            // FieldDatatype
            // 
            this.FieldDatatype.AutoSize = true;
            this.FieldDatatype.Location = new System.Drawing.Point(9, 50);
            this.FieldDatatype.Name = "FieldDatatype";
            this.FieldDatatype.Size = new System.Drawing.Size(83, 12);
            this.FieldDatatype.TabIndex = 4;
            this.FieldDatatype.Text = "FieldDatatype";
            // 
            // cmbInterval
            // 
            this.cmbInterval.FormattingEnabled = true;
            this.cmbInterval.Location = new System.Drawing.Point(104, 149);
            this.cmbInterval.Name = "cmbInterval";
            this.cmbInterval.Size = new System.Drawing.Size(121, 20);
            this.cmbInterval.TabIndex = 8;
            this.cmbInterval.SelectedIndexChanged += new System.EventHandler(this.cmbInterval_SelectedIndexChanged);
            // 
            // Cbx_datatype
            // 
            this.Cbx_datatype.FormattingEnabled = true;
            this.Cbx_datatype.Items.AddRange(new object[] {
            "Quantities",
            "Categories"});
            this.Cbx_datatype.Location = new System.Drawing.Point(104, 42);
            this.Cbx_datatype.Name = "Cbx_datatype";
            this.Cbx_datatype.Size = new System.Drawing.Size(159, 20);
            this.Cbx_datatype.TabIndex = 8;
            this.Cbx_datatype.SelectedIndexChanged += new System.EventHandler(this.Cbx_datatype_SelectedIndexChanged);
            // 
            // cmbStasticField
            // 
            this.cmbStasticField.FormattingEnabled = true;
            this.cmbStasticField.Location = new System.Drawing.Point(104, 101);
            this.cmbStasticField.Name = "cmbStasticField";
            this.cmbStasticField.Size = new System.Drawing.Size(159, 20);
            this.cmbStasticField.TabIndex = 8;
            this.cmbStasticField.SelectedIndexChanged += new System.EventHandler(this.Cbx_datatype_SelectedIndexChanged);
            // 
            // StatistField
            // 
            this.StatistField.AutoSize = true;
            this.StatistField.Location = new System.Drawing.Point(9, 104);
            this.StatistField.Name = "StatistField";
            this.StatistField.Size = new System.Drawing.Size(77, 12);
            this.StatistField.TabIndex = 0;
            this.StatistField.Text = "StatistField";
            // 
            // Histogram2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DataSelect);
            this.Name = "Histogram2";
            this.Size = new System.Drawing.Size(323, 229);
            this.DataSelect.ResumeLayout(false);
            this.DataSelect.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox DataSelect;
        private System.Windows.Forms.Label ChooseInterval;
        private System.Windows.Forms.Label FieldDatatype;
        private System.Windows.Forms.ComboBox cmbInterval;
        private System.Windows.Forms.ComboBox Cbx_datatype;
        private System.Windows.Forms.ComboBox cmbStasticField;
        private System.Windows.Forms.Label StatistField;
    }
}

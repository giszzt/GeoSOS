using System.ComponentModel;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// A control that is specifically designed to allow choosing a font family name
    /// </summary>
    partial class FontFamilyControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FontFamilyControl));
            this.ffdNames = new GIS.Common.Dialogs.FontFamilyDropDown();
            this.SuspendLayout();
            // 
            // ffdNames
            // 
            resources.ApplyResources(this.ffdNames, "ffdNames");
            this.ffdNames.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ffdNames.FormattingEnabled = true;
            this.ffdNames.Name = "ffdNames";
            // 
            // FontFamilyControl
            // 
            this.AutoScaleMode = this.AutoScaleMode;
            this.Controls.Add(this.ffdNames);
            this.Name = "FontFamilyControl";
            resources.ApplyResources(this, "$this");
            this.ResumeLayout(false);

        }

        #endregion

        private FontFamilyDropDown ffdNames;
    }
}
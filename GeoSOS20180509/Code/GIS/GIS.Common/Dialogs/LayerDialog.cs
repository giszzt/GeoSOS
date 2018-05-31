using System;
using System.Data;
using System.Windows.Forms;
using DotSpatial.Symbology;

namespace GIS.Common.Dialogs
{
    /// <summary>
    /// This is a basic form which is displayed when the user double-clicks on a layer name
    /// in the legend
    /// </summary>
    public class LayerDialog : Form
    {
        private ICategoryControl _rasterCategoryControl;
        private DialogButtons dialogButtons1;
        private Panel panel1;
        private Panel pnlContent;
        private PropertyGrid propertyGrid1;
        private TabControl tabControl1;
        private TabPage tabDetails;
        private TabPage tabSymbology;

        #region Events

        /// <summary>
        /// Occurs when the apply changes situation forces the symbology to become updated.
        /// </summary>
        public event EventHandler ChangesApplied;

        #endregion

        #region Private Variables

        private ILayer _layer;
        //static private IRasterLayer layer;
        //static private IRasterSymbolizer _symbolizer;
        //private RasterCategoryControl RasCon = new RasterCategoryControl(layer, _symbolizer);
        
        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of LayerDialog
        /// </summary>
        public LayerDialog()
        {
            
            InitializeComponent();
        }

        /// <summary>
        /// Creates a new instance of LayerDialog form to display the symbology and
        /// other properties of the specified feature layer
        /// </summary>
        /// <param name="selectedLayer">the specified feature layer that is
        /// modified using this form</param>
        /// <param name="control">The control.</param>
        public LayerDialog(ILayer selectedLayer, ICategoryControl control)
            : this()
        {
            _layer = selectedLayer;
            propertyGrid1.SelectedObject = _layer;
            Configure(control);
        }

        private void Configure(ICategoryControl control)
        {
            var userControl = control as UserControl;
            userControl.Parent = pnlContent;
            userControl.Visible = true;

            _rasterCategoryControl = control;
            _rasterCategoryControl.Initialize(_layer);
        }

        #endregion

        #region Methods

        #endregion

        #region Properties

        #endregion

        #region Event Handlers

        #endregion

        #region Protected Methods

        #endregion

        #region Component Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayerDialog));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSymbology = new System.Windows.Forms.TabPage();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.tabDetails = new System.Windows.Forms.TabPage();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dialogButtons1 = new DialogButtons();
            this.tabControl1.SuspendLayout();
            this.tabSymbology.SuspendLayout();
            this.tabDetails.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabSymbology);
            this.tabControl1.Controls.Add(this.tabDetails);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabSymbology
            // 
            this.tabSymbology.Controls.Add(this.pnlContent);
            resources.ApplyResources(this.tabSymbology, "tabSymbology");
            this.tabSymbology.Name = "tabSymbology";
            this.tabSymbology.UseVisualStyleBackColor = true;
            // 
            // pnlContent
            // 
            //pnlContent.Controls.Add(RasCon);
            resources.ApplyResources(this.pnlContent, "pnlContent");
            this.pnlContent.Name = "pnlContent";
            // 
            // tabDetails
            // 
            this.tabDetails.Controls.Add(this.propertyGrid1);
            resources.ApplyResources(this.tabDetails, "tabDetails");
            this.tabDetails.Name = "tabDetails";
            this.tabDetails.UseVisualStyleBackColor = true;
            // 
            // propertyGrid1
            // 
            resources.ApplyResources(this.propertyGrid1, "propertyGrid1");
            this.propertyGrid1.Name = "propertyGrid1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dialogButtons1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // dialogButtons1
            // 
            resources.ApplyResources(this.dialogButtons1, "dialogButtons1");
            this.dialogButtons1.Name = "dialogButtons1";
            this.dialogButtons1.OkClicked += new System.EventHandler(this.dialogButtons1_OkClicked);
            this.dialogButtons1.ApplyClicked += new System.EventHandler(this.dialogButtons1_ApplyClicked);
            this.dialogButtons1.CancelClicked += new System.EventHandler(this.dialogButtons1_CancelClicked);
            // 
            // LayerDialog
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LayerDialog";
            this.ShowInTaskbar = false;
            this.tabControl1.ResumeLayout(false);
            this.tabSymbology.ResumeLayout(false);
            this.tabDetails.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void dialogButtons1_ApplyClicked(object sender, EventArgs e)
        {
            OnApplyChanges();
        }

        private void dialogButtons1_CancelClicked(object sender, EventArgs e)
        {
            _rasterCategoryControl.Cancel();
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        /// <summary>
        /// Forces changes to be written from the copy symbology to
        /// the original, updating the map display.
        /// </summary>
        public void ApplyChanges()
        {

            try
            {
                OnApplyChanges();
            }
            catch (SyntaxErrorException)
            {
                MessageBox.Show("The expression you provided for one of the rows is not valid. Try revising the expression to look like '[FieldId] >= Value'");
            }
        }

        /// <summary>
        /// Occurs during apply changes operations and is overrideable in subclasses
        /// </summary>
        protected virtual void OnApplyChanges()
        {
            _rasterCategoryControl.ApplyChanges();

            if (ChangesApplied != null) ChangesApplied(_layer, EventArgs.Empty);
        }


        private void dialogButtons1_OkClicked(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            OnApplyChanges();
            Close();
        }
    }
}
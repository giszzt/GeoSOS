using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotSpatial.Symbology;

namespace GIS.Common.Dialogs
{
    public partial class SymbologyEditor : Form
    {
        #region Private Varieties

        FeatureLayer _lineSymbolizer;

        #endregion


        #region Construct

        public SymbologyEditor()
        {
            InitializeComponent();
        }
        public SymbologyEditor(FeatureLayer symbolizer)
        {
            InitializeComponent();
            _lineSymbolizer = symbolizer;

        }

        #endregion

        private void btnBlueLine_Click(object sender, EventArgs e)
        {
            if (_lineSymbolizer != null)
            {
                double width = 3;
                (_lineSymbolizer.Symbolizer as IPolygonSymbolizer).SetFillColor(Color.Cyan);
                _lineSymbolizer.Symbolizer.SetOutline(Color.Blue, width);
                this.Close();
                
            }
            else
            {
                MessageBox.Show("null");
                this.Close();
            }

        }
    }
}

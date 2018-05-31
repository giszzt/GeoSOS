using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Symbology;
using GIS.GDAL.Overlay;

namespace GIS.AddIns.Overlay
{
    public partial class OverlayDialog : Form
    {
        private OverlayType _type;

        public string baseLayerPath
        {
            get { return cmbBase.Text; }
        }

        public string overlayLayerPath
        {
            get { return cmbOverlay.Text; }
        }

        public string resultLayerPath
        {
            get { return tbResult.Text; }
        }

        public OverlayDialog(OverlayType type)
        {
            InitializeComponent();
            Initial(type);
        }

        private void Initial(OverlayType type)
        {
            _type = type;

            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            IMap map = GIS.FrameWork.Application.App.Map;
            for (int i = 0; i < map.Layers.Count; i++)
            {
                ILayer layer = map.Layers[i];
                if (layer is IMapPolygonLayer)
                {
                    cmbBase.Items.Add(layer.LegendText);
                    cmbOverlay.Items.Add(layer.LegendText);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cmbBase.Text) && !String.IsNullOrEmpty(cmbOverlay.Text) && !String.IsNullOrEmpty(tbResult.Text))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnBaseLayer_Click(object sender, EventArgs e)
        {
            OpenFileDialog baseLayerDialog = new OpenFileDialog();
            baseLayerDialog.Filter = "Shapefile (*.shp)|*.shp";
            baseLayerDialog.Title = "Open Shapefile for Intersection";

            if (baseLayerDialog.ShowDialog() == DialogResult.OK)
            {
                cmbBase.Text = baseLayerDialog.FileName;
            }
        }

        private void btnOverlayLayer_Click(object sender, EventArgs e)
        {
            OpenFileDialog overlayLayerDialog = new OpenFileDialog();
            overlayLayerDialog.Filter = "Shapefile (*.shp)|*.shp";
            overlayLayerDialog.Title = "Open Shapefile to intersect on";

            if (overlayLayerDialog.ShowDialog() == DialogResult.OK)
            {
                cmbOverlay.Text = overlayLayerDialog.FileName;
            }
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            SaveFileDialog resultDialog = new SaveFileDialog();
            resultDialog.Filter = "Shapefile (*.shp)|*.shp";
            resultDialog.Title = "Save as Shapefile";

            if (resultDialog.ShowDialog() == DialogResult.OK)
            {
                tbResult.Text = resultDialog.FileName;
            }
        }
    }
}

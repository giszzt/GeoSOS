using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GIS.FrameWork;
using DotSpatial.Controls;
using DotSpatial.Data;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using OxyPlot.Annotations;
using DotSpatial.Symbology;
using ICSharpCode.Core.WinForms;
using FrameWork.Gui;
using GIS.AddIns.Statistic.Control;


namespace GIS.AddIns.Statistic
{
    public partial class SetPlotForm : Form
    {
       public IFeatureLayer _featureLayer;
       IViewContent view1 = WorkbenchSingleton.Workbench.ActiveViewContent;
       
        public SetPlotForm()
        {
            InitializeComponent();
            
            cmbPlotType.Items.Add("Scatter Points");
            cmbPlotType.Items.Add("Line Series");
            cmbPlotType.Items.Add("Linear Bar Series");
            cmbPlotType.Items.Add("Histogram Series");
            cmbPlotType.Items.Add("Histogram2 Series");
        }
 

        private void cmbLayer_DropDown(object sender, EventArgs e)
        {
            cmbLayer.Items.Clear();
            foreach (IMapLayer item in GIS.FrameWork.Application.App.Map.Layers)
            {

                if (item is IFeatureLayer)
                {

                    cmbLayer.Items.Add(item.LegendText);

                }
            }
        }

        private void cmbLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (IMapLayer item in GIS.FrameWork.Application.App.Map.Layers)
            {
                if (item.LegendText == cmbLayer.Text)
                {
                    _featureLayer = item as IFeatureLayer;
                    //_featureLayer.SelectionChanged += new EventHandler(_featureLayer_SelectionChanged);
                    break;
                }
            }
        }

        private void cmbPlotType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            panel2.Controls.Clear();
            MapStatistics ss = view1.Control as MapStatistics;

            switch (cmbPlotType.SelectedIndex)
            {    
                case 0:
                {
                    ss._pm.Series.Clear();
                    ss._pm.Axes.Clear();                  
                    ss.plotView1.Refresh();
                    ss.plotView1.Invalidate();                 
                    ScatterPoints sp = new ScatterPoints(ss, _featureLayer);
                    panel2.Controls.Add(sp);
                    sp._ms.plotView1.Model.InvalidatePlot(true);
                }
                break;
                case 1:
                {
                    ss._pm.Series.Clear();
                    ss._pm.Axes.Clear();
                    ss.plotView1.Refresh();
                    ss.plotView1.Invalidate();              
                    LinePlot lp = new LinePlot(ss, _featureLayer);
                    panel2.Controls.Add(lp);
                    lp._ms.plotView1.Model.InvalidatePlot(true);
                }
                break;
                case 2:
                {
                    ss._pm.Series.Clear();
                    ss._pm.Axes.Clear();
                    ss.plotView1.Refresh();
                    ss.plotView1.Invalidate();
                    LineBarPlot lb = new LineBarPlot(ss, _featureLayer);
                    panel2.Controls.Add(lb);
                    lb._ms.plotView1.Model.InvalidatePlot(true);
                }
                break;
                case 3:
                {

                    ss._pm.Series.Clear(); 
                    ss._pm.Axes.Clear();
                    ss.plotView1.Refresh();
                    ss.plotView1.Invalidate();
                    Histogram Hg = new Histogram(ss, _featureLayer);
                    panel2.Controls.Add(Hg);
                    Hg._ms.plotView1.Model.InvalidatePlot(true);
                }
                break;
                case 4:
                {

                    ss._pm.Series.Clear();
                    ss._pm.Axes.Clear();
                    ss.plotView1.Refresh();
                    ss.plotView1.Invalidate();
                    Histogram2 Hg2 = new Histogram2(ss, _featureLayer);
                    panel2.Controls.Add(Hg2);
                    Hg2._ms.plotView1.Model.InvalidatePlot(true);
                }
                break;

            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            switch (cmbPlotType.SelectedIndex)
            {
                case 0:
                    {

                        (this.panel2.Controls[0] as ScatterPoints).DrawPlot();
                    }
                    break;

                case 1:
                    {

                        (this.panel2.Controls[0] as LinePlot).DrawPlot();
                    }
                    break;
                case 2:
                    {

                        (this.panel2.Controls[0] as LineBarPlot).DrawPlot();
                    }
                    break;
                case 3:
                    {
                        (this.panel2.Controls[0] as Histogram).DrawPlot();
                    }
                    break;
                case 4:
                    {
                        (this.panel2.Controls[0] as Histogram2).DrawPlot();
                    }
                    break;

            }
            //if (cmbPlotType.Text == "Scatter Points")
            //{
            //    (this.panel2.Controls[0] as ScatterPoints).DrawPlot();
            //}
            //else if (cmbPlotType.Text == "Line Series")
            //{
            //    (this.panel2.Controls[0] as LinePlot).DrawPlot();
            //}
            this.Close();
        }

        //private void _featureLayer_SelectionChanged(object sender, EventArgs e)
        //{
        //    //if (this.IsDisposed == false)
        //    //{
        //        switch (cmbPlotType.SelectedIndex)
        //        {
        //            case 0:
        //                {

        //                    (this.panel2.Controls[0] as ScatterPoints).feature_selected();
        //                }
        //                break;

        //            case 1:
        //                {

        //                    (this.panel2.Controls[0] as LinePlot).feature_selected();
        //                }
        //                break;
        //            case 2:
        //                {

        //                    (this.panel2.Controls[0] as LineBarPlot).feature_selected();
        //                }
        //                break;
        //        //}
        //        //(this.panel2.Controls[0] as ScatterPoints).feature_selected();

        //    }
      
        //}

        private void btnReset_Click(object sender, EventArgs e)
        {
            switch (cmbPlotType.SelectedIndex)
            {
                case 0:
                    {

                        (this.panel2.Controls[0] as ScatterPoints).reset();
                    }
                    break;

                case 1:
                    {

                        (this.panel2.Controls[0] as LinePlot).ReSet();
                    }
                    break;
                case 2:
                    {

                        (this.panel2.Controls[0] as LineBarPlot).ReSet();
                    }
                    break;
                case 4:
                    {
                        (this.panel2.Controls[0] as Histogram2).ReSet();
                    }
                    break;
                    
            }
            //(this.panel2.Controls[0] as ScatterPoints).reset(); 
            
        }

    }
}

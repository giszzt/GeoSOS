using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Symbology;
using GeoAPI.Geometries;
using DotSpatial.Projections;
using ICSharpCode.Core;


namespace GIS.Common.Dialogs
{
    public partial class Buffer : Form
    {
        #region Const String

        string _buffer = StringParser.Parse("${res:GIS.Common.Dialogs.Buffer}");
        string _ok = StringParser.Parse("${res:UI.Button.OK}");
        string _dis = StringParser.Parse("${res:GIS.Common.Dialogs.Buffer.Distance}");
        string _unit = StringParser.Parse("${res:GIS.Common.Dialogs.Buffer.Unit}");


        #endregion

        #region Private Variable

        private string _bufferType;
        private IMap _map = GIS.FrameWork.Application.App.Map;
        private double _distance;

        #endregion

        #region Construct
        public Buffer()
        {
            InitializeComponent();
        }

        public Buffer(string bufferType)
        {
            InitializeComponent();
            _bufferType = bufferType;

            this.Text = _buffer;
            lbDistance.Text = _dis;
            lbUnit.Text = _unit;
            btnOk.Text = _ok;

        }

        #endregion

        #region Event

        private void ok_Click(object sender, EventArgs e)
        {
            if((double.TryParse(disValue.Text,out _distance)) == false)
            {
                MessageBox.Show("Please input valid value");
                disValue.Clear();
            }
            else
            {
                if (_bufferType == "LineBuffer") { lineBuffer(); }
                else if (_bufferType == "PointBuffer") { pointBuffer(); }
                else if (_bufferType == "PolygonBuffer") { polygonBuffer(); }
                this.Close();
            }
            
        }

        #endregion

        #region Method

        private ILayer bufferLayer()
        {
            foreach (ILayer item in _map.MapFrame.DrawingLayers)
            {
                if (item.LegendText == "ROI")
                {
                    return item;
                }
            }
            ILayer layer = new MapPolygonLayer();
            layer.LegendText = "ROI";
            (layer as IPolygonLayer).Symbolizer = GIS.FrameWork.ROIConfigure.bufferSymbolizer;
            _map.MapFrame.DrawingLayers.Insert(0, layer);
            return layer;
        }

        private void lineBuffer()
        {
            foreach (ILayer item in _map.MapFrame.DrawingLayers)
            {
                if (item.LegendText == "Line")
                {                    
                    IMapLineLayer lineLayer = item as IMapLineLayer;
                    ILayer layer = bufferLayer();
                    for (int i = 0; i< lineLayer.DataSet.Features.Count; i++)
                    {
                        (layer as IMapPolygonLayer).DataSet.AddFeature(lineLayer.DataSet.Features[i].Geometry.Buffer(_distance));
                    }
                    _map.Refresh();
                    break;
                }
            }
        }

        private void pointBuffer()
        {
            foreach (ILayer item in _map.MapFrame.DrawingLayers)
            {
                if (item.LegendText == "Point")
                {
                    IMapPointLayer pointLayer = item as IMapPointLayer;
                    ILayer layer = bufferLayer();
                    for (int i = 0; i < pointLayer.DataSet.Features.Count; i++)
                    {
                        (layer as IMapPolygonLayer).DataSet.AddFeature(pointLayer.DataSet.Features[i].Geometry.Buffer(_distance));
                    }
                    _map.Refresh();
                    break;
                }
            }
        }

        private void polygonBuffer()
        {
            foreach (ILayer item in _map.MapFrame.DrawingLayers)
            {
                if (item.LegendText == "Polygon")
                {
                    IMapPolygonLayer polygonLayer = item as IMapPolygonLayer;
                    ILayer layer = bufferLayer();
                    for (int i = 0; i < polygonLayer.DataSet.Features.Count; i++)
                    {
                        (layer as IMapPolygonLayer).DataSet.AddFeature(polygonLayer.DataSet.Features[i].Geometry.Buffer(_distance));
                    }
                    _map.Refresh();
                    break;
                }
            }
        }

        #endregion
    }
}

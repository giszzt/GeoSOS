using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Symbology;

namespace MultiPlan
{
    public partial class ConflictConsultControl : UserControl
    {
        public ConflictConsultControl()
        {
            InitializeComponent();
            LoadConflicts();
        }

        #region 变量
        IFeatureSet currentLayer;
        IFeature featureA;
        IFeature featureB;
        IFeature featureConflict;
        IFeatureSet featureLayerConflict;
        IFeatureSet featureLayerA;
        IFeatureSet featureLayerB;

        string AFieldName = string.Empty;//规划A的标识码
        string BFieldName = string.Empty;//规划B的标识码
        string ConflictA = string.Empty;//与AFieldName对应的冲突图层的字段
        string ConflictB = string.Empty;//与BFieldName对应的冲突图层的字段

        Rule rule = new Rule();
        PlanTree planTree = new PlanTree();
        RuleTree ruleTree = new RuleTree();
        List<Conflict> conflicts = new List<Conflict>();
        //ConflictFeature conflicFeature;

        //private int comboBoxColumnIndex1 = 0; // 处理意见列
        //private int comboBoxColumnIndex2 = 0; // 处理意见列

        #endregion

        /// <summary>
        /// 加载冲突
        /// </summary>
        private void LoadConflicts()
        {
            //conflicFeature = new ConflictFeature();
            //conflicFeature.SetSymbol();//设置符号

            conflicts = ruleTree.GetConflicts();
            foreach (Conflict item in conflicts)
            {
                xmlComboBox.Items.Add(item.Name);
            }
        }

        /// <summary>
        /// 选择冲突
        /// </summary>
        private void SelectConflict(object sender, EventArgs e)
        {
            foreach (Conflict item in conflicts)
            {
                if (string.Equals(xmlComboBox.SelectedItem.ToString(), item.Name))
                {
                    string conflictAdd = item.Address;
                    //"C:\\Users\\Administrator.ZX-201609231534\\Desktop\\testdata\\c1.shp"
                    featureLayerConflict = CommonMethod.GetFeatureSetByPath(conflictAdd);
                    featureLayerConflict.Name = item.Name;
                    groupBoxConflict.Text = "冲突图层，类型：" + item.ConflictType;

                    //bool isSDEA;
                    item.ZoneA = planTree.GetZone(item.ZoneA.Name.Split('.')[0], item.ZoneA.Name.Split('.')[1]);
                    //string zoneAAdd = planTree.GetZoneAddress(item.zoneA.name.Split('.')[0], item.zoneA.name.Split('.')[1], out isSDEA);
                    string zoneAAdd = item.ZoneA.Address;
                    AFieldName = item.ZoneA.BSM + "_BSM";
                    ConflictA = AFieldName;
                    featureLayerA = CommonMethod.GetFeatureSetByPath(zoneAAdd);
                    featureLayerA.Name = item.ZoneA.Name;
                    groupBoxA.Text = item.ZoneA.Name;

                    //bool isSDEB;
                    item.ZoneB = planTree.GetZone(item.ZoneB.Name.Split('.')[0], item.ZoneB.Name.Split('.')[1]);
                    //string zoneBAdd = planTree.GetZoneAddress(item.zoneB.name.Split('.')[0], item.zoneB.name.Split('.')[1], out isSDEB);
                    string zoneBAdd = item.ZoneB.Address;
                    BFieldName = item.ZoneB.BSM + "_BSM";
                    ConflictB = BFieldName;
                    featureLayerB = CommonMethod.GetFeatureSetByPath(zoneBAdd);
                    featureLayerB.Name = item.ZoneB.Name;
                    groupBoxB.Text = item.ZoneB.Name;

                    associateLayer(featureLayerA, featureLayerB);
                    break;
                }
            }
        }

        private void associateLayer(IFeatureSet aLayer, IFeatureSet bLayer)
        {

            if (featureLayerConflict != null)
            {
                #region 加载一个临时内存图层，为空间范围划定服务
                //IWorkspaceFactory workspaceFactory = new InMemoryWorkspaceFactoryClass();
                //IWorkspaceName workspaceName = workspaceFactory.Create("", "tempWorkspace", null, 0);
                //IName name = (IName)workspaceName;
                //IWorkspace inmemWor = (IWorkspace)name.Open();

                //IField oField = new FieldClass();
                //IFields oFields = new FieldsClass();
                //IFieldsEdit oFieldsEdit = null;
                //IFieldEdit oFieldEdit = null;

                //oFieldsEdit = oFields as IFieldsEdit;
                //oFieldEdit = oField as IFieldEdit;

                //IGeometryDef geometryDef = new GeometryDefClass();
                //IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
                //geometryDefEdit.AvgNumPoints_2 = 5;
                //geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
                //geometryDefEdit.GridCount_2 = 1;
                //geometryDefEdit.HasM_2 = false;
                //geometryDefEdit.HasZ_2 = false;
                //geometryDefEdit.SpatialReference_2 = (featureLayerConflict.FeatureClass as IGeoDataset).SpatialReference;

                //oFieldEdit.Name_2 = "SHAPE";
                //oFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                //oFieldEdit.GeometryDef_2 = geometryDef;
                //oFieldEdit.IsNullable_2 = true;
                //oFieldEdit.Required_2 = true;
                //oFieldsEdit.AddField(oField);

                //IFeatureClass featureClass = (inmemWor as IFeatureWorkspace).CreateFeatureClass("tempLayer", oFields, null, null, esriFeatureType.esriFTSimple, "SHAPE", "");
                //IFeatureLayer featurelayer = new FeatureLayerClass();
                //featurelayer.Name = "临时图层";
                //featurelayer.FeatureClass = featureClass;

                //ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbolClass();
                //simpleFillSymbol.Style = esriSimpleFillStyle.esriSFSNull;
                //ILineSymbol lineSymbol = new SimpleLineSymbolClass();
                //RgbColorClass rgbcolor = new RgbColorClass();
                //rgbcolor.Red = 255;
                //rgbcolor.Green = 0;
                //rgbcolor.Blue = 0;
                //lineSymbol.Color = rgbcolor as IColor;
                //lineSymbol.Width = 1;
                //simpleFillSymbol.Outline = lineSymbol;

                //ISimpleRenderer simpleRender = new SimpleRendererClass();
                //simpleRender.Symbol = simpleFillSymbol as ISymbol;

                //IGeoFeatureLayer geoFeatureLayer = featurelayer as IGeoFeatureLayer;
                //geoFeatureLayer.Renderer = simpleRender as IFeatureRenderer;

                //axMapControlConflict.AddLayer(featurelayer as ILayer, 0);
                #endregion
                IMapPolygonLayer polyLayer1 = new MapPolygonLayer(featureLayerA);             
                IPolygonSymbolizer polySymbolizer1 = new PolygonSymbolizer(Color.FromArgb(0, Color.Yellow), Color.Red, 1);
                polyLayer1.Symbolizer = polySymbolizer1;
                this.mapConflict.MapFrame.DrawingLayers.Add(polyLayer1);

                IMapPolygonLayer polyLayer2 = new MapPolygonLayer(featureLayerB);
                IPolygonSymbolizer polySymbolizer2 = new PolygonSymbolizer(Color.FromArgb(0, Color.Yellow), Color.Purple, 1);
                polyLayer2.Symbolizer = polySymbolizer2;
                this.mapConflict.MapFrame.DrawingLayers.Add(polyLayer2);

                this.mapConflict.Refresh();
            }
            this.mapConflict.ClearLayers();
            this.mapConflict.Layers.Add(featureLayerConflict);

            this.mapA.ClearLayers();
            this.mapA.Layers.Add(aLayer);

            this.mapB.ClearLayers();
            this.mapB.Layers.Add(bLayer);

            if (featureLayerConflict != null)
            {
                //LoadAttributeTable(featureLayerConflict.FeatureClass);
            }
        }

        private void mapConflict_ViewExtentChanged(object sender, EventArgs e)
        {
            this.mapA.ViewExtents.SetValues(this.mapConflict.ViewExtents.MinX, this.mapConflict.ViewExtents.MinY, this.mapConflict.ViewExtents.MaxX, this.mapConflict.ViewExtents.MaxY);
            this.mapB.ViewExtents.SetValues(this.mapConflict.ViewExtents.MinX, this.mapConflict.ViewExtents.MinY, this.mapConflict.ViewExtents.MaxX, this.mapConflict.ViewExtents.MaxY);
            this.mapNow.ViewExtents.SetValues(this.mapConflict.ViewExtents.MinX, this.mapConflict.ViewExtents.MinY, this.mapConflict.ViewExtents.MaxX, this.mapConflict.ViewExtents.MaxY);
            this.mapA.Refresh();
            this.mapB.Refresh();
            this.mapNow.Refresh();
        }

        private void mapA_Click(object sender, EventArgs e)
        { 
            if (GIS.FrameWork.Application.App.Map == null || (GIS.FrameWork.Application.App.Map as Map).Name != mapA.Name)
            {
                GIS.FrameWork.Application.App.Map = mapA;
                if (GIS.FrameWork.Application.App.Legend != null)
                {
                    GIS.FrameWork.Application.App.Legend.RootNodes.Clear();
                }
                mapA.Legend = GIS.FrameWork.Application.App.Legend;  
            }
        }

        private void mapConflict_Click(object sender, EventArgs e)
        {
            if (GIS.FrameWork.Application.App.Map == null || (GIS.FrameWork.Application.App.Map as Map).Name != mapConflict.Name)
            {
                GIS.FrameWork.Application.App.Map = mapConflict;
                if (GIS.FrameWork.Application.App.Legend != null)
                {
                    GIS.FrameWork.Application.App.Legend.RootNodes.Clear();
                }
                mapConflict.Legend = GIS.FrameWork.Application.App.Legend;
            }
        }

        private void mapB_Click(object sender, EventArgs e)
        {
            if (GIS.FrameWork.Application.App.Map == null || (GIS.FrameWork.Application.App.Map as Map).Name != mapB.Name)
            {
                GIS.FrameWork.Application.App.Map = mapB;
                if (GIS.FrameWork.Application.App.Legend != null)
                {
                    GIS.FrameWork.Application.App.Legend.RootNodes.Clear();
                }
                mapB.Legend = GIS.FrameWork.Application.App.Legend;
            }
        }

        private void mapNow_Click(object sender, EventArgs e)
        {
            if (GIS.FrameWork.Application.App.Map == null || (GIS.FrameWork.Application.App.Map as Map).Name != mapNow.Name)
            {
                GIS.FrameWork.Application.App.Map = mapNow;
                if (GIS.FrameWork.Application.App.Legend != null)
                {
                    GIS.FrameWork.Application.App.Legend.RootNodes.Clear();
                }
                mapNow.Legend = GIS.FrameWork.Application.App.Legend;
            }
        }
    }
}

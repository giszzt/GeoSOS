using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DotSpatial.Controls;
using DotSpatial.Data;
using OSGeo.OSR;
using OSGeo.OGR;
using GIS.GDAL.Overlay;
using GIS.FrameWork;

namespace MultiPlan
{
    public class Conflict
    {
        /// <summary>
        /// 冲突名称
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// 冲突类型
        /// </summary>
        public string ConflictType
        {
            get
            {
                return _conflictType;
            }
            set
            {
                _conflictType = value;
            }
        }

        /// <summary>
        /// 冲突结果保存位置
        /// </summary>
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        }

        /// <summary>
        /// 管控区A
        /// </summary>
        public ControlZone ZoneA
        {
            get
            {
                return _zoneA;
            }
            set
            {
                _zoneA = value;
            }
        }

        /// <summary>
        /// 管控区B
        /// </summary>
        public ControlZone ZoneB
        {
            get
            {
                return _zoneB;
            }
            set
            {
                _zoneB = value;
            }
        }

        string _name;//冲突名称
        string _conflictType;//冲突类型
        string _address;//冲突结果保存位置
        ControlZone _zoneA = new ControlZone();//管控区A
        ControlZone _zoneB = new ControlZone();//管控区B

        public bool ConflictAnalysis()
        {
            IFeatureSet resultSet = null;
            try
            {
                #region 相交分析

                OSGeo.OGR.Layer layerA = null;
                if (_zoneA.FeatureSet != null)
                {
                    layerA = GIS.GDAL.VectorConverter.DS2OrgLayer(_zoneA.FeatureSet);
                }
                else
                {
                    layerA = GIS.GDAL.VectorConverter.GetOgrLayer(_zoneA.Address);
                }

                OSGeo.OGR.Layer layerB = null;
                if (_zoneB.FeatureSet != null)
                {
                    layerB = GIS.GDAL.VectorConverter.DS2OrgLayer(_zoneB.FeatureSet);
                }
                else
                {
                    layerB = GIS.GDAL.VectorConverter.GetOgrLayer(_zoneB.Address);
                }

                OSGeo.OGR.Layer resultLayer = null;
                using (OSGeo.OGR.Driver driver = Ogr.GetDriverByName("ESRI Shapefile"))
                {
                    if (driver == null) System.Environment.Exit(-1);
                    string[] resultPath = _address.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                    string resultName = resultPath[resultPath.Length - 1];
                    using (var dsResult = driver.CreateDataSource(_address, new string[] { }))
                    {
                        if (dsResult == null) throw new Exception("Can't get to the datasoure.");

                        for (int i = 0; i < dsResult.GetLayerCount(); i++)
                        {
                            resultLayer = dsResult.GetLayerByIndex(i);
                            if (resultLayer != null && resultLayer.GetLayerDefn().GetName() == resultName)
                            {
                                dsResult.DeleteLayer(i);
                                break;
                            }
                        }
                        SpatialReference reference = layerA.GetSpatialRef();
                        FeatureDefn definition = layerA.GetLayerDefn();
                        wkbGeometryType type = definition.GetGeomType();
                        resultLayer = dsResult.CreateLayer("ResultLayer", layerA.GetSpatialRef(), layerA.GetLayerDefn().GetGeomType(), new string[] { });

                        bool intersectSuccess = GIS.GDAL.Overlay.Overlay.OverlayOperate(layerA, layerB, ref resultLayer, OverlayType.Intersects, null);

                        if (!intersectSuccess)
                        {
                            return false;
                        }
                    }
                }

                #endregion

                resultSet = DotSpatial.Data.DataManager.DefaultDataManager.OpenFile(_address) as IFeatureSet;

                #region 添加转换要素的字段

                DataTable resultTable = resultSet.DataTable;

                DataColumn conflictColumn = new DataColumn();
                conflictColumn.DataType = typeof(string);
                conflictColumn.ColumnName = "冲突类型";
                conflictColumn.MaxLength = 100;
                resultTable.Columns.Add(conflictColumn);

                DataColumn conflictColumn2 = new DataColumn();
                conflictColumn2.DataType = typeof(string);
                conflictColumn2.ColumnName = "处理意见";
                conflictColumn2.MaxLength = 100;
                resultTable.Columns.Add(conflictColumn2);

                DataColumn conflictColumn3 = new DataColumn();
                conflictColumn3.DataType = typeof(string);
                conflictColumn3.ColumnName = "备注";
                conflictColumn3.MaxLength = 50;
                resultTable.Columns.Add(conflictColumn3);

                DataColumn conflictColumn4 = new DataColumn();
                conflictColumn4.DataType = typeof(string);
                conflictColumn4.ColumnName = "用地类型";
                conflictColumn4.MaxLength = 50;
                resultTable.Columns.Add(conflictColumn4);

                #endregion

                int index = resultTable.Columns.IndexOf("冲突类型");
                for (int i = 0; i < resultTable.Rows.Count; i++)
                {
                    DataRow dataRow = resultTable.Rows[i];
                    dataRow[index] = _conflictType;
                }
                
                resultSet.Save();
                if (resultSet.Projection == null)
                {

                }
                GIS.FrameWork.Application.App.Map.Layers.Add(resultSet);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

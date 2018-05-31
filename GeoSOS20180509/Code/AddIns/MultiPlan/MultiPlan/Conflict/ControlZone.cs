using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;
using System.Text.RegularExpressions;
using DotSpatial.Data;

namespace MultiPlan
{
    public class ControlZone
    {
        #region 变量

        /// <summary>
        /// 管控区名称
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
        /// 标记字段，用于区分要素的字段
        /// </summary>
        public string UniqueField
        {
            get
            {
                return _uniqueField;
            }
            set
            {
                _uniqueField = value;
            }
        }

        /// <summary>
        /// 物理路径
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
        /// 转换条件
        /// </summary>
        public string SQL
        {
            get
            {
                return _sql;
            }
            set
            {
                _sql = value;
            }
        }

        /// <summary>
        /// 空间过滤
        /// </summary>
        public string SpatialRegion
        {
            get
            {
                return _spatialRigon;
            }
            set
            {
                _spatialRigon = value;
            }
        }

        /// <summary>
        /// 转换目标值
        /// </summary>
        public string TransValue
        {
            get
            {
                return _transValue;
            }
            set
            {
                _transValue = value;
            }
        }

        /// <summary>
        /// 标识码
        /// </summary>
        public string BSM
        {
            get
            {
                return _BSM;
            }
            set
            {
                _BSM = value;
            }
        }

        /// <summary>
        /// 所属规划
        /// </summary>
        public Plan Plan
        {
            get
            {
                return _plan;
            }
            set
            {
                _plan = value;
            }
        }

        /// <summary>
        ///  管控区数据
        /// </summary>
        public IFeatureSet FeatureSet
        {
            get
            {
                return _featureSet;
            }
            set
            {
                _featureSet = value;
            }
        }

        string _name;//管控区名称
        string _uniqueField;//标记字段，用于区分要素的字段
        string _sql;//转换条件
        string _address;//物理路径
        string _spatialRigon;//空间过滤
        string _transValue;//转换目标值
        string _BSM;//标识码
        IFeatureSet _featureSet;//管控区数据
        Plan _plan;

        #endregion

        #region 方法

        /// <summary>
        /// 创建管控区
        /// </summary>
        public void CreateControlZone()
        {
            _featureSet = MultiPlan.CommonMethod.GetFeatureSetByPath(_plan.Address);

            if (_featureSet != null)
            {
                AddBSMColumn(_featureSet);
                IFeatureSet selectedSet = SelectTransFeatures(_featureSet, _sql, _spatialRigon);
                TransferAttributes(selectedSet);
                selectedSet.SaveAs(_address, true);
            }
        }

        private void AddBSMColumn(IFeatureSet dataSet)
        {
            DataTable dataTable = dataSet.DataTable;

            DataColumn BSMColumn = new DataColumn();
            BSMColumn.DataType = typeof(int);
            BSMColumn.ColumnName = BSM + "_" + "BSM";
            dataTable.Columns.Add(BSMColumn);

            int index = dataTable.Columns.IndexOf(BSM + "_" + "BSM");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow dataRow = dataTable.Rows[i];
                dataRow[index] = i;
            }
        }

        private IFeatureSet SelectTransFeatures(IFeatureSet dataSet, string filterExpression, string spatialRegion)
        {
            try
            {     
                IFeatureSet filterSet = null;
                List<DotSpatial.Data.IFeature> filterList = null;

                bool getSpatialRegion = false;
                if (_spatialRigon != null && spatialRegion != "")
                {
                    IDataManager dataManager = new DataManager();
                    filterSet = dataManager.OpenFile(_spatialRigon) as IFeatureSet;
                    if (filterSet.FeatureType == FeatureType.Polygon)
                    {
                        filterList = (filterSet as FeatureSet).SelectByAttribute("");
                        getSpatialRegion = true;
                    }
                }
                
                #region 空间和属性过滤

                IFeatureSet corFeatureSet = CorrectFeatures(dataSet as FeatureSet);
                List<DotSpatial.Data.IFeature> dataList = corFeatureSet.SelectByAttribute(filterExpression);
                if (getSpatialRegion)
                {
                    for (int i = 0; i < dataList.Count; i++)
                    {
                        bool isIntersection = false;
                        foreach (DotSpatial.Data.IFeature feature in filterList)
                        {
                            if (dataList[i].Geometry.Intersects(feature.Geometry))
                            {
                                isIntersection = true;
                                break;
                            }
                        }
                        if (!isIntersection)
                        {
                            dataList.RemoveAt(i);
                            i--;
                        }
                    }
                }
                
                #endregion

                IFeatureSet resultSet = new FeatureSet(dataList);
                resultSet.Projection = dataSet.Projection;
                return resultSet;
            }
            catch (Exception ex)
            {               
                MessageBox.Show("分析失败！");
                return null;
            }
        }

        private void TransferAttributes(IFeatureSet transFeatureSet)
        {
            //int num = 0;
            //int featureNum = transFeatureSet.Features.Count;
            //ProgressForm pf = new ProgressForm();
            DataTable dataTable = transFeatureSet.DataTable;
            try
            {
                #region 转换时编辑字段

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    DataColumn dataColumn = dataTable.Columns[i];
                    string columnName = dataColumn.ColumnName.ToUpper();
                    columnName = columnName.Replace('.', '_');
                    if ((string.Equals(columnName, "SHAPE_AREA")) || (string.Equals(columnName, "SHAPE_LENGTH"))
                            || (string.Equals(columnName, "SHAPE_LENG")) || (string.Equals(columnName, "SHAPE_LEN")))
                    {
                        dataColumn.ColumnName = columnName;
                    }
                }

                DataColumn transColumn = new DataColumn();
                transColumn.DataType = typeof(string);
                transColumn.ColumnName = BSM + "_" + _plan.TransField;
                dataTable.Columns.Add(transColumn);

                #endregion

                #region 转换时添加数据

                //pf.Show(this as IWin32Window);
                int index = dataTable.Columns.IndexOf(BSM + "_" + _plan.TransField);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    //pf.Update(num++, featureNum, "正在生成管控区...");
                    dataRow[index] = _transValue;
                }

                #endregion

                MessageBox.Show("创建成功！");
            }
            catch (Exception e)
            {
                MessageBox.Show("创建失败！");
            }
            finally
            {
                //pf.Close();
                //Application.DoEvents();
                //pf.Dispose();
            }
        }

        private IFeatureSet CorrectFeatures(IFeatureSet featureSet)
        {
            IFeatureSet corFeatureSet = featureSet;
            List<int> corList = new List<int>();
            for (int i = 0; i < featureSet.ShapeIndices.Count; i++)
            {
                try
                {
                    IFeature feature = featureSet.GetFeature(i);
                    IFeatureSet reFeatureSet = new FeatureSet();
                    reFeatureSet.Features.Add(feature);
                    //有待检验能否检测出其他异常
                    reFeatureSet.SelectByAttribute("");
                    corList.Add(i);
                }
                catch
                {
                    //featureSet.Features.RemoveAt(i);             
                }
            }
            corFeatureSet = corFeatureSet.CopySubset(corList);
            return corFeatureSet;
        }

        #endregion

    }


}

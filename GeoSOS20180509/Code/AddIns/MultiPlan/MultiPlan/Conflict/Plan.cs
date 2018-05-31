using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DotSpatial.Data;

namespace MultiPlan
{
    public class Plan
    {
        /// <summary>
        /// 规划名称
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
        /// 转换字段
        /// </summary>
        public string TransField
        {
            get
            {
                return _transField;
            }
            set
            {
                _transField = value;
            }
        }

        /// <summary>
        /// 转换字段的别名
        /// </summary>
        public string TransFieldAlias
        {
            get
            {
                return _transFieldAlias;
            }
            set
            {
                _transFieldAlias = value;
            }
        }

        /// <summary>
        /// 管控区集合
        /// </summary>
        public Dictionary<string, ControlZone> ControlZones
        {
            get
            {
                return _controlZones;
            }
            set
            {
                _controlZones = value;
            }
        }

        /// <summary>
        /// 规划数据
        /// </summary>
        public IFeatureSet FeatureSet
        {
            get
            {
                if (_featureSet == null && File.Exists(_address))
                {
                    ///根据_address获取规划数据
                    _featureSet = CommonMethod.GetFeatureSetByPath(_address);
                }

                return _featureSet;
            }
            set
            {
                _featureSet = value;
            }
        }

        string _name;//规划名称
        string _address;//物理路径
        string _transField;//转换字段
        string _transFieldAlias;//转换字段的别名
        Dictionary<string, ControlZone> _controlZones = new Dictionary<string, ControlZone>();//管控区集合
        IFeatureSet _featureSet;//规划数据

    }
}

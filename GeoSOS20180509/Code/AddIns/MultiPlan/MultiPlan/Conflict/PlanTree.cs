using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace MultiPlan
{
    public class PlanTree
    {
        Dictionary<string, Plan> _planDic = new Dictionary<string, Plan>();
        string xmlpath = Config.configDir + "\\Plans.xml";
        /// <summary>
        /// 读取XML
        /// </summary>
        public void Initial()
        {
            _planDic.Clear();//清空
            try
            {
                //利用xmldoc对象读取xml文件
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(xmlpath);
                //读取根节点
                XmlElement xmlele = xmldoc.DocumentElement;
                //获取根节点下的所有节点
                XmlNodeList plans = xmlele.ChildNodes;
                foreach (XmlNode planXML in plans)
                {
                    Plan plan = new Plan();
                    plan.Name = planXML.Attributes["name"].Value;
                    if (planXML.HasChildNodes)
                    {
                        XmlNodeList xmlnodes = planXML.ChildNodes;
                        XmlNode planInfoXml = xmlnodes[0];
                        plan.Address = planInfoXml.ChildNodes[0].InnerText;
                        if (planInfoXml.ChildNodes[1].InnerText != string.Empty && planInfoXml.ChildNodes[1].InnerText != null)
                        {
                            plan.TransField = planInfoXml.ChildNodes[1].InnerText;
                        }
                        else
                        {
                            plan.TransField = "GKQ";
                        }
                        if (planInfoXml.ChildNodes[2].InnerText != string.Empty && planInfoXml.ChildNodes[2].InnerText != null)
                        {
                            plan.TransFieldAlias = planInfoXml.ChildNodes[2].InnerText;
                        }
                        else
                        {
                            plan.TransFieldAlias = "管控区";
                        }

                        XmlNode controlZonesXML = xmlnodes[1];
                        if (controlZonesXML.HasChildNodes)
                        {
                            foreach (XmlNode controlZoneXML in controlZonesXML.ChildNodes)
                            {
                                ControlZone cz = new ControlZone();
                                cz.Name = controlZoneXML.Attributes["name"].Value.ToString();
                                cz.UniqueField = controlZoneXML.ChildNodes[0].InnerText;
                                cz.Address = controlZoneXML.ChildNodes[1].InnerText;
                                cz.SQL = SetSQL(controlZoneXML.ChildNodes[2]);
                                cz.SpatialRegion = controlZoneXML.ChildNodes[3].InnerText;
                                cz.TransValue = controlZoneXML.ChildNodes[4].InnerText;
                                cz.BSM = controlZoneXML.ChildNodes[5].InnerText;
                                cz.Plan = plan;
                                plan.ControlZones.Add(cz.Name, cz);
                            }
                        }
                    }
                    _planDic.Add(plan.Name, plan);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 创建树
        /// </summary>
        public void BuildTree(TreeView treeview)
        {
            Initial();
            treeview.Nodes.Clear();//清空
            foreach (string key in _planDic.Keys)
            {
                TreeNode planNode = new TreeNode();
                planNode.Text = key;

                foreach (string key2 in _planDic[key].ControlZones.Keys)
                {
                    TreeNode controlZoneNode = new TreeNode();
                    controlZoneNode.Text = key2;
                    planNode.Nodes.Add(controlZoneNode);
                }
                treeview.Nodes.Add(planNode);
            }
        }

        /// <summary>
        /// 保存树
        /// </summary>
        public void SaveTree()
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlNode xmlnode = xmldoc.CreateXmlDeclaration("1.0", "GB2312", null);
            xmldoc.AppendChild(xmlnode);

            XmlText xmltext;
            //创建<Plans>
            XmlElement xmlelem = xmldoc.CreateElement("", "Plans", "");
            xmltext = xmldoc.CreateTextNode("");
            xmlelem.AppendChild(xmltext);
            xmldoc.AppendChild(xmlelem);

            foreach (string key in _planDic.Keys)
            {
                Plan plan = _planDic[key];
                //创建<Plan>
                XmlElement planelem = xmldoc.CreateElement("", "Plan", "");
                xmltext = xmldoc.CreateTextNode("");
                planelem.AppendChild(xmltext);
                //创建<Plan>的属性
                planelem.SetAttribute("name", "", plan.Name);
                xmldoc.ChildNodes.Item(1).AppendChild(planelem);

                #region PlanInfo

                //创建<PlanInfo>
                XmlElement planinfoelem = xmldoc.CreateElement("", "PlanInfo", "");
                xmltext = xmldoc.CreateTextNode("");
                planinfoelem.AppendChild(xmltext);
                planelem.AppendChild(planinfoelem);
                //创建<PlanInfo><Address>
                XmlElement addelem = xmldoc.CreateElement("", "Address", "");
                xmltext = xmldoc.CreateTextNode(plan.Address);
                addelem.AppendChild(xmltext);
                planinfoelem.AppendChild(addelem);
                //创建<PlanInfo><TransField>
                XmlElement transelem = xmldoc.CreateElement("", "TransField", "");
                xmltext = xmldoc.CreateTextNode(plan.TransField);
                transelem.AppendChild(xmltext);
                planinfoelem.AppendChild(transelem);
                //创建<PlanInfo><TransField2>
                XmlElement trans2elem = xmldoc.CreateElement("", "TransFieldAlias", "");
                xmltext = xmldoc.CreateTextNode(plan.TransFieldAlias);
                trans2elem.AppendChild(xmltext);
                planinfoelem.AppendChild(trans2elem);

                #endregion

                #region ControlZones

                //创建<ControlZones>
                XmlElement controlZoneselem = xmldoc.CreateElement("", "ControlZones", "");
                xmltext = xmldoc.CreateTextNode("");
                controlZoneselem.AppendChild(xmltext);
                planelem.AppendChild(controlZoneselem);

                foreach (string key2 in plan.ControlZones.Keys)
                {
                    ControlZone controlZone = plan.ControlZones[key2];
                    //创建<ControlZone>
                    XmlElement controlZoneelem = xmldoc.CreateElement("", "ControlZone", "");
                    xmltext = xmldoc.CreateTextNode("");
                    controlZoneelem.AppendChild(xmltext);
                    //创建<ControlZone>的属性
                    controlZoneelem.SetAttribute("name", "", controlZone.Name);
                    controlZoneselem.AppendChild(controlZoneelem);
                    //创建<ControlZone><IsSDE>
                    XmlElement uniqueelem = xmldoc.CreateElement("", "UniqueField", "");
                    //xmltext = xmldoc.CreateTextNode(controlZone._isSDE.ToString());
                    uniqueelem.AppendChild(xmltext);
                    controlZoneelem.AppendChild(uniqueelem);
                    //创建<ControlZone><Address>
                    XmlElement addelem2 = xmldoc.CreateElement("", "Address", "");
                    xmltext = xmldoc.CreateTextNode(controlZone.Address);
                    addelem2.AppendChild(xmltext);
                    controlZoneelem.AppendChild(addelem2);
                    //创建<ControlZone><IsSDE>
                    XmlElement sqlelem = xmldoc.CreateElement("", "SQL", "");
                    xmltext = xmldoc.CreateTextNode(controlZone.SQL);
                    sqlelem.AppendChild(xmltext);
                    controlZoneelem.AppendChild(sqlelem);
                    //创建<ControlZone><SpatialRigon>
                    XmlElement spatialelem = xmldoc.CreateElement("", "SpatialRigon", "");
                    xmltext = xmldoc.CreateTextNode(controlZone.SpatialRegion);
                    spatialelem.AppendChild(xmltext);
                    controlZoneelem.AppendChild(spatialelem);
                    //创建<ControlZone><TransValue>
                    XmlElement transelem2 = xmldoc.CreateElement("", "TransValue", "");
                    xmltext = xmldoc.CreateTextNode(controlZone.TransValue);
                    transelem2.AppendChild(xmltext);
                    controlZoneelem.AppendChild(transelem2);
                    //创建<ControlZone><BSM>
                    XmlElement bsmelem = xmldoc.CreateElement("", "BSM", "");
                    xmltext = xmldoc.CreateTextNode(controlZone.BSM);
                    bsmelem.AppendChild(xmltext);
                    controlZoneelem.AppendChild(bsmelem);
                }

                #endregion

            }
            xmldoc.Save(xmlpath);
        }

        /// <summary>
        /// 根据规划名称、管控区名称获取管控区
        /// </summary>
        public ControlZone GetZone(string planName, string zoneName)
        {
            ControlZone zone = new ControlZone();

            //利用xmldoc对象读取xml文件
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(xmlpath);
            //读取根节点
            XmlElement xmlele = xmldoc.DocumentElement;
            //获取根节点下的所有节点
            XmlNodeList plans = xmlele.ChildNodes;

            foreach (XmlNode planXML in plans)
            {
                if (string.Equals(planXML.Attributes["name"].Value, planName))
                {
                    XmlNodeList xmlnodes = planXML.ChildNodes;
                    XmlNode controlZonesXML = xmlnodes[1];
                    foreach (XmlNode controlZoneXML in controlZonesXML.ChildNodes)
                    {
                        if (string.Equals(controlZoneXML.Attributes["name"].Value.ToString(), zoneName))
                        {
                            zone.Name = zoneName;
                            //zone._isSDE = Convert.ToBoolean(controlZoneXML.ChildNodes[0].InnerText);
                            zone.Address = controlZoneXML.ChildNodes[1].InnerText;
                            zone.SQL = SetSQL(controlZoneXML.ChildNodes[2]);
                            zone.SpatialRegion = controlZoneXML.ChildNodes[3].InnerText;
                            zone.TransValue = controlZoneXML.ChildNodes[4].InnerText;
                            zone.BSM = controlZoneXML.ChildNodes[5].InnerText;
                            break;
                        }
                    }
                    break;
                }
            }

            return zone;
        }

        /// <summary>
        /// 根据规划名称、管控区名称获取物理路径
        /// </summary>
        public string GetZoneAddress(string planName, string zoneName, out bool isSDE)
        {
            string address = String.Empty;
            isSDE = false;

            //利用xmldoc对象读取xml文件
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(xmlpath);
            //读取根节点
            XmlElement xmlele = xmldoc.DocumentElement;
            //获取根节点下的所有节点
            XmlNodeList plans = xmlele.ChildNodes;

            foreach (XmlNode planXML in plans)
            {
                if (string.Equals(planXML.Attributes["name"].Value, planName))
                {
                    XmlNodeList xmlnodes = planXML.ChildNodes;
                    XmlNode controlZonesXML = xmlnodes[1];
                    foreach (XmlNode controlZoneXML in controlZonesXML.ChildNodes)
                    {
                        if (string.Equals(controlZoneXML.Attributes["name"].Value.ToString(), zoneName))
                        {
                            isSDE = Convert.ToBoolean(controlZoneXML.ChildNodes[0].InnerText);
                            address = controlZoneXML.ChildNodes[1].InnerText;
                            break;
                        }
                    }
                    break;
                }
            }
            return address;
        }

        /// <summary>
        /// 保存生成管控区的路径
        /// </summary>
        public void SaveZonePath(string dir, string planName, string zoneName)
        {
            //if (dir.Contains(".gdb"))
            //{
            //    _planDic[planName].ControlZones[zoneName].address = dir + "\\" + planName + "_" + zoneName;
            //}
            //else
            //{
            //    _planDic[planName].ControlZones[zoneName].address = dir + "\\" + planName + "_" + zoneName + ".shp";
            //}
            _planDic[planName].ControlZones[zoneName].Address = dir;
            //_planDic[planName].ControlZones[zoneName]._isSDE = false;
            SaveTree();
        }

        public void SaveZonePath(string dir, string planName, string zoneName, string filename)
        {
            if (dir.Contains(".gdb"))
            {
                _planDic[planName].ControlZones[zoneName].Address = dir + "\\" + filename;
            }
            else
            {
                _planDic[planName].ControlZones[zoneName].Address = dir + "\\" + filename + ".shp";
            }
            //_planDic[planName].ControlZones[zoneName]._isSDE = false;
            SaveTree();
        }

        /// <summary>
        /// 获得所有规划名称
        /// </summary>
        public List<string> GetAllPlans()
        {
            List<string> result = new List<string>();
            foreach (string key in _planDic.Keys)
            {
                result.Add(key);
            }
            return result;
        }

        /// <summary>
        /// 根据规划获取所有管控区名称
        /// </summary>
        public List<string> GetAllZonesByPlan(string planname)
        {
            List<string> result = new List<string>();
            foreach (string key in _planDic[planname].ControlZones.Keys)
            {
                result.Add(key);
            }
            return result;
        }

        /// <summary>
        /// 获取选中树,返回节点信息
        /// </summary>
        public object GetSelectedNode(TreeNode node)
        {
            try
            {
                if (_planDic.Keys.Contains(node.Text))//规划节点
                {
                    Plan plan = _planDic[node.Text];
                    return plan;
                }
                else if (_planDic.Keys.Contains(node.Parent.Text))//管控区节点
                {
                    ControlZone cz = _planDic[node.Parent.Text].ControlZones[node.Text];
                    return cz;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获得SQL
        /// </summary>
        private void GetSQL(string sql)
        {

        }

        /// <summary>
        /// 设置SQL
        /// </summary>
        private string SetSQL(XmlNode sqlXML)
        {
            string sql = string.Empty;
            sql = sqlXML.InnerText;

            return sql;
        }

        #region 规划节点

        /// <summary>
        /// 增加规划节点，即父节点
        /// </summary>
        public void AddPlan(Plan plan)
        {
            if (_planDic.Keys.Contains(plan.Name))
            {
                _planDic.Remove(plan.Name);
            }
            _planDic.Add(plan.Name, plan);
        }

        /// <summary>
        /// 删除规划节点
        /// </summary>
        public void RemovePlan(Plan plan)
        {
            _planDic.Remove(plan.Name);
        }

        /// <summary>
        /// 修改规划节点
        /// </summary>
        public void ModifyPlan(Plan plan)
        {
            _planDic[plan.Name] = plan;
        }

        /// <summary>
        /// 修改规划节点的名称
        /// </summary>
        public void ModifyPlanName(Plan plan, string newName)
        {
            _planDic.Remove(plan.Name);
            plan.Name = newName;
            _planDic.Add(newName, plan);
        }

        #endregion

        #region 管控区节点

        /// <summary>
        /// 增加管控区节点，即子节点
        /// </summary>
        public void AddZone(ControlZone controlZone, Plan plan)
        {
            if (_planDic[plan.Name].ControlZones.Keys.Contains(controlZone.Name))
            {
                _planDic[plan.Name].ControlZones.Remove(controlZone.Name);
            }
            _planDic[plan.Name].ControlZones.Add(controlZone.Name, controlZone);
        }

        /// <summary>
        /// 删除管控区节点
        /// </summary>
        public void RemoveZone(ControlZone controlZone, Plan plan)
        {
            _planDic[plan.Name].ControlZones.Remove(controlZone.Name);
        }

        /// <summary>
        /// 修改管控区节点
        /// </summary>
        public void ModifyZone(ControlZone controlZone, Plan plan)
        {
            _planDic[plan.Name].ControlZones[controlZone.Name] = controlZone;
        }

        /// <summary>
        /// 修改管控区节点名称
        /// </summary>
        public void ModifyZoneName(ControlZone controlZone, Plan plan, string newName)
        {
            _planDic[plan.Name].ControlZones.Remove(controlZone.Name);
            controlZone.Name = newName;
            _planDic[plan.Name].ControlZones.Add(newName, controlZone);
        }

        #endregion

    }
}

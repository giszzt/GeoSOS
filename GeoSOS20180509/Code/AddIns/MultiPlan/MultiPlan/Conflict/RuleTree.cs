using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace MultiPlan
{
    public class RuleTree
    {
        public Dictionary<string, Rule> _ruleDic = new Dictionary<string, Rule>();
        string xmlpath = Config.configDir + "\\Rules.xml";
        //public string buleMapAddress = string.Empty;
        public bool isSDE = false;

        /// <summary>
        /// 读取XML
        /// </summary>
        public void Initial()
        {
            _ruleDic.Clear();//清空
            try
            {
                //利用xmldoc对象读取xml文件
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(xmlpath);
                //读取根节点
                XmlElement xmlele = xmldoc.DocumentElement;
                //buleMapAddress = xmlele.Attributes["blueMapAddress"].Value;
                isSDE = Convert.ToBoolean(xmlele.Attributes["isSDE"].Value);
                //获取根节点下的所有节点
                XmlNodeList rules = xmlele.ChildNodes;
                foreach (XmlNode ruleXML in rules)
                {
                    Rule rule = new Rule();
                    rule.Name = ruleXML.Attributes["name"].Value;
                    if (ruleXML.HasChildNodes)
                    {
                        XmlNodeList conflictNodes = ruleXML.ChildNodes;
                        foreach (XmlNode conflictXml in conflictNodes)
                        {
                            Conflict conflict = new Conflict();
                            conflict.Name = conflictXml.Attributes["name"].Value.ToString();
                            conflict.ConflictType = conflictXml.Attributes["type"].Value.ToString();
                            conflict.Address = conflictXml.Attributes["address"].Value.ToString();

                            XmlNodeList zoneNodes = conflictXml.ChildNodes;
                            conflict.ZoneA.Name = zoneNodes[0].Attributes["name"].Value.ToString();
                            conflict.ZoneB.Name = zoneNodes[1].Attributes["name"].Value.ToString();

                            rule.ConflictDic.Add(conflict.Name, conflict);
                        }
                    }
                    _ruleDic.Add(rule.Name, rule);
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
            foreach (string key in _ruleDic.Keys)
            {
                TreeNode ruleNode = new TreeNode();
                ruleNode.Text = key;

                foreach (string key2 in _ruleDic[key].ConflictDic.Keys)
                {
                    TreeNode conflictNode = new TreeNode();
                    conflictNode.Text = key2;
                    ruleNode.Nodes.Add(conflictNode);
                }
                treeview.Nodes.Add(ruleNode);
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
            //创建<Rules>
            XmlElement xmlelem = xmldoc.CreateElement("", "Rules", "");
            //xmlelem.SetAttribute("blueMapAddress", buleMapAddress);
            xmlelem.SetAttribute("isSDE", isSDE.ToString());
            xmltext = xmldoc.CreateTextNode("");
            xmlelem.AppendChild(xmltext);
            xmldoc.AppendChild(xmlelem);

            foreach (string key in _ruleDic.Keys)
            {
                Rule rule = _ruleDic[key];
                //创建<Rule>
                XmlElement ruleelem = xmldoc.CreateElement("", "Rule", "");
                xmltext = xmldoc.CreateTextNode("");
                ruleelem.AppendChild(xmltext);
                //创建<Rule>的属性
                ruleelem.SetAttribute("name", "", rule.Name);
                xmldoc.ChildNodes.Item(1).AppendChild(ruleelem);

                #region

                foreach (string key2 in rule.ConflictDic.Keys)
                {
                    Conflict conflict = rule.ConflictDic[key2];
                    //创建<Conflict>
                    XmlElement conflictelem = xmldoc.CreateElement("", "Conflict", "");
                    conflictelem.SetAttribute("name", "", conflict.Name);
                    conflictelem.SetAttribute("type", "", conflict.ConflictType);
                    conflictelem.SetAttribute("address", "", conflict.Address);
                    xmltext = xmldoc.CreateTextNode("");
                    conflictelem.AppendChild(xmltext);
                    ruleelem.AppendChild(conflictelem);
                    //管控区A
                    XmlElement elemA = xmldoc.CreateElement("", "ControlZone", "");
                    elemA.SetAttribute("name", conflict.ZoneA.Name);
                    conflictelem.AppendChild(elemA);
                    //管控区B
                    XmlElement elemB = xmldoc.CreateElement("", "ControlZone", "");
                    elemB.SetAttribute("name", conflict.ZoneB.Name);
                    conflictelem.AppendChild(elemB);
                }

                #endregion
            }
            xmldoc.Save(xmlpath);
        }

        /// <summary>
        /// 保存冲突结果路径
        /// </summary>
        public void SaveConflictPath(string dir, string ruleName, string conflictName)
        {
            //Conflict conflict = _ruleDic[ruleName].ConflictDic[conflictName];
            //if (dir.Contains(".gdb"))
            //{
            //    conflict.Address = dir;
            //}
            //else
            //{
            //    conflict.Address = dir + ".shp";
            //}
            SaveTree();
        }

        public void SaveConflictPath(string dir, string ruleName, string conflictName, string filename)
        {
            Conflict conflict = _ruleDic[ruleName].ConflictDic[conflictName];
            if (dir.Contains(".gdb"))
            {
                conflict.Address = dir + "\\" + filename;
            }
            else
            {
                conflict.Address = dir + "\\" + filename + ".shp";
            }
            SaveTree();
        }

        //public void SaveBlueAddress()
        //{
        //    //利用xmldoc对象读取xml文件
        //    XmlDocument xmldoc = new XmlDocument();
        //    xmldoc.Load(xmlpath);
        //    //读取根节点
        //    XmlElement xmlele = xmldoc.DocumentElement;
        //    xmlele.SetAttribute("blueMapAddress", buleMapAddress);
        //    xmlele.SetAttribute("isSDE", isSDE.ToString());

        //    xmldoc.Save(xmlpath);
        //}

        /// <summary>
        /// 获取所有冲突
        /// </summary>
        /// <returns></returns>
        public List<Conflict> GetConflicts()
        {
            TreeView treeview = new TreeView();
            BuildTree(treeview);

            List<Conflict> result = new List<Conflict>();
            Conflict conflict = new Conflict();
            foreach (string key in _ruleDic.Keys)
            {
                Rule rule = _ruleDic[key];
                foreach (string key2 in rule.ConflictDic.Keys)
                {
                    conflict = rule.ConflictDic[key2];
                    result.Add(conflict);
                }
            }

            //blueMapAddress = buleMapAddress;
            return result;
        }

        /// <summary>
        /// 获取选中树,返回节点信息
        /// </summary>
        public object GetSelectedNode(TreeNode node)
        {
            try
            {
                if (_ruleDic.Keys.Contains(node.Text))//规划冲突节点
                {
                    Rule rule = _ruleDic[node.Text];
                    return rule;
                }
                else if (_ruleDic.Keys.Contains(node.Parent.Text))//冲突节点
                {
                    Conflict conflict = _ruleDic[node.Parent.Text].ConflictDic[node.Text];
                    return conflict;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取冲突
        /// </summary>
        public Conflict GetConflict(string conflictName, string ruleName)
        {
            return _ruleDic[ruleName].ConflictDic[conflictName];
        }


        #region 规划冲突节点

        /// <summary>
        /// 新增规划冲突节点名称，即父节点
        /// </summary>
        public void AddRule(Rule rule)
        {
            if (_ruleDic.Keys.Contains(rule.Name))
            {
                _ruleDic.Remove(rule.Name);
            }
            _ruleDic.Add(rule.Name, rule);
        }

        /// <summary>
        /// 删除规划冲突节点名称
        /// </summary>
        public void RemoveRule(Rule rule)
        {
            _ruleDic.Remove(rule.Name);
        }

        /// <summary>
        /// 修改规划冲突节点
        /// </summary>
        public void ModifyRule(Rule rule)
        {
            _ruleDic[rule.Name] = rule;
        }

        /// <summary>
        /// 修改规划冲突节点名称
        /// </summary>
        public void ModifyRuleName(Rule rule, string newName)
        {
            _ruleDic.Remove(rule.Name);
            rule.Name = newName;
            _ruleDic.Add(newName, rule);
        }

        #endregion

        #region 冲突节点

        /// <summary>
        /// 新增冲突节点
        /// </summary>
        public void AddConflict(Conflict conflict, Rule rule)
        {
            if (_ruleDic[rule.Name].ConflictDic.Keys.Contains(conflict.Name))
            {
                _ruleDic[rule.Name].ConflictDic.Remove(conflict.Name);
            }
            _ruleDic[rule.Name].ConflictDic.Add(conflict.Name, conflict);
        }

        /// <summary>
        /// 删除冲突节点
        /// </summary>
        public void RemoveConflict(Conflict conflict, Rule rule)
        {
            _ruleDic[rule.Name].ConflictDic.Remove(conflict.Name);
        }

        /// <summary>
        /// 修改冲突节点
        /// </summary>
        public void ModifyConflict(Conflict conflict, Rule rule)
        {
            _ruleDic[rule.Name].ConflictDic[conflict.Name] = conflict;
        }

        /// <summary>
        /// 修改冲突节点名称
        /// </summary>
        public void ModifyConflictName(Conflict conflict, Rule rule, string newName)
        {
            _ruleDic[rule.Name].ConflictDic.Remove(conflict.Name);
            conflict.Name = newName;
            _ruleDic[rule.Name].ConflictDic.Add(newName, conflict);
        }

        #endregion

    }
}

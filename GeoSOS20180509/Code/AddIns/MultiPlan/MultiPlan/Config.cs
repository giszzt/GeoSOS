using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace MultiPlan
{
    public static class Config
    {
        public static string configDir = string.Empty;//配置路径
        public static string dataDir = string.Empty;//数据路径
        public static string modelDir = string.Empty;//模型路径
        public static string skinDir = string.Empty;//皮肤路径
        static string xmlpath = string.Empty;

        public static void Initial()
        {
            if (File.Exists(Application.StartupPath + "\\Config\\Config.xml"))
            {
                //工具
                configDir = Application.StartupPath + "\\Config";
                dataDir = Application.StartupPath + "\\Data";
                modelDir = Application.StartupPath + "\\Models";
                skinDir = Application.StartupPath + "\\Skins";
            }
            else
            {
                //插件
                configDir = Application.StartupPath + "\\AddIns\\MultiPlan\\Config";
                dataDir = Application.StartupPath + "\\AddIns\\MultiPlan\\Data";
                modelDir = Application.StartupPath + "\\AddIns\\MultiPlan\\Models";
                skinDir = Application.StartupPath + "\\AddIns\\MultiPlan\\Skins";
            }

            xmlpath = configDir + "\\Config.xml";
            XDocument xDoc;
            xDoc = XDocument.Load(xmlpath);
            XElement root = xDoc.Root;

            #region 缺省的路径，存放冲突检测过程及结果数据

            XElement defaultSet = root.Element("DefaultDataSet");
            XElement GKQSet = defaultSet.Element("GKQSet");
            if (GKQSet.Value == "")
            {
                GKQSet.Value = dataDir + "\\MultiPlan.gdb\\GKQ";
            }
            XElement CTTBSet = defaultSet.Element("CTTBSet");
            if (CTTBSet.Value == "")
            {
                CTTBSet.Value = dataDir + "\\MultiPlan.gdb\\CTTB";
            }
            XElement XTYZSet = defaultSet.Element("XTYZSet");
            if (XTYZSet.Value == "")
            {
                XTYZSet.Value = dataDir + "\\MultiPlan.gdb\\XTYZ";
            }
            XElement XTMXSet = defaultSet.Element("XTMXSet");
            if (XTMXSet.Value == "")
            {
                XTMXSet.Value = dataDir + "\\MultiPlan.gdb\\XTMX";
            }
            XElement LTSet = defaultSet.Element("LTSet");
            if (LTSet.Value == "")
            {
                LTSet.Value = dataDir + "\\MultiPlan.gdb\\LT";
            }

            #endregion

            xDoc.Save(xmlpath);
        }

        /// <summary>
        /// 获取默认Set的路径
        /// </summary>
        public static string GetDefaultSet(string setName)
        {
            XDocument xDoc = XDocument.Load(xmlpath);
            XElement root = xDoc.Root;
            XElement defaultSet = root.Element("DefaultDataSet");
            XElement set = defaultSet.Element(setName);
            return set.Value;
        }

        /// <summary>
        /// 获得默认SDE连接参数
        /// </summary>
        public static List<string> GetSDEConnect()
        {
            List<string> result = new List<string>();
            XDocument xDoc = XDocument.Load(xmlpath);
            XElement root = xDoc.Root;
            XElement defaultSet = root.Element("ConnectSDE");
            XElement set1 = defaultSet.Element("Server");
            result.Add(set1.Value);
            XElement set2 = defaultSet.Element("User");
            result.Add(set2.Value);
            XElement set3 = defaultSet.Element("Password");
            result.Add(set3.Value);
            return result;
        }

        /// <summary>
        /// 设置SDE连接参数
        /// </summary>
        public static void Set(string server, string user, string password)
        {
            try
            {
                XDocument xDoc = XDocument.Load(xmlpath);
                XElement root = xDoc.Root;
                XElement defaultSet = root.Element("ConnectSDE");
                XElement serverSet = defaultSet.Element("Server");
                serverSet.Value = server;
                XElement userSet = defaultSet.Element("User");
                userSet.Value = user;
                XElement passwordSet = defaultSet.Element("Password");
                passwordSet.Value = password;

                xDoc.Save(xmlpath);
                MessageBox.Show("设置成功！");
            }
            catch
            { }
        }
    }
}

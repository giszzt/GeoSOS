/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2016/5/17
 * Time: 10:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Windows.Controls.Ribbon;

using ICSharpCode.Core;
using ICSharpCode.Core.Presentation;

namespace FrameWork.Ribbon
{
    /// <summary>
    /// Description of RibbonService.
    /// </summary>
    public static class RibbonService
    {

        static object CreateRibbonItemFromDescriptor(RibbonItemDescriptor descriptor)
        {
            Codon codon = descriptor.Codon;
            object caller = descriptor.Caller;
            string type = codon.Properties.Contains("type") ? codon.Properties["type"] : "Item";

            bool createCommand = codon.Properties["loadclasslazy"] == "false";

            switch (type)
            {
                case "Tab":
                    return new RibTab(codon, caller, ConvertSubItems(descriptor.SubItems), descriptor.Conditions);
                case "Group":
                    return new RibGroup(codon, caller, ConvertSubItems(descriptor.SubItems), descriptor.Conditions);
                case "Button":
                    return new RibButton(codon, caller, createCommand, descriptor.Conditions);
                case "MenuItem":
                    return new RibMenuItem(codon, caller, createCommand, descriptor.Conditions);
                case "Menu":
                    return new RibMenu(codon, descriptor.Caller, ConvertSubItems(descriptor.SubItems), descriptor.Conditions);
                //				case "Separator":
                //					return new ConditionalSeparator(codon, caller, true, descriptor.Conditions);
                default:
                    throw new System.NotSupportedException("unsupported menu item type : " + type);
            }
        }

        internal static ArrayList ConvertSubItems(IList items)
        {
            ArrayList r = new ArrayList();
            if (items != null)
            {
                foreach (RibbonItemDescriptor descripor in items)
                {
                    r.Add(CreateRibbonItemFromDescriptor(descripor));
                }
            }
            return r;
        }

        /// <summary>
        /// 向Ribbon中添加Ribbon控件,Ribbon主控件中必须为Tab类型
        /// </summary>
        public static void AddItemsToRibbon(Microsoft.Windows.Controls.Ribbon.Ribbon ribbon, object owner, string addInTreePath)
        {
            AddItemsToRibbon(ribbon, AddInTree.BuildItems<RibbonItemDescriptor>(addInTreePath, owner, false));
        }

        /// <summary>
        /// 根据描述,向Ribbon中添加Ribbon控件
        /// </summary>
        public static void AddItemsToRibbon(Microsoft.Windows.Controls.Ribbon.Ribbon ribbon, List<RibbonItemDescriptor> descriptors)
        {
            foreach (RibbonItemDescriptor descriptor in descriptors)
            {
                object item = CreateRibbonItemFromDescriptor(descriptor);
                if (item is Microsoft.Windows.Controls.Ribbon.RibbonTab)
                {
                    ribbon.Items.Add((Microsoft.Windows.Controls.Ribbon.RibbonTab)item);
                    (item as Ribbon.RibTab).CreateGroups();
                    if (item is IStatusUpdate)
                        ((IStatusUpdate)item).UpdateStatus();
                }
                else if (item is Microsoft.Windows.Controls.Ribbon.RibbonApplicationMenuItem)
                {
                    ribbon.ApplicationMenu.Items.Add((Microsoft.Windows.Controls.Ribbon.RibbonApplicationMenuItem)item);
                    if (item is IStatusUpdate)
                        ((IStatusUpdate)item).UpdateStatus();
                }
            }
        }

        /// <summary>
        /// 更新Ribbon
        /// </summary>
        public static void UpdateStatus(Microsoft.Windows.Controls.Ribbon.Ribbon ribbon)
        {
            if (ribbon == null)
                return;
            UpdateStatus(ribbon.Items);
        }

        /// <summary>
        /// 更新Ribbon Items
        /// </summary>
        public static void UpdateStatus(IEnumerable ribbonItems)
        {
            if (ribbonItems == null)
                return;
            foreach (object o in ribbonItems)
            {
                IStatusUpdate cmi = o as IStatusUpdate;
                if (cmi != null)
                    cmi.UpdateStatus();
            }
        }


    }
}

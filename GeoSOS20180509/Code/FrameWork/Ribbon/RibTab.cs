/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2016/5/17
 * Time: 10:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Collections;
using System.Collections.Generic;
using ICSharpCode.Core;
using ICSharpCode.Core.Presentation;
using Microsoft.Windows.Controls.Ribbon;

namespace FrameWork.Ribbon
{
    /// <summary>
    /// Description of RibbonTab.
    /// </summary>
    sealed class RibTab : RibbonTab, IStatusUpdate
    {
        readonly Codon codon;
        readonly object caller;
        readonly IList subItems;
        readonly IEnumerable<ICondition> conditions;

        public RibTab(Codon codon, object caller, IList subItems, IEnumerable<ICondition> conditions)
        {
            this.codon = codon;
            this.caller = caller;
            this.conditions = conditions;
            this.subItems = subItems;
            if (codon.Properties.Contains("name"))
            {
                this.Name = codon.Properties["name"];
            }
            UpdateText();
        }

        /// <summary>
        /// Update Text
        /// </summary>
        public void UpdateText()
        {
            if (codon.Properties.Contains("label"))
            {
                this.Header = StringParser.Parse(codon.Properties["label"]);
            }
        }

        /// <summary>
        /// Update Status
        /// </summary>
        public void UpdateStatus()
        {
            if (ICSharpCode.Core.Condition.GetFailedAction(conditions, caller) == ConditionFailedAction.Exclude)
            {
                this.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.Visibility = Visibility.Visible;
                foreach (object item in subItems)
                {
                    if (item is IStatusUpdate)
                    {
                        ((IStatusUpdate)item).UpdateStatus();
                        //((IStatusUpdate)item).UpdateText();//影响运行效率，一般不会修改内容
                    }
                }
            }
        }

        /// <summary>
        /// Creat Groups
        /// </summary>
        public void CreateGroups()
        {
            this.Items.Clear();
            foreach (object item in subItems)
            {
                if (!this.Items.Contains((RibbonGroup)item))
                {
                    this.Items.Add((RibbonGroup)item);
                    (item as Ribbon.RibGroup).CreateItems();
                }
                if (item is IStatusUpdate)
                {
                    ((IStatusUpdate)item).UpdateStatus();
                    ((IStatusUpdate)item).UpdateText();
                }
            }
        }
    }
}

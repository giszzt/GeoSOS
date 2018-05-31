using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using ICSharpCode.Core;
using ICSharpCode.Core.Presentation;
using Microsoft.Windows.Controls.Ribbon;

namespace FrameWork.Ribbon
{
    public class RibMenu : RibbonApplicationMenuItem, IStatusUpdate
    {
        object caller;
        Codon codon;
        IList subItems;        
        IEnumerable<ICondition> conditions;


        public RibMenu(Codon codon, object caller, IList subItems, IEnumerable<ICondition> conditions)
		{
			if (subItems == null) subItems = new ArrayList(); // don't crash when item has no children
			this.codon    = codon;
			this.caller   = caller;
			this.subItems = subItems;
			this.conditions = conditions;
            if (codon != null)
            {
                CreateSubItems();
            }
			UpdateText();
		}

        void CreateSubItems()
        {
            this.Items.Clear();
            foreach (object item in subItems)
            {
                if (item is RibbonApplicationMenuItem)
                {
                    Items.Add((RibbonApplicationMenuItem)item);
                    if (item is IStatusUpdate)
                    {
                        ((IStatusUpdate)item).UpdateStatus();
                        ((IStatusUpdate)item).UpdateText();
                    }
                }
                else
                {
                    ISubmenuBuilder submenuBuilder = (ISubmenuBuilder)item;
                    this.Items.AddRange(submenuBuilder.BuildSubmenu(codon, caller));
                }
            }
        }

        public void UpdateStatus()
        {
        	if (ICSharpCode.Core.Condition.GetFailedAction(conditions, caller) == ConditionFailedAction.Exclude)
				this.Visibility = Visibility.Collapsed;
			else
			{
				this.Visibility = Visibility.Visible;
			}
        }

        public void UpdateText()
        {
            if (codon != null)
            {
                if (codon.Properties.Contains("tooltip"))
                {
                    ToolTip = StringParser.Parse(codon.Properties["tooltip"]);
                }
                if (codon.Properties.Contains("label"))
                {
                    this.Header = StringParser.Parse(codon.Properties["label"]);
                }
            }
        }
    }
}

// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpCode.Core.WinForms;

namespace FrameWork
{
    public class Menu : MenuItem, IStatusUpdate
    {
        Codon codon;
        object caller;
        IList subItems;
        bool isInitialized;
        IEnumerable<ICondition> conditions;

        public Menu(Codon codon, object caller, IList subItems, IEnumerable<ICondition> conditions)
        {
            if (subItems == null) subItems = new ArrayList(); // don't crash when item has no children
            this.codon = codon;
            this.caller = caller;
            this.subItems = subItems;
            //this.RightToLeft = RightToLeft.Inherit;
            this.conditions = conditions;

            UpdateText();
        }

        public Menu(string text, IEnumerable<ICondition> conditions, params ToolStripItem[] subItems)
        {
            this.Text = StringParser.Parse(text);
            this.MenuItems.AddRange(subItems);
            this.conditions = conditions;
        }

        void CreateSubItems()
        {
            MenuItems.Clear();
            foreach (object item in subItems)
            {
                if (item is MenuItem)
                {
                    MenuItems.Add((MenuItem)item);
                    if (item is IStatusUpdate)
                    {
                        ((IStatusUpdate)item).UpdateStatus();
                        ((IStatusUpdate)item).UpdateText();
                    }
                }
                else
                {
                    ISubmenuBuilder submenuBuilder = (ISubmenuBuilder)item;
                    MenuItems.AddRange(submenuBuilder.BuildSubmenu(codon, caller));
                }
            }
        }

        protected override void OnSelect(EventArgs e)
        {
            if (codon != null)
            {
                CreateSubItems();
            }
            base.OnSelect(e);
        }

        //public override bool Enabled
        //{
        //    get
        //    {
        //        if (codon == null)
        //        {
        //            return base.Enabled;
        //        }
        //        return Condition.GetFailedAction(conditions, caller) != ConditionFailedAction.Disable;
        //    }
        //}

        public virtual void UpdateStatus()
        {
            if (codon != null)
            {
                ConditionFailedAction failedAction = Condition.GetFailedAction(conditions, caller);
                this.Visible = failedAction != ConditionFailedAction.Exclude;
                if (!isInitialized && failedAction != ConditionFailedAction.Exclude)
                {
                    isInitialized = true;
                    CreateSubItems(); // must be created to support shortcuts
                    if (MenuItems.Count == 0 && subItems.Count > 0)
                    {
                        MenuItems.Add(new MenuItem());
                    }
                }
            }
        }

        public virtual void UpdateText()
        {
            if (codon != null)
            {
                Text = StringParser.Parse(codon.Properties["label"]);
            }
        }
    }
}


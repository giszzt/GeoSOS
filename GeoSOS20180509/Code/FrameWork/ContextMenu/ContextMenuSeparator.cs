// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpCode.Core.WinForms;

namespace FrameWork
{
    public class ContextMenuSeparator : MenuSeparator, IStatusUpdate
    {
        object caller;
        Codon codon;
        IEnumerable<ICondition> conditions;

        public ContextMenuSeparator()
        {
            this.RightToLeft = RightToLeft.Inherit;
            this.conditions = Enumerable.Empty<ICondition>();
        }

        public ContextMenuSeparator(Codon codon, object caller, IEnumerable<ICondition> conditions)
        {
            this.RightToLeft = RightToLeft.Inherit;
            this.caller = caller;
            this.codon = codon;
            this.conditions = conditions;
        }

        public virtual void UpdateStatus()
        {
            if (codon != null)
            {
                ConditionFailedAction failedAction = Condition.GetFailedAction(conditions, caller);
                this.Enabled = failedAction != ConditionFailedAction.Disable;
                this.Visible = failedAction != ConditionFailedAction.Exclude;
            }
        }

        public virtual void UpdateText()
        {
        }
    }
}


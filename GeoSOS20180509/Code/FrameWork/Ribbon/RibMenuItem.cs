using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using ICSharpCode.Core;
using ICSharpCode.Core.Presentation;
using Microsoft.Windows.Controls.Ribbon;

namespace FrameWork.Ribbon
{
    public class RibMenuItem :RibbonApplicationMenuItem,IStatusUpdate
    {
        object caller;
        Codon codon;
        ICommand ribbonCommand = null;
        IEnumerable<ICondition> conditions;

        public RibMenuItem(Codon codon, object caller, bool createCommand, IEnumerable<ICondition> conditions)
		{
			this.caller = caller;
            this.codon = codon;
            this.conditions = conditions;

            if (createCommand)
            {
                ribbonCommand = (ICommand)codon.AddIn.CreateObject(codon.Properties["class"]);
            }
            if (codon.Properties.Contains("label"))
            {            	
                this.Header = StringParser.Parse(codon.Properties["label"]);
            }
            if (this.Icon == null && codon.Properties.Contains("icon"))
            {                
                this.Icon = PresentationResourceService.GetBitmapSource(codon.Properties["icon"]);
            }

            UpdateStatus();
            UpdateText();
		}
		
		protected override void OnClick()
        {
            base.OnClick();
            if (ribbonCommand == null)
            {
                ribbonCommand = (ICommand)codon.AddIn.CreateObject(codon.Properties["class"]);
            }
            if (ribbonCommand != null)
            {
                ribbonCommand.Run();
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

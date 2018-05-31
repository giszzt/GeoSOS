/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2016/5/17
 * Time: 14:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

using ICSharpCode.Core;
using ICSharpCode.Core.Presentation;
using Microsoft.Windows.Controls.Ribbon;

namespace FrameWork.Ribbon
{
	/// <summary>
	/// Description of RibButton.
	/// </summary>
	public class RibButton:RibbonButton,IStatusUpdate
	{
		object caller;
        Codon codon;
        ICommand ribbonCommand = null;
        IEnumerable<ICondition> conditions;
		
		public RibButton(Codon codon, object caller, bool createCommand, IEnumerable<ICondition> conditions)
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
                this.Label = StringParser.Parse(codon.Properties["label"]);
            }
            if (this.LargeImageSource == null && codon.Properties.Contains("icon"))
            {                
                this.LargeImageSource = PresentationResourceService.GetBitmapSource(codon.Properties["icon"]);
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

        /// <summary>
        /// Update Status
        /// </summary>
        public void UpdateStatus()
        {
            ConditionFailedAction action =ICSharpCode.Core.Condition.GetFailedAction(conditions, caller);
            if (action == ConditionFailedAction.Exclude)
                this.Visibility = Visibility.Collapsed;
            else if (action == ConditionFailedAction.Disable)
            {
                this.Visibility = Visibility.Visible;
                this.IsEnabled = false;
            }
            else
            {
                this.Visibility = Visibility.Visible;
                this.IsEnabled = true;
            }
        }

        /// <summary>
        /// Update Text
        /// </summary>
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
                    this.Label = StringParser.Parse(codon.Properties["label"]);
                }
            }
        }
	}
}

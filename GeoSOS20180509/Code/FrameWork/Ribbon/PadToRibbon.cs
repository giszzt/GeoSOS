using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

using ICSharpCode.Core;
using ICSharpCode.Core.Presentation;
using Microsoft.Windows.Controls.Ribbon;

namespace FrameWork.Ribbon
{
    class PadToRibbon : RibbonApplicationMenuItem
    {
        PadDescriptor padDescriptor;

        public PadToRibbon(PadDescriptor padDescriptor)
        {
            this.padDescriptor = padDescriptor;
            this.Header = StringParser.Parse(padDescriptor.Title);

            if (padDescriptor.Icon != null)
            {
                this.Icon = PresentationResourceService.GetBitmapSource(padDescriptor.Icon);
            }
        }

        protected override void OnClick()
        {
            base.OnClick();
            padDescriptor.BringPadToFront();
        }
    }
}

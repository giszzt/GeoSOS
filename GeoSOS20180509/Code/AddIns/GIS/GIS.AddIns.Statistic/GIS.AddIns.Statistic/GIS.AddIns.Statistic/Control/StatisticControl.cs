using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpCode.Core.WinForms;
using System.Drawing;

namespace GIS.AddIns.Statistic
{
    public partial class StatisticControl:UserControl
    {
        private System.Windows.Forms.ToolStrip _toolStrip;
        public StatisticControl()
        {
            InitializeComponent();
            _toolStrip = ToolbarService.CreateToolStrip(this, "/FrameWork/PlotToolStrip");
            _toolStrip.GripStyle = ToolStripGripStyle.Hidden;
            _toolStrip.Dock = DockStyle.Right;
            Controls.Add(_toolStrip);
        }
    }
}

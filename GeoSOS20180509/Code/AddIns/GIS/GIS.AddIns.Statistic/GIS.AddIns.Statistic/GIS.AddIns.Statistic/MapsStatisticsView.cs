using System.Linq;
using System.Text;
using ICSharpCode.Core;
using FrameWork.Gui;
using System.Windows.Forms;

namespace GIS.AddIns.Statistic
{
    public class MapsStatisticsView : AbstractViewContent
    {
        MapStatistics panel;

        public MapsStatisticsView(string name)
        {
            panel = new MapStatistics();
            panel.Name = name;
            panel.Dock = DockStyle.Fill;
            this.TitleName = name;
        }

        public override object Control
        {
            get
            {
                return panel;
            }
        }
    }
}

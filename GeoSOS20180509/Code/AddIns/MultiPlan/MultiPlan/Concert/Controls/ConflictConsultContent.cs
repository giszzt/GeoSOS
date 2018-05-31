using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrameWork.Gui;

namespace MultiPlan
{
    public class ConflictConsultContent : AbstractViewContent
    {
        ConflictConsultControl control;

        public ConflictConsultContent(string name)
        {
            control = new ConflictConsultControl();
            control.Name = name;
            control.Dock = DockStyle.Fill;
            this.TitleName = name;
        }

        public override object Control
        {
            get
            {
                return control;
            }
        }
    }
}

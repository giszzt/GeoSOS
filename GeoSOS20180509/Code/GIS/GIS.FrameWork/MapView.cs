using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using FrameWork.Gui;
using System.Windows.Forms;
using DotSpatial.Controls;

namespace GIS.FrameWork
{
    public class MapView : AbstractViewContent
    {
        Map panel;

        public MapView(string name)
        {
            panel = new Map();
            panel.Name = name;
            panel.Dock = DockStyle.Fill;
            panel.ZoomOutFartherThanMaxExtent = true;
            this.TitleName = name;
            panel.Click += new EventHandler(Panel_Click);
           
        }

        /// <summary>
        /// Get Map from MapView
        /// </summary>
        public override object Control
        {
            get
            {
                return panel;
            }
        }

        /// <summary>
        /// Set MapView Map to Application.App.Map
        /// </summary>
        private void Panel_Click(object sender, EventArgs e)
        {
            if (Application.App.Map == null || (Application.App.Map as Map).Name != panel.Name)
            {
                Application.App.Map = panel;
                if (GIS.FrameWork.Application.App.Legend != null)
                {
                    GIS.FrameWork.Application.App.Legend.RootNodes.Clear();
                }
                panel.Legend = Application.App.Legend;
            }
        }
    }
}

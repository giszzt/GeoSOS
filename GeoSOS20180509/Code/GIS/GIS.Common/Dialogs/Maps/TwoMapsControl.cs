using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GIS.FrameWork;
using DotSpatial.Controls;

namespace GIS.Common.Dialogs
{
    public partial class TwoMapsControl : UserControl
    {
        public TwoMapsControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the Left Title
        /// </summary>
        public string LeftTitle
        {
            get
            {
                return this.groupBox1.Text;
            }

            set
            {
                this.groupBox1.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the Right Title
        /// </summary>
        public string RightTitle
        {
            get
            {
                return this.groupBox2.Text;
            }

            set
            {
                this.groupBox2.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the Left Map
        /// </summary>
        public Map LeftMap
        {
            get
            {
                return leftMap;
            }

            set
            {
                leftMap = value;
            }
        }

        /// <summary>
        /// Gets or sets the Right Map
        /// </summary>
        public Map RightMap
        {
            get
            {
                return rightMap;
            }

            set
            {
                rightMap = value;
            }
        }

        /// <summary>
        /// ViewExtents of Left Map Changes
        /// </summary>
        private void leftMap_ViewExtentsChanged(object sender, DotSpatial.Data.ExtentArgs e)
        {
            if (this.leftMap.ViewExtents != this.rightMap.ViewExtents)
            {                
                this.rightMap.ViewExtents.SetValues(this.leftMap.ViewExtents.MinX, this.leftMap.ViewExtents.MinY, this.leftMap.ViewExtents.MaxX, this.leftMap.ViewExtents.MaxY);
                this.rightMap.Refresh();
            }
        }

        /// <summary>
        /// ViewExtents of Right Map Changes
        /// </summary>
        private void rightMap_ViewExtentsChanged(object sender, DotSpatial.Data.ExtentArgs e)
        {
            if (this.leftMap.ViewExtents != this.rightMap.ViewExtents)
            {                
                this.leftMap.ViewExtents.SetValues(this.rightMap.ViewExtents.MinX, this.rightMap.ViewExtents.MinY, this.rightMap.ViewExtents.MaxX, this.rightMap.ViewExtents.MaxY);
                this.leftMap.Refresh();
            }
        }

        /// <summary>
        /// Left Map Click
        /// </summary>
        private void leftMap_Click(object sender, EventArgs e)
        {
            if (GIS.FrameWork.Application.App.Map == null || (GIS.FrameWork.Application.App.Map as Map).Name != leftMap.Name)
            {
                GIS.FrameWork.Application.App.Map = leftMap;              
            }
        }

        /// <summary>
        /// Right Map Click
        /// </summary>
        private void rightMap_Click(object sender, EventArgs e)
        {
            if (GIS.FrameWork.Application.App.Map == null || (GIS.FrameWork.Application.App.Map as Map).Name != rightMap.Name)
            {
                GIS.FrameWork.Application.App.Map = rightMap;
            }
        }
    }
}

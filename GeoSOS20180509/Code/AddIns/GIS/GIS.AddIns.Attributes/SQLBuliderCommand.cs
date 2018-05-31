using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using ICSharpCode.Core;
using DotSpatial.Controls;
using DotSpatial.Symbology;
using DotSpatial.Data;
using FrameWork.Gui;
using GIS.FrameWork;
using GIS.Common.Dialogs;

namespace GIS.AddIns.Attributes
{
    public class SQLBuliderCommand : AbstractCommand
    {
        //private IMap _map = GIS.FrameWork.Application.app.Map;
        GIS.Common.Dialogs.Attributes _attributes = WorkbenchSingleton.Workbench.GetPad(typeof(AttributesPad)).PadContent.Control as GIS.Common.Dialogs.Attributes;
        
        public override void Run()
        {
            DataTable thisDgv = _attributes.GetTable();
            DataGridView thisDataGridView = _attributes.GetDgv();
            FeatureLayer thisLayer = _attributes.GetLayer();
                            
            
            if (thisDgv != null && thisDataGridView != null && thisLayer != null)
            {
                SQLBulider filter = new SQLBulider(thisDgv, thisDataGridView, thisLayer);
                filter.ShowDialog();
            }
            else
            {
                MessageBox.Show("please add attribute table");
            }
            
        }
    }
}

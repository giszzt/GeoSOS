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
using GIS.Common.Dialogs;
using GIS.FrameWork;

namespace GIS.AddIns.Attributes
{
    public class FieldCaculatorCommand : AbstractCommand
    {
        private IMap _map = GIS.FrameWork.Application.App.Map;
        private GIS.Common.Dialogs.Attributes _attributes = WorkbenchSingleton.Workbench.GetPad(typeof(AttributesPad)).PadContent.Control as GIS.Common.Dialogs.Attributes;


        public override void Run()
        {
            DataTable dataTable = _attributes.GetTable();
            DataGridView thisDgv = _attributes.GetDgv();            
            if (_map.Layers.Count >= 1 && dataTable != null && thisDgv != null)
            {
                //FieldCaculator fieldCaculator = new FieldCaculator(_map, dataTable,thisDgv);
                //fieldCaculator.ShowDialog();    

                _attributes.FieldCalculationExecute();                
            }
            else
            {
                MessageBox.Show("not attribute table");
            }
        }
    }
}

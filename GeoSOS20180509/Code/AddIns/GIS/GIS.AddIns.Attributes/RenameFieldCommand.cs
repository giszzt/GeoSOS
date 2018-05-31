using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using DotSpatial.Symbology;
using System.Windows.Forms;
using System.Data;
using GIS.Common.Dialogs;
using FrameWork.Gui;
using GIS.FrameWork;

namespace GIS.AddIns.Attributes
{
    class RenameFieldCommand : AbstractCommand
    {
        private FeatureLayer _featureLayer = null;
        GIS.Common.Dialogs.Attributes _attrPad;
        public override void Run()
        {
            _attrPad = WorkbenchSingleton.Workbench.GetPad(typeof(AttributesPad)).PadContent.Control as GIS.Common.Dialogs.Attributes;
            _featureLayer = _attrPad.GetLayer();
            if (_featureLayer == null)
            {
                MessageBox.Show("please add a attributes table");
                return;
            }
            renameField();
        }

        //rename field
        private void renameField()
        {


            // 不支持大属性表
            if (_featureLayer.DataSet.AttributesPopulated != true)
            {
                MessageBox.Show(StringParser.Parse("${res:GIS.Common.Dialogs.SymbologyFormsMessageStrings.TableEditorControl_renameFieldToolStripMenuItem_Click_This_feature_is_not_yet_supported_for_datasets_with_larger_than_50_000_rows_}"));
                return;
            }
            //collect the field
            List<string> field = new List<string>();
            DataTable dt = _featureLayer.DataSet.DataTable;
            if (dt == null)
            {
                return;
            }
            foreach (DataColumn dc in dt.Columns)
            {

                field.Add(dc.ToString());
            }


            RenameFieldDialog renameFieldDialog = new RenameFieldDialog(field);
            if (renameFieldDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            int index = dt.Columns.IndexOf(renameFieldDialog.ResultCombination[0]);
            dt.Columns[index].ColumnName = renameFieldDialog.ResultCombination[1]; //rename column
            
        }
    }
}

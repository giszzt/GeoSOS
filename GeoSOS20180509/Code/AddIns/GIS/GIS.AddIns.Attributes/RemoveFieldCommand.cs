using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using DotSpatial.Symbology;
using System.Data;
using GIS.Common.Dialogs;
using System.Windows.Forms;
using FrameWork.Gui;
using GIS.FrameWork;

namespace GIS.AddIns.Attributes
{
    class RemoveFieldCommand : AbstractCommand
    {
        private FeatureLayer _featureLayer;
        private DataGridView _dataGridView;
        GIS.Common.Dialogs.Attributes _attrPad;
        public override void Run()
        {
            _attrPad = WorkbenchSingleton.Workbench.GetPad(typeof(AttributesPad)).PadContent.Control as GIS.Common.Dialogs.Attributes;
            _featureLayer = _attrPad.GetLayer();
            _dataGridView = _attrPad.GetDgv();

            if (_featureLayer == null || _dataGridView == null)
            {
                MessageBox.Show("please add a attribute table");
                return;
            }
            removeField();
        }


        //remove field
        private void removeField()
        {
            if (_featureLayer.DataSet.AttributesPopulated)
            {
                List<string> field = new List<string>();
                List<string> selectedField = new List<string>();

                //collect the field

                //foreach (DataColumn dc in _featureLayer.DataSet.GetColumns())
                foreach (DataColumn dc in _featureLayer.DataSet.DataTable.Columns)
                {
                    field.Add(dc.ToString());
                }
                DeleteFieldsDialog del = new DeleteFieldsDialog(field);
                if (del.ShowDialog() == DialogResult.OK)
                {
                    if (MessageBox.Show(
                        StringParser.Parse("${res:GIS.Common.Dialogs.SymbologyFormsMessageStrings.TableEditorControl_RemoveFields}"),
                        StringParser.Parse("${res:GIS.Common.Dialogs.SymbologyFormsMessageStrings.TableEditorControl_Confirm}"),
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes
                        )
                    {

                        selectedField = del.SelectedFieldIdList;
                    }
                }
                
                _dataGridView.SuspendLayout();
                //dataGridView1.SelectionChanged -= DataGridView1SelectionChanged;
                _attrPad.IgnoreTableSelectionChanged = true;
                foreach (string fi in selectedField)
                {
                    _featureLayer.DataSet.DataTable.Columns.Remove(fi);
                }
                //dataGridView1.SelectionChanged += DataGridView1SelectionChanged;
                _attrPad.IgnoreTableSelectionChanged = false;
                _dataGridView.ResumeLayout();
                _dataGridView.ClearSelection();
            }
            else
            {
                MessageBox.Show(StringParser.Parse("${res:GIS.Common.Dialogs.SymbologyFormsMessageStrings.TableEditorControl_removeFieldToolStripMenuItem_Click_Column_edits_are_not_yet_supported_for_datasets_with_more_than_50_000_attributes}"));
            }
        }
    }
}

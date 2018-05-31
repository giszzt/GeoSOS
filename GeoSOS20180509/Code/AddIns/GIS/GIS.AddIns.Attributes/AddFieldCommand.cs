using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using FrameWork.Gui;
using GIS.FrameWork;
using DotSpatial.Symbology;
using System.Windows.Forms;
using GIS.Common.Dialogs;

namespace GIS.AddIns.Attributes
{
    /// <summary>
    /// 添加新字段插件
    /// </summary>
    class AddFieldCommand : AbstractCommand
    {

        private FeatureLayer _featureLayer;
        private DataGridView _dataGridView;
        private GIS.Common.Dialogs.Attributes _attrPad;

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
            CreateNewColumn();
        }

        //add a new field (column)
        private void CreateNewColumn()
        {
            // 不支持打属性表添加新字段
            if (!_featureLayer.DataSet.AttributesPopulated)
            {
                MessageBox.Show(StringParser.Parse("${res:GIS.Common.Dialogs.SymbologyFormsMessageStrings.LargeEditsNotSupported}"));
                return;
            }

            // 不支持无属性表添加新字段
            if (_featureLayer.DataSet.DataTable == null)
            {
                MessageBox.Show(StringParser.Parse("${res:GIS.Common.Dialogs.SymbologyFormsMessageStrings.TableEditorControl_NewFieldFail}"));
                return;
            }

            var addCol = new AddNewColum();

            if (addCol.ShowDialog(_attrPad) != DialogResult.OK)
            {
                // 添加新字段失败
                //MessageBox.Show(StringParser.Parse("${res:GIS.Common.Dialogs.SymbologyFormsMessageStrings.TableEditorControl_NewFieldFail}"));
                return;
            }

            
            _featureLayer.DataSet.DataTable.Columns.Add(addCol.Name, addCol.Type);

            _dataGridView.ClearSelection();
        }
    }
}

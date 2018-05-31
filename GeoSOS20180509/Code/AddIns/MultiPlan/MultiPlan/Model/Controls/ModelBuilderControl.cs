using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Dataweb.NShape.WinFormsUI;
using Dataweb.NShape.Advanced;
using Dataweb.NShape.GeneralShapes;
using Dataweb.NShape;
using Dataweb.NShape.Layouters;
using GIS.Common.Dialogs;
using GIS.Common;
using DotSpatial.Data;

namespace MultiPlan
{
    public partial class ModelBuilderControl : UserControl
    {
        public ModelBuilderControl()
        {
            InitializeComponent();
        }

        #region 变量

        public string output = string.Empty;//模型最终保存路径

        //图形操作
        string libraryPath = Application.StartupPath;
        Diagram diagram;
        const int zoomStep = 5;
        const string fileFilter = "模型文件|*.nspj|All Files|*.*";       
        int state = 0;
        Dataweb.NShape.Shape inputShape;
        #region variables for show results
        Dictionary<Dataweb.NShape.Shape, int> countDic = new Dictionary<Dataweb.NShape.Shape, int>();
        DataTable statTable = new DataTable();
        IFeatureSet tempSet = new FeatureSet();
        int tempId;
        #endregion
        bool foundOutput;

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initial()
        {
            InitialDiagram(null);
        }

        #region 图方法及事件

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitialDiagram(string modelMap)
        {
            project1.AddLibraryByFilePath(Path.Combine(libraryPath, "AddIns\\MultiPlan\\Dataweb.NShape.GeneralShapes.dll"), true);
            project1.LibrarySearchPaths.Add(libraryPath);
            project1.AutoLoadLibraries = true;
            if ((modelMap != "") && (modelMap != null))
            {
                LoadDiagram(modelMap);
            }
            else
            {
                xmlStore1.DirectoryName = xmlStore1.FileExtension = string.Empty;
                project1.Name = "NewProject";
                project1.Create();
                diagram = diagramSetController1.CreateDiagram("Diagram 1");
                display1.LoadDiagram("Diagram 1");

                //设置字体
                CharacterStyle characterStyle = new CharacterStyle
                {
                    Name = "Font1",
                    Size = 12,
                    ColorStyle = project1.Design.ColorStyles["Black"],
                    FontName = "Tahoma"
                };
                project1.Design.AddStyle(characterStyle);
                project1.Repository.Insert(project1.Design, characterStyle);
            }
            SetTemplate();
        }

        /// <summary>
        /// 设置符号
        /// </summary>
        private void SetTemplate()
        {
            try
            {
                //设置基本图形
                RectangleBase ellipseShape = (RectangleBase)project1.ShapeTypes["Ellipse"].CreateInstance();
                ellipseShape.Width = 120;
                ellipseShape.Height = 72;
                ellipseShape.X = 250;
                ellipseShape.Y = 250;

                RectangleBase ellipseShape2 = (RectangleBase)project1.ShapeTypes["Ellipse"].CreateInstance();
                ellipseShape2.Width = 120;
                ellipseShape2.Height = 72;
                ellipseShape2.X = 250;
                ellipseShape2.Y = 250;

                DiamondBase diamondShape = (DiamondBase)project1.ShapeTypes["Diamond"].CreateInstance();
                diamondShape.Width = 120;
                diamondShape.Height = 72;
                diamondShape.X = 250;
                diamondShape.Y = 250;

                //设置模板
                Template ellipseTemplate = new Template("Input", ellipseShape);
                ((IPlanarShape)ellipseTemplate.Shape).FillStyle = project1.Design.FillStyles.Yellow;
                ((ICaptionedShape)ellipseTemplate.Shape).SetCaptionCharacterStyle(0, project1.Design.CharacterStyles["Font1"]);
                project1.Repository.Insert(ellipseTemplate);

                Template ellipseTemplate2 = new Template("Output", ellipseShape2);
                ((IPlanarShape)ellipseTemplate2.Shape).FillStyle = project1.Design.FillStyles.Green;
                ((ICaptionedShape)ellipseTemplate2.Shape).SetCaptionCharacterStyle(0, project1.Design.CharacterStyles["Font1"]);
                project1.Repository.Insert(ellipseTemplate2);

                Template diamondTemplate = new Template("Operate", diamondShape);
                ((IPlanarShape)diamondTemplate.Shape).FillStyle = project1.Design.FillStyles.Blue;
                ((ICaptionedShape)diamondTemplate.Shape).SetCaptionCharacterStyle(0, project1.Design.CharacterStyles["Font1"]);
                project1.Repository.Insert(diamondTemplate);

                RectangularLine line = (RectangularLine)project1.ShapeTypes["RectangularLine"].CreateInstance();
                line.EndCapStyle = project1.Design.CapStyles.ClosedArrow;
                line.LineStyle = project1.Design.LineStyles["Green"];
                project1.Repository.InsertAll(new Template("Connect", line));
            }
            catch
            { }
        }

        /// <summary>
        /// 禁止Ctrl+C和Ctrl+V
        /// </summary>
        private void display1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyData == (Keys.Control | Keys.C)) || (e.KeyData == (Keys.Control | Keys.V)))
            {
                //禁止Ctrl+C和Ctrl+V
                e.Handled = true;
            }
        }

        /// <summary>
        /// 禁止右键
        /// </summary>
        private void display1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //禁止右键
                display1.ContextMenuStrip.Dispose();
            }
        }

        /// <summary>
        /// 图中删除图形
        /// </summary>
        private void display1_ShapesRemoved(object sender, Dataweb.NShape.Controllers.DiagramPresenterShapesEventArgs e)
        {
        }

        /// <summary>
        /// 图中插入图形
        /// </summary>
        private void display1_ShapesInserted(object sender, Dataweb.NShape.Controllers.DiagramPresenterShapesEventArgs e)
        {
            if (state == 1)
            {
                //标记出流程输入点
                foreach (var item in e.Shapes)
                { 
                    if (item is Dataweb.NShape.Shape)
                    {
                        inputShape = item as Dataweb.NShape.Shape;
                        return;
                    }
                }
            }
            else if (state == 2)
            {
            }
            else if (state == 3)
            {
            }
        }

        /// <summary>
        /// 双击图中图形
        /// </summary>
        private void display1_ShapeDoubleClick(object sender, Dataweb.NShape.Controllers.DiagramPresenterShapeClickEventArgs e)
        {
            if (e.Shape.Template.Name == "Input")
            {
                showInputForm(e.Shape);
            }
            else if (e.Shape.Template.Name == "Output")
            {
                showOutputForm(e.Shape);
            }
            else if (e.Shape.Template.Name == "Operate")
            {
                showOperateForm(e.Shape);
            }
            else if (e.Shape.Template.Name == "Connect")
            {
                showConnectForm(e.Shape);
            }
        }

        #region 各种图形双击触发事件

        private void showInputForm(Dataweb.NShape.Shape inShape)
        {
            if ((inShape != null) && (inShape.Template.Name == "Input" || inShape.Template.Name == "Operate"))
            {
                ModelInPutForm mpForm = new ModelInPutForm();
                mpForm.StartPosition = FormStartPosition.CenterParent;
                mpForm.Initial(inShape.Data, true);
                mpForm.ShowDialog();
                if (mpForm.DialogResult == DialogResult.OK)
                {
                    string label = string.Empty;
                    inShape.Data = mpForm.GetInfo(ref label);     
                    if (inShape.Template.Name == "Input")
                    {
                        EllipseBase eb = (EllipseBase)inShape;
                        eb.SetCaptionText(0, label);
                    }
                    if (inShape.Template.Name == "Operate")
                    {
                        DiamondBase db = (DiamondBase)inShape;
                        db.SetCaptionText(0, label);
                    }
                    display1.Refresh();
                }
            }
        }

        private void showOutputForm(Dataweb.NShape.Shape outShape)
        {
            if ((outShape != null) && (outShape.Template.Name == "Output"))
            {
                ModelOutPutForm moform = new ModelOutPutForm();
                moform.StartPosition = FormStartPosition.CenterParent;
                moform.ShowDialog();
                if (moform.DialogResult == DialogResult.OK)
                {
                    string label = moform.result;
                    EllipseBase eb = (EllipseBase)outShape;
                    eb.SetCaptionText(0, label);
                    display1.Refresh();
                }
            }
        }

        private void showOperateForm(Dataweb.NShape.Shape operShape)
        {
            if ((operShape != null) && (operShape.Template.Name == "Operate"))
            {
                ModelOperateForm opForm = new ModelOperateForm();
                opForm.StartPosition = FormStartPosition.CenterParent;
                opForm.ShowDialog();
                if (opForm.DialogResult == DialogResult.OK)
                {
                    if (opForm.result == "SQL")
                    {
                        showSQLForm(operShape);
                    }
                    else if (opForm.result == "相交分析")
                    {
                        showInputForm(operShape);
                    }
                }
            }
        }

        private void showSQLForm(Dataweb.NShape.Shape operShape)
        {
            if (inputShape != null && inputShape.Data.Split('|').Length == 2)
            {
                string dataPath = inputShape.Data.Split('|')[1];
                IFeatureSet featureSet = MultiPlan.CommonMethod.GetFeatureSetByPath(dataPath);
                SQLBuilder2 sqlBuilder = new SQLBuilder2(featureSet);
                sqlBuilder.ShowDialog();
                if (sqlBuilder.DialogResult == DialogResult.OK)
                {
                    operShape.Data = "SQL";
                    DiamondBase db = (DiamondBase)operShape;
                    db.SetCaptionText(0, sqlBuilder.sqlExpression);
                    display1.Refresh();
                }
            }
        }

        private void showConnectForm(Dataweb.NShape.Shape connShape)
        {
            if ((connShape != null) && (connShape.Template.Name == "Connect"))
            {
                ModelConnectForm mcform = new ModelConnectForm();
                mcform.StartPosition = FormStartPosition.CenterParent;
                mcform.ShowDialog();
                if (mcform.DialogResult == DialogResult.OK)
                {
                    string label = mcform.result;
                    if (label == "Yes")
                    {
                        connShape.LineStyle = project1.Design.LineStyles["Green"];
                    }
                    else if (label == "No")
                    {
                        connShape.LineStyle = project1.Design.LineStyles["Red"];
                    }
                    display1.Refresh();
                }  
            }
        }

        # endregion

        /// <summary>
        /// 连接图形时发生事件
        /// </summary>
        private void cachedRepository1_ConnectionInserted(object sender, RepositoryShapeConnectionEventArgs e)
        {
        }

        /// <summary>
        /// 加载
        /// </summary>
        private void LoadDiagram(string path)
        {
            if (display1.Diagram != null)
            {
                display1.Diagram.Clear();
            }
            if (project1.IsOpen)
            {
                project1.Close();
            }
            int index = path.LastIndexOf('\\');
            string modelPath = path.Substring(0, index);
            string modelName = path.Substring(index + 1, path.Length - 1 - index).Split('.')[0];
            xmlStore1.DirectoryName = modelPath;
            xmlStore1.FileExtension = "nspj";
            project1.Name = modelName;
            project1.Open();
            display1.LoadDiagram("Diagram 1");
            diagram = display1.Diagram;
            //标记出流程输入点
            foreach (var item in diagram.Shapes)
            {
                if (item is Dataweb.NShape.Shape)
                {
                    Dataweb.NShape.Shape shape = item as Dataweb.NShape.Shape;
                    if (shape.Template.Name == "Input")
                    {
                        inputShape = shape;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void SaveDiagram(string path)
        {
            project1.Name = Path.GetFileNameWithoutExtension(path);
            xmlStore1.DirectoryName = Path.GetDirectoryName(path);
            xmlStore1.FileExtension = Path.GetExtension(path);

            // 保存
            if (!string.IsNullOrEmpty(project1.Name) && !string.IsNullOrEmpty(xmlStore1.DirectoryName))
                project1.Repository.SaveChanges();
        }

        /// <summary>
        /// 新建
        /// </summary>
        private void NewDiagram()
        {
            cachedRepository1.DeleteAll(diagram.Shapes);
        }

        #endregion


        #region 图操作

        /// <summary>
        /// 打开
        /// </summary>
        private void openButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = Config.modelDir;
            dlg.Filter = fileFilter;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(dlg.FileName) && File.Exists(dlg.FileName))
                {
                    LoadDiagram(dlg.FileName);
                    SetTemplate();
                }
                else MessageBox.Show(string.Format("文件 '{0}' 不存在.", dlg.FileName));
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void saveButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = "保存";
                dlg.InitialDirectory = Config.modelDir;
                dlg.CreatePrompt = false;
                dlg.Filter = fileFilter;
                if (!string.IsNullOrEmpty(xmlStore1.DirectoryName))
                    dlg.InitialDirectory = xmlStore1.DirectoryName;
                dlg.FileName = project1.Name + (string.IsNullOrEmpty(xmlStore1.FileExtension) ? ".nspj" : xmlStore1.FileExtension);
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    SaveDiagram(dlg.FileName);
                }
            }
        }

        /// <summary>
        /// 新建
        /// </summary>
        private void newButton_Click(object sender, EventArgs e)
        {
            NewDiagram();
        }

        /// <summary>
        /// 选择
        /// </summary>
        private void selectButton_Click(object sender, EventArgs e)
        {
            listView1.Items[0].Selected = true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            foreach (Dataweb.NShape.Shape shape in display1.SelectedShapes)
            {
                diagram.Shapes.Remove(shape);
            }
            display1.Refresh();
        }

        /// <summary>
        /// 清空
        /// </summary>
        private void clearButton_Click(object sender, EventArgs e)
        {
            diagram.Clear();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void modifyButton_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 放大
        /// </summary>
        private void zoomInButton_Click(object sender, EventArgs e)
        {
            if (display1.ZoomLevel < 10000)
                display1.ZoomLevel += zoomStep;
        }

        /// <summary>
        /// 缩小
        /// </summary>
        private void zoomOutButton_Click(object sender, EventArgs e)
        {
            if (display1.ZoomLevel < 10000)
                display1.ZoomLevel -= zoomStep;
        }

        /// <summary>
        /// 网格背景开关
        /// </summary>
        private void gridButton_Click(object sender, EventArgs e)
        {
            if (display1.IsGridVisible)
            {
                display1.IsGridVisible = false;
            }
            else
            {
                display1.IsGridVisible = true;
            }
        }

        /// <summary>
        /// 输入因子
        /// </summary>
        private void inputButton_Click(object sender, EventArgs e)
        {
            listView1.Items[1].Selected = true;
            state = 1;
        }

        /// <summary>
        /// 输出因子
        /// </summary>
        private void outputButton_Click(object sender, EventArgs e)
        {
            listView1.Items[2].Selected = true;
            state = 2;
        }

        /// <summary>
        /// 操作因子
        /// </summary>
        private void operateButton_Click(object sender, EventArgs e)
        {
            listView1.Items[3].Selected = true;
            state = 3;
        }

        /// <summary>
        /// 连接
        /// </summary>
        private void connectButton_Click(object sender, EventArgs e)
        {
            int c = listView1.Items.Count;
            foreach (var item in listView1.Items)
            {
                string str = item.ToString();
            }
            listView1.Items[4].Selected = true;
            state = 4;
        }

        #endregion


        #region 模型操作

        /// <summary>
        /// 执行
        /// </summary>
        private void excecuteButton_Click(object sender, EventArgs e)
        {
            //ProgressForm pf = new ProgressForm();
            //pf.StartMarquee("正在执行...");
            //pf.Show();

            //ExcecuteModel(operateDic);

            //pf.StopMarquee("执行结束！");
            //pf.Close();
            int aa = System.Environment.TickCount;
            
            countDic.Clear();
            #region initial the StatTable
            statTable.Columns.Clear();
            statTable.Columns.Add(new DataColumn("ID", System.Type.GetType("System.Int16")));
            statTable.Columns.Add(new DataColumn("Shape", inputShape.GetType()));
            statTable.Columns.Add(new DataColumn("Code", System.Type.GetType("System.String")));
            #endregion

            string dataPath = inputShape.Data.Split('|')[1];
            IFeatureSet inputSet = MultiPlan.CommonMethod.GetFeatureSetByPath(dataPath);

            #region another way to search through all features 
            //foreach (IFeature feature in inputSet.Features)
            //{
            //    foundOutput = false;
            //    IList<IFeature> featureList = new List<IFeature>();
            //    featureList.Add(feature);
            //    tempSet = new FeatureSet(featureList);
            //    findChildShape(inputShape, true);
            //}
            #endregion
            for (int i = 0; i < inputSet.Features.Count; i++)
            {
                foundOutput = false;
                tempId = i;
                IList<IFeature> featureList = new List<IFeature>();
                featureList.Add(inputSet.Features[i]);
                tempSet = new FeatureSet(featureList);
                findChildShape(inputShape, true);
            }

            MessageBox.Show((System.Environment.TickCount - aa).ToString());

            #region To show the stat results of each output shape
            foreach (Dataweb.NShape.Shape shape in countDic.Keys)
            {
                (shape as EllipseBase).SetCaptionText(0, countDic[shape].ToString());
                //MessageBox.Show("Output is found:" + ((EllipseBase)shape).GetCaptionText(0) + " and count is " + countDic[shape].ToString());
            }
            #endregion

            #region To show the StatTable
            DataGridView view = new DataGridView();
            view.DataSource = statTable;
            view.Dock = System.Windows.Forms.DockStyle.Fill;
            Form form = new Form();
            form.Controls.Add(view);
            form.Show();
            #endregion
        }

        private void findChildShape(Dataweb.NShape.Shape parentShape, bool direction)
        {
            if (parentShape != null && !foundOutput)
            {                   
                foreach (var connInfo in parentShape.GetConnectionInfos(ControlPointId.Any, null))
                {
                    //找到连接线的起点
                    if (connInfo.OtherPointId != -1)
                        continue;
                    #region direction
                    if (direction && connInfo.OtherShape.LineStyle == project1.Design.LineStyles["Green"]) { }
                    else if (!direction && connInfo.OtherShape.LineStyle == project1.Design.LineStyles["Red"]) { }
                    else { continue; }
                    #endregion
                    //找到连接线 
                    foreach (var connInfo2 in connInfo.OtherShape.GetConnectionInfos(ControlPointId.Any, null))
                    {
                        //找到连接线的终点
                        if (connInfo2.OwnPointId != -2)
                            continue;
                        //找到连接图形
                        Dataweb.NShape.Shape shape = connInfo2.OtherShape;
                        if (shape.Template.Name == "Output")
                        {
                            #region add a new row to StatTable
                            DataRow newRow = statTable.NewRow();
                            newRow[0] = tempId;
                            newRow[1] = shape;
                            newRow[2] = ((EllipseBase)shape).GetCaptionText(0);
                            statTable.Rows.Add(newRow);
                            #endregion
                            if (countDic.Keys.Contains(shape))
                            {
                                countDic[shape] = countDic[shape] + 1;
                            }
                            else
                            {
                                countDic.Add(shape, 1);
                            }
                            foundOutput = true;
                            return;
                        }
                        else if (shape.Template.Name == "Operate")
                        {
                            if (shape.Data == "SQL")
                            {
                                IFeatureSet resultSet = tempSet.CopySubset((shape as DiamondBase).GetCaptionText(0));
                                if (resultSet.Features.Count > 0)
                                {
                                    findChildShape(shape, true);
                                }
                                else
                                {
                                    findChildShape(shape, false);
                                }
                            }
                            else
                            {
                                string dataPath = shape.Data.Split('|')[1];
                                IFeatureSet filterSet = MultiPlan.CommonMethod.GetFeatureSetByPath(dataPath);
                                if (filterSet != null)
                                {
                                    IFeatureSet resultSet = GIS.Common.FeatureSetQuery.SpatialQuery(tempSet, filterSet);
                                    if (resultSet.Features.Count > 0)
                                    {
                                        findChildShape(shape, true);
                                    }
                                    else
                                    {
                                        findChildShape(shape, false);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 执行模型
        /// </summary>
        private void ExcecuteModel(Dictionary<string, string> operateDic)
        {
            int num = operateDic.Count;
            for (int i = 0; i < operateDic.Count; i++)
            {
                string key = operateDic.Keys.ToList()[i];
                if (ExcecuteOperate(key, operateDic[key], ref output))
                {
                    //如果执行成功，就删除操作
                    operateDic.Remove(key);
                }
            }

            if (operateDic.Count == num)
            {
                //没有执行任何一个操作
                MessageBox.Show("执行失败，请检查模型参数！");
                return;
            }
            if (operateDic.Count != 0)
            {
                //循环执行，直到operateDic为空
                ExcecuteModel(operateDic);
            }
            else
            {
                MessageBox.Show("执行成功！");
                return;
            }
        }

        /// <summary>
        /// 执行操作
        /// </summary>
        private bool ExcecuteOperate(string operateName, string data, ref string output)
        {
            bool result = false;
            //if (operateName == "SQL")
            //{
            //    GPSQL sql = new GPSQL();
            //    result = sql.Excecute(data, ref output);
            //}
            //else if (operateName == "缓冲区分析")
            //{
            //    GPBuffer buffer = new GPBuffer();
            //    result = buffer.Excecute(data, ref output);
            //}
            //else if (operateName == "相交运算")
            //{
            //    GPIntersect intersect = new GPIntersect();
            //    result = intersect.Excecute(data, ref output);
            //}
            //else if (operateName == "擦除运算")
            //{
            //    GPErase erase = new GPErase();
            //    result = erase.Excecute(data, ref output);
            //}
            //else if (operateName == "合并运算")
            //{
            //    GPUnion union = new GPUnion();
            //    result = union.Excecute(data, ref output);
            //}
            //else if (operateName == "相交取反运算")
            //{
            //    GPSymDiff symDiff = new GPSymDiff();
            //    result = symDiff.Excecute(data, ref output);
            //}
            //else
            //{ result = false; }

            return result;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using System.Windows.Forms;
using DotSpatial.Symbology;
using DotSpatial.Data;
using OSGeo.GDAL;
using System.Collections;
using Accord.Statistics.Filters;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using Accord.Math;
using Accord.MachineLearning.DecisionTrees.Pruning;
using Accord.MachineLearning.DecisionTrees.Rules;
using GIS.FrameWork;
using FrameWork.Gui;
using System.Threading;
using System.Drawing;
using GIS.AddIns.Ca.CommonDialog;
using GIS.AddIns.Ca.CaClass;
using GIS.AddIns.Ca.CsDialog;
using GIS.AddIns.Ca.CommonClass;

namespace GIS.AddIns.Ca

{
    class DecisionTreeCommand:AbstractCommand
    {


        #region fields
        GIS.Common.Dialogs.Console.Console consolePad;
        ImageForm imageForm;
        #endregion


        #region delegate
        delegate void AsyncUpdateConsole(string line);
        delegate void AsyncUpdateImage(double[] imgBuffer, int width, int height, LandUseClassificationInfo landUseInfo);
        #endregion

        #region event handlers
        private void UpdateConsole(string line)
        {
            if (this.consolePad.InvokeRequired)
            {
                this.consolePad.Invoke(new AsyncUpdateConsole(delegate(string line1)
                {
                    
                    this.consolePad.addLineToInfo(line1);

                }), line);

            }
            else
            {
                this.consolePad.addLineToInfo(line);
            }
            
        }
        private void UpdateImage(double[] imgBuffer, int width, int height, LandUseClassificationInfo landUseInfo)
        {
            if (this.imageForm.InvokeRequired)
            {
                this.imageForm.Invoke(new AsyncUpdateImage(delegate(double[] imgBuffer1, int width1, int height1, LandUseClassificationInfo landUseInfo1)
                    {
                        this.imageForm.UpdateImage(imgBuffer1, width1, height1, landUseInfo1);
                    }), imgBuffer, width, height, landUseInfo);
            }
            else
            {
                this.imageForm.UpdateImage(imgBuffer, width, height, landUseInfo);
            }

        }

        #endregion









        public override void Run()
        {
            
            // 读取设置参数
            var form = new DTCaSetUpForm();
            form.ShowDialog();

            if (form.DialogResult != DialogResult.OK)
            {
                
            }
            else
            {

           
                var inputColumnNames = form.driveLayerNames;
                var outputColumnName = form.beginLayerName;
                LandUseClassificationInfo landUseInfo = form.LandUse;
                if (landUseInfo == null)
                {
                    landUseInfo = new LandUseClassificationInfo();
                    landUseInfo.AllTypes = new List<StructLanduseInfo>() 
                    {
                        new StructLanduseInfo(){LandUseTypeName="城市", LandUseTypeValue = 1.0, LandUseTypeColor = Color.Black.ToArgb()},
                        new StructLanduseInfo(){LandUseTypeName="非城市", LandUseTypeValue = 0.0, LandUseTypeColor = Color.White.ToArgb()},
                        new StructLanduseInfo(){LandUseTypeName="水体", LandUseTypeValue = 2.0, LandUseTypeColor = Color.Blue.ToArgb()},
                    };
                    landUseInfo.ConvertableInfos.Add(new StructLanduseInfo() { LandUseTypeName = "非城市", LandUseTypeValue = 0.0, LandUseTypeColor = Color.White.ToArgb() });
                    landUseInfo.NullInfo = new StructLanduseInfo() { LandUseTypeName = "空值", LandUseTypeValue = -9999.0, LandUseTypeColor = Color.Transparent.ToArgb() };
                    landUseInfo.UnConvertableInfos.Add(new StructLanduseInfo() { LandUseTypeName = "水体", LandUseTypeValue = 2.0, LandUseTypeColor = Color.Blue.ToArgb() });
                    landUseInfo.UrbanInfos.Add(new StructLanduseInfo() { LandUseTypeName = "城市", LandUseTypeValue = 1.0, LandUseTypeColor = Color.Black.ToArgb() });
                }           

                // 初始化控制台
                this.consolePad = WorkbenchSingleton.Workbench.GetPad(typeof(ConsolePad)).PadContent.Control as GIS.Common.Dialogs.Console.Console;
                // 显示控制台
                WorkbenchSingleton.Workbench.GetPad(typeof(ConsolePad)).BringPadToFront();

                // 初始化树
                var tree = new CaDecisionTree(inputColumnNames, outputColumnName);
                tree.updateConsoleInfoEvent += UpdateConsole;
                tree.updateImageEvent += UpdateImage;                        
                tree.LandUseInfo = landUseInfo;
                tree.SampleRate = form.RateOfSample;
                // 显示模拟图像
                this.imageForm = new ImageForm(tree.OuputBuffer, tree.Width, tree.Height, landUseInfo);
                this.imageForm.Width = tree.Width;
                this.imageForm.Height = tree.Height;                
                imageForm.Show();
                // 开始训练模拟
                Thread threadSimulate = new Thread(new ParameterizedThreadStart(tree.Run));
                threadSimulate.IsBackground = true;
                threadSimulate.Start(form.NumOfSimulate);
            }
        }

        
    }
}

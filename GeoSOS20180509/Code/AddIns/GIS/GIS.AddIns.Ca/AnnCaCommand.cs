using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using System.Drawing;
using FrameWork.Gui;
using GIS.FrameWork;
using System.Threading;
using GIS.AddIns.Ca.CaDialog;
using GIS.AddIns.Ca.CaClass;
using GIS.AddIns.Ca.CommonClass;
using GIS.AddIns.Ca.CommonDialog;
using GIS.AddIns.Ca.CsDialog;
using System.Windows.Forms;
namespace GIS.AddIns.Ca
{
    class AnnCaCommand: AbstractCommand
    {
        #region fields 
        // 控制台 pad
        GIS.Common.Dialogs.Console.Console consolePad = null;
        // image form
        ImageForm imageForm = null;
        #endregion


        #region delegate

        private delegate void AsyncUpdateImage(double[] imgBuffer, int width, int height, LandUseClassificationInfo landUseInfo);
        private delegate void AsyncUpdateConsole(string line);

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
                    
                    this.imageForm.UpdateImage(imgBuffer1, width1, height1, landUseInfo);
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

            // 显示设置参数窗口
            var setUpForm = new AnnCaSetUpForm();
            setUpForm.ShowDialog();

            if (setUpForm.DialogResult != DialogResult.OK)
            {


            }
            else
            {

          

                // 获得参数
                string beginLayerName = setUpForm.beginLayerName;
                string endLayerName = setUpForm.endLayerName;
                List<string> driveLayerNames = setUpForm.driveLayerNames;

                int numOfSample = setUpForm.NumOfSample;
                int numOfTrain = setUpForm.NumOfTrain;
                int numOfSimulate = setUpForm.NumOfSimulate;
                double threshold = setUpForm.Threshold;
                double alpha = setUpForm.Alpha;

                LandUseClassificationInfo landInfo = setUpForm.LandUse;

                // 显示控制台
                this.consolePad = WorkbenchSingleton.Workbench.GetPad(typeof(ConsolePad)).PadContent.Control as GIS.Common.Dialogs.Console.Console;
            
                WorkbenchSingleton.Workbench.GetPad(typeof(ConsolePad)).BringPadToFront();

                //string beginLayerName = "2001";
                //string endLayerName = "2006";
                //List<string> driveLayerNames = new List<string>() { "dem", "distocity", "distohighway", "distorailway", "distoroad", "distotown", "slope" };

                if (landInfo == null)
                {
                        landInfo = new LandUseClassificationInfo();
                        landInfo.NullInfo = new StructLanduseInfo() { LandUseTypeColor = Color.White.ToArgb(), LandUseTypeName = "NULL", LandUseTypeValue = -3.40282306074e+038 };
                        landInfo.AllTypes = new List<StructLanduseInfo>()
                        {
                            new StructLanduseInfo() {LandUseTypeName = "城市", LandUseTypeValue = 1, LandUseTypeColor = Color.Black.ToArgb()},
                            new StructLanduseInfo() {LandUseTypeName = "水", LandUseTypeValue = 2, LandUseTypeColor = Color.Blue.ToArgb()},
                            new StructLanduseInfo() {LandUseTypeName = "田", LandUseTypeValue = 3, LandUseTypeColor = Color.Yellow.ToArgb()},
                            new StructLanduseInfo() {LandUseTypeName = "森林", LandUseTypeValue = 4, LandUseTypeColor = Color.Green.ToArgb()},
                            new StructLanduseInfo() {LandUseTypeName = "果园", LandUseTypeValue = 5, LandUseTypeColor = Color.LightPink.ToArgb()}
                        };
                }


            
                // 设置网络
                var network = new AnnCa(beginLayerName, endLayerName, driveLayerNames, landInfo);
                network.NumOfSamples = numOfSample;
                network.TimesOfTrain = numOfTrain;
                network.Threshold = threshold;
                network.Alpha = alpha;
            

                network.updateConsoleEvent += this.UpdateConsole;
                network.updateImageEvent += this.UpdateImage;

                // 设置图像显示
                this.imageForm = new ImageForm(network.BeginBuffer, network.Width, network.Height, landInfo);
                this.imageForm.Show();
                // 开始模拟
                Thread threadSimulate = new Thread(new ParameterizedThreadStart(network.Run));
                threadSimulate.IsBackground = true;
                threadSimulate.Start(numOfSimulate);
            }
        }
    }
}

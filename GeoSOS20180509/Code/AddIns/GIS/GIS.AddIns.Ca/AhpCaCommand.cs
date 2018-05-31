using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;

using System.Windows.Forms;
using System.Drawing;
using FrameWork.Gui;
using GIS.FrameWork;
using System.Threading;
using GIS.AddIns.Ca.CsDialog;
using GIS.AddIns.Ca.CommonClass;
using GIS.AddIns.Ca.CaClass;
using GIS.AddIns.Ca.CommonDialog;

namespace GIS.AddIns.Ca
{
    class AhpCaCommand: AbstractCommand
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
            var form = new AhpCaSetUpForm();
            form.ShowDialog();

            if (form.DialogResult != DialogResult.OK)
            {

            }
            else
            {

            

                // 初始化控制台
                this.consolePad = WorkbenchSingleton.Workbench.GetPad(typeof(ConsolePad)).PadContent.Control as GIS.Common.Dialogs.Console.Console;
                // 显示控制台
                WorkbenchSingleton.Workbench.GetPad(typeof(ConsolePad)).BringPadToFront();

                string beginLayerName = form.BeginLayerName;//"city2001_012"
                string endLayerName = form.EndLayerName;//"city2006_012";
                List<string> driveLayerNames = form.DriveLayerNames;

                //= new List<string>()
                //{
                //    "distocity",
                //    "distohighway",
                //    "distorailway",
                //    "distoroad",
                //    "distotown"  
                //};
                LandUseClassificationInfo landUseInfo = form.LandUseInfo;
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

                float[] weights = form.Weights;
                if (weights == null)
                {
                     weights = new float[]{0.35f, 0.13f, 0.13f, 0.13f, 0.25f};
                }
                int sizeOfNeighbour = form.SizeOfNeighbour;//3;
                double globalFactor = form.GlobalFactor;//0.5;
                double localFactor = form.LocalFactor;//0.5;
                double alpha = form.Alpha;//1;
                int countOfCity = form.CountOfCity;//200;

                var mce = new AhpCa(beginLayerName, endLayerName, driveLayerNames);
                mce.LandUseInfo = landUseInfo;
                mce.Weights = weights;
                mce.SizeOfNeighbour = sizeOfNeighbour;
                mce.GlobalFactor = globalFactor;
                mce.LocalFactor = localFactor;
                mce.Alpha = alpha;
                mce.CountOfCity = countOfCity;
                mce.updateConsoleEvent += UpdateConsole;
                mce.updateImageEvent += UpdateImage;

                this.imageForm = new ImageForm(mce.BeginBuffer, mce.Width, mce.Height, landUseInfo);
                this.imageForm.Show();

                Thread threadSimulate = new Thread(new ThreadStart(mce.simulate));
                threadSimulate.IsBackground = true;
                threadSimulate.Start();
            }
        }
    }
}

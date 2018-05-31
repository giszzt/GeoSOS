using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using FrameWork.Gui;
using GIS.FrameWork;

using System.Windows.Forms;
using System.Threading;
using GIS.AddIns.Ca.CommonDialog;
using GIS.AddIns.Ca.CommonClass;
using GIS.AddIns.Ca.Algorithms;

namespace GIS.AddIns.Ca
{
    /// <summary>
    /// 生成一个表示logistic概率的栅格图层
    /// </summary>
    class LogisticRegressionCommand: AbstractCommand
    {
        #region fields
        // 控制台 pad
        GIS.Common.Dialogs.Console.Console consolePad = null;
        #endregion

        #region delegate
        private delegate void AsyncUpdateConsole(string line);

       
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
        #endregion
        public override void Run()
        {

            var form = new LogisticSetUpForm();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                // 获得参数
                string beginLayerName = form.BeginLayerName;
                string endLayerName = form.EndLayerName;
                string resultLayerName = form.SavePathName;
                List<string> driveLayerNames = form.DriveLayerNames;
                int numOfSample = form.NumberOfSample;
                LandUseClassificationInfo landUse = form.LandUse;

                // 初始化对象
                var lg = new LogisticRegression(beginLayerName, endLayerName, driveLayerNames);
                lg.updateConsoleEvent += UpdateConsole;
                lg.ResultLayerName = resultLayerName;
                lg.NumberOfSample = numOfSample; // 采样的数目
                lg.landUse = landUse;

                // 显示控制台
                this.consolePad = WorkbenchSingleton.Workbench.GetPad(typeof(ConsolePad)).PadContent.Control as GIS.Common.Dialogs.Console.Console;
                WorkbenchSingleton.Workbench.GetPad(typeof(ConsolePad)).BringPadToFront();
                lg.Run();
                //Thread thread = new Thread(new ThreadStart(lg.Run));
                //thread.IsBackground = true;
                //thread.Start();                
            }
            else
            {

            }

           
        }
    }
}

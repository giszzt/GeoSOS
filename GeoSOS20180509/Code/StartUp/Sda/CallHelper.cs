﻿// ********************************************************************************************************
// Product Name: MainProgram.cs
// Description:  Main entry of the program
// Copyright (c) 
//
// Contributor(s): 
//
// |-----------------|----------|----------------------------------------------------------------------
// |      Name       |  Date    |                        Comments
// |-----------------|----------|----------------------------------------------------------------------
// | Zhang Han       |05/31/2016| Create
// |-----------------|----------|----------------------------------------------------------------------
//
// ********************************************************************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

using ICSharpCode.Core;
using ICSharpCode.Core.WinForms;
using FrameWork.Commands;
using FrameWork.Gui;
//using ICSharpCode.SharpDevelop.Project;
using ICSharpCode.SharpDevelop;


namespace FrameWork.Sda
{
	internal sealed class CallHelper : MarshalByRefObject
	{
		StartUpHost.CallbackHelper callback;
		bool useSharpDevelopErrorHandler;
		
		
		public override object InitializeLifetimeService()
		{
			return null;
		}
		
		#region Initialize Core
		public void InitSharpDevelopCore(StartUpHost.CallbackHelper callback, StartupSettings properties)
		{
			// creating the service manager initializes the logging service etc.
			ICSharpCode.Core.Services.ServiceManager.Instance = new FWServiceManager();
			
			LoggingService.Info("InitFrameWork...");
			this.callback = callback;
			CoreStartup startup = new CoreStartup(properties.ApplicationName);
			if (properties.UseSharpDevelopErrorHandler) {
				this.useSharpDevelopErrorHandler = true;
				ExceptionBox.RegisterExceptionBoxForUnhandledExceptions();
			}
			startup.ConfigDirectory = properties.ConfigDirectory;
			startup.DataDirectory = properties.DataDirectory;
			if (properties.PropertiesName != null) {
				startup.PropertiesName = properties.PropertiesName;
			}
            //AssemblyParserService.DomPersistencePath = properties.DomPersistencePath;//by hanz
			
			if (properties.ApplicationRootPath != null) {
				FileUtility.ApplicationRootPath = properties.ApplicationRootPath;
			}

			startup.StartCoreServices();
			Assembly exe = Assembly.Load(properties.ResourceAssemblyName);
            ResourceService.RegisterNeutralStrings(new ResourceManager("StartUp.Resources.StringResources", exe));
            ResourceService.RegisterNeutralImages(new ResourceManager("StartUp.Resources.BitmapResources", exe));
			
			MenuCommand.LinkCommandCreator = delegate(string link) { return new LinkCommand(link); };
			MenuCommand.KnownCommandCreator = CreateICommandForWPFCommand;
            ICSharpCode.Core.Presentation.MenuService.LinkCommandCreator = MenuCommand.LinkCommandCreator;
            //StringParser.RegisterStringTagProvider(new SharpDevelopStringTagProvider());//by hanz
			
			LoggingService.Info("Looking for AddIns...");
			foreach (string file in properties.addInFiles) {
				startup.AddAddInFile(file);
			}
			foreach (string dir in properties.addInDirectories) {
				startup.AddAddInsFromDirectory(dir);
			}
			
			if (properties.AllowAddInConfigurationAndExternalAddIns) {
				startup.ConfigureExternalAddIns(Path.Combine(PropertyService.ConfigDirectory, "AddIns.xml"));
			}
			if (properties.AllowUserAddIns) {
				startup.ConfigureUserAddIns(Path.Combine(PropertyService.ConfigDirectory, "AddInInstallTemp"),
				                            Path.Combine(PropertyService.ConfigDirectory, "AddIns"));
			}
			
			LoggingService.Info("Loading AddInTree...");
			startup.RunInitialization();

            LoggingService.Info("InitFrameWork finished");
		}
		
		static ICommand CreateICommandForWPFCommand(AddIn addIn, string commandName)
		{
			var wpfCommand = ICSharpCode.Core.Presentation.MenuService.GetRegisteredCommand(addIn, commandName);
			if (wpfCommand != null)
				return new WpfCommandWrapper(wpfCommand);
			else
				return null;
		}
		
		sealed class WpfCommandWrapper : AbstractCommand
		{
			readonly System.Windows.Input.ICommand wpfCommand;
			
			public WpfCommandWrapper(System.Windows.Input.ICommand wpfCommand)
			{
				this.wpfCommand = wpfCommand;
			}
			
			public override void Run()
			{
				var routedCommand = wpfCommand as System.Windows.Input.RoutedCommand;
				if (routedCommand != null) {
					var target = System.Windows.Input.FocusManager.GetFocusedElement(WorkbenchSingleton.MainWindow);
					routedCommand.Execute(this.Owner, target);
				} else {
					wpfCommand.Execute(this.Owner);
				}
			}
			
			public override string ToString()
			{
				return "[WpfCommandWrapper " + wpfCommand + "]";
			}
		}
		#endregion
		
		#region Initialize and run Workbench
		public void RunWorkbench(WorkbenchSettings settings)
		{
			if (settings.RunOnNewThread) {
				Thread t = new Thread(RunWorkbenchInternal);
				t.SetApartmentState(ApartmentState.STA);
				t.Name = "StartUp";
				t.Start(settings);
			} else {
				RunWorkbenchInternal(settings);
			}
		}
		
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		void RunWorkbenchInternal(object settings)
		{
			WorkbenchSettings wbSettings = (WorkbenchSettings)settings;
			
			WorkbenchStartup wbc = new WorkbenchStartup();
			LoggingService.Info("Initializing workbench...");
			wbc.InitializeWorkbench();
			
			RunWorkbenchInitializedCommands();
			
			LoggingService.Info("Starting workbench...");
			Exception exception = null;
			// finally start the workbench.
			try {
				callback.BeforeRunWorkbench();
				if (Debugger.IsAttached) {
					wbc.Run(wbSettings.InitialFileList);
				} else {
					try {
						wbc.Run(wbSettings.InitialFileList);
					} catch (Exception ex) {
						exception = ex;
					}
				}
			} finally {
				LoggingService.Info("Unloading services...");
				try {
					WorkbenchSingleton.OnWorkbenchUnloaded();
					PropertyService.Save();
				} catch (Exception ex) {
					LoggingService.Warn("Exception during unloading", ex);
					if (exception == null) {
						exception = ex;
					}
				}
			}
			LoggingService.Info("Finished running workbench.");
			callback.WorkbenchClosed();
			if (exception != null) {
				const string errorText = "Unhandled exception terminated the workbench";
				LoggingService.Fatal(exception);
				if (useSharpDevelopErrorHandler) {
					System.Windows.Forms.Application.Run(new ExceptionBox(exception, errorText, true));
				} else {
					throw new RunWorkbenchException(errorText, exception);
				}
			}
		}
		
		void RunWorkbenchInitializedCommands()
		{
			foreach (ICommand command in AddInTree.BuildItems<ICommand>("/Workspace/AutostartAfterWorkbenchInitialized", null, false)) {
				try {
					command.Run();
				} catch (Exception ex) {
					// allow startup to continue if some commands fail
					MessageService.ShowException(ex);
				}
			}
		}
		#endregion

		public bool CloseWorkbench(bool force)
		{
			if (WorkbenchSingleton.InvokeRequired) {
				return WorkbenchSingleton.SafeThreadFunction<bool, bool>(CloseWorkbenchInternal, force);
			} else {
				return CloseWorkbenchInternal(force);
			}
		}
		bool CloseWorkbenchInternal(bool force)
		{
			foreach (IWorkbenchWindow window in WorkbenchSingleton.Workbench.WorkbenchWindowCollection.ToArray()) {
				if (!window.CloseWindow(force))
					return false;
			}
			WorkbenchSingleton.MainWindow.Close();
			return true;
		}
		
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "needs to be run in correct AppDomain")]
		public void KillWorkbench()
		{
			Dispatcher.CurrentDispatcher.BeginInvokeShutdown(DispatcherPriority.Normal);
		}
		

		public bool WorkbenchVisible {
			get {
				if (WorkbenchSingleton.InvokeRequired) {
					return WorkbenchSingleton.SafeThreadFunction<bool>(GetWorkbenchVisibleInternal);
				} else {
					return GetWorkbenchVisibleInternal();
                }
			}
			set {
				if (WorkbenchSingleton.InvokeRequired) {
					WorkbenchSingleton.SafeThreadCall(SetWorkbenchVisibleInternal, value);
				} else {
					SetWorkbenchVisibleInternal(value);
				}
			}
		}
		bool GetWorkbenchVisibleInternal()
		{
			return WorkbenchSingleton.MainWindow.Visibility == Visibility.Visible;
		}
		void SetWorkbenchVisibleInternal(bool value)
		{
			WorkbenchSingleton.MainWindow.Visibility = value ? Visibility.Visible : Visibility.Hidden;
		}
	}
}

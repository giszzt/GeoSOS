// Copyright (c) HanZ (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using ICSharpCode.Core.Services;
using System;
using ICSharpCode.Core.WinForms;
using ICSharpCode.SharpDevelop;

namespace FrameWork.Sda
{
	sealed class FWServiceManager : ServiceManager
	{
		readonly ILoggingService loggingService = new log4netLoggingService();
		readonly IMessageService messageService = new SDMessageService();
		readonly ThreadSafeServiceContainer container = new ThreadSafeServiceContainer();
		
		public override ILoggingService LoggingService {
			get { return loggingService; }
		}
		
		public override IMessageService MessageService {
			get { return messageService; }
		}
		
		public override object GetService(Type serviceType)
		{
			return container.GetService(serviceType);
		}
	}
	
	sealed class SDMessageService : WinFormsMessageService
	{
		public override void ShowException(Exception ex, string message)
		{
			if (ex != null)
				ExceptionBox.ShowErrorBox(ex, message);
			else
				ShowError(message);
		}
	}
}

using Trading.ViewModels;
using log4net;
using log4net.Config;
using Prism.Events;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Threading;
using Trading.UserControls;

namespace Trading
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : PrismApplication
	{
		protected override Window CreateShell()
		{
			Container.Resolve<ILog>().Debug("Start");
			return Container.Resolve<MainWindowViewModel>().View as Window;
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterSingleton<Environment>();
			containerRegistry.RegisterDialog<PlaceOrderDialog, PlaceOrderViewModel>();
			containerRegistry.RegisterInstance(LogManager.GetLogger(Assembly.GetExecutingAssembly(), DateTime.Now.ToShortTimeString()));
		}

		protected override void Initialize()
		{
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
			XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
			base.Initialize();
		}

		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Exception ev = (Exception)e.ExceptionObject;
			string errorMsg = ev.ToString();
			//errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

			Container.Resolve<ILog>().Fatal("Unhandled Exception: " + errorMsg);

			MessageBox.Show("未处理的异常:\r\n" + errorMsg);
		}
	}
}

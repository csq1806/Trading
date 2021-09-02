using log4net;
using Newtonsoft.Json;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Trading.Models;

namespace Trading
{
	public class Environment
	{
		private ILog logger = ContainerLocator.Container.Resolve<ILog>();
		//public Task Initialize()
		//{


		//}
		public Task<List<Lang>> LoadLangResources()
		{
			logger.Debug("start loading lang file.");
			return Task.Run(async () =>
			{
				var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"lang\lang.json");
				var lang = await File.ReadAllTextAsync(path, System.Text.Encoding.UTF8);

				logger.Debug("lang file loaded.");
				return JsonConvert.DeserializeObject<List<Lang>>(lang);
			});
		}

		public Task<ObservableCollection<Stock>> LoadStocks()
		{
			logger.Debug("start loading stock file.");
			return Task.Run(async () =>
			{
				var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"stocks.json");
				var stocks = await File.ReadAllTextAsync(path, System.Text.Encoding.UTF8);

				logger.Debug("stock file loaded.");
				return JsonConvert.DeserializeObject<ObservableCollection<Stock>>(stocks);
			});
		}
	}
}

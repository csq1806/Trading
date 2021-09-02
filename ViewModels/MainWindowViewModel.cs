using Trading.Views;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;
using System.Windows.Controls;
using Trading.Models;
using System.Windows;
using RestSharp;
using Newtonsoft.Json;

namespace Trading.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		private Environment environment = null;
		private Random random = new Random();
		private ManualResetEvent mre = new ManualResetEvent(true);
		//TODO: the URL used to post trading stock to server
		private RestClient client = new RestClient("http://localhost");
		public ICommand StockCellClickCommand { get; private set; }
		public MainWindowViewModel(
			MainWindow view,
			IUnityContainer container,
			Environment environment) : base(view, container)
		{
			this.environment = environment;
			StockCellClickCommand = new DelegateCommand<string>(OnStockCellClickCommand);
			Initialize();
		}

		private void OnStockCellClickCommand(string state)
		{
			mre.Reset();
			var parameter = new DialogParameters();
			parameter.Add("st", new TradingStock
			{
				ID = SelectedStock.ID,
				Price = state switch
				{
					"buy" => SelectedStock.BuyPrice,
					"sell" => SelectedStock.SellPrice,
					_ => throw new NotImplementedException(),
				},
				Quantities = state switch
				{
					"buy" => SelectedStock.BuyQuantities,
					"sell" => SelectedStock.SellQuantities,
					_ => throw new NotImplementedException(),
				},
				Portfolio = "Default"
			});
			parameter.Add("lang", selectedLang.Code);
			Logger.Debug($"user clicks {state}, selected record:{JsonConvert.SerializeObject(SelectedStock)}");
			mre.Set();

			Container.Resolve<IDialogService>().ShowDialog("PlaceOrderDialog", parameter, r =>
			{
				if (r.Result != ButtonResult.OK) return;
				var tradingStock = r.Parameters.GetValue<TradingStock>("st");

				//post trading stock to server
				var json = JsonConvert.SerializeObject(tradingStock);
				Logger.Debug($"user {tradingStock.State}s record: {json}");
				var request = new RestRequest("trading", Method.POST);
				request.RequestFormat = RestSharp.DataFormat.Json;
				request.AddParameter("Application/Json", json, ParameterType.RequestBody);
				client.ExecuteAsync(request);
				Logger.Debug("posted record to server");
				MessageBox.Show("Posted to server, check log for detail.");
			});
		}

		async private void Initialize()
		{
			LangResources = await environment.LoadLangResources();
			SelectedLang = LangResources.First();
			Stocks = await environment.LoadStocks();
			_ = Task.Run(() => UpdateStockTicks());
		}

		private void UpdateStockTicks()
		{
			while (true)
			{
				mre.WaitOne();
				Stocks.ForEach(stock =>
				{
					stock.BuyQuantities = random.Next(1, 1000);
					stock.SellQuantities = random.Next(1, 1000);
					stock.BuyPrice = (decimal)random.NextDouble() * 10;
					stock.SellPrice = (decimal)random.NextDouble() * 10;
				});
				Thread.Sleep(random.Next(100, 2000));
			}
		}

		private Lang selectedLang;

		public Lang SelectedLang
		{
			get { return selectedLang; }
			set
			{
				if (selectedLang != value)
				{
					selectedLang = value;
					ResourceDictionary dict = new ResourceDictionary();
					dict.Source = new Uri($@"Lang\{selectedLang.Code}.xaml", UriKind.Relative);

					App.Current.Resources.MergedDictionaries.Clear();
					App.Current.Resources.MergedDictionaries.Add(dict);
					OnPropertyChanged();
				}
			}

		}



		private List<Lang> langResources;

		public List<Lang> LangResources
		{
			get { return langResources; }
			set
			{
				langResources = value;
				OnPropertyChanged();
			}
		}
		private ObservableCollection<Stock> stocks;

		public ObservableCollection<Stock> Stocks
		{
			get { return stocks; }
			set
			{
				stocks = value;
				OnPropertyChanged();
			}
		}
		public Stock SelectedStock { get; set; }
	}
}

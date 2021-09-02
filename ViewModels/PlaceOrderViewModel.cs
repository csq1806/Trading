using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Trading.Models;

namespace Trading.ViewModels
{
	public class PlaceOrderViewModel : ModelBase, IDialogAware
	{
		public PlaceOrderViewModel()
		{
			Portfolios = new List<string>
			{
				"Default","投资组合1","投资组合2","投资组合3","投资组合4"
			};
		}
		public string Title => App.Current.Resources.MergedDictionaries.Last()["PlaceOrderWindowTitle"].ToString();
		public ICommand BuyCommand => new DelegateCommand(OnBuyCommand);

		public ICommand SellCommand => new DelegateCommand(OnSellCommand);

		public event Action<IDialogResult> RequestClose;

		public bool CanCloseDialog() => true;

		public void OnDialogClosed()
		{

		}

		private void OnBuyCommand()
		{
			var parameter = new DialogParameters();
			var st = Stocks.First();
			st.State = "buy";
			parameter.Add("st", st);
			RequestClose?.Invoke(new DialogResult(ButtonResult.OK, parameter));
		}
		private void OnSellCommand()
		{
			var parameter = new DialogParameters();
			var st = Stocks.First();
			st.State = "sell";
			parameter.Add("st", st);
			RequestClose?.Invoke(new DialogResult(ButtonResult.OK, parameter));
		}

		public void OnDialogOpened(IDialogParameters parameters)
		{
			Stocks = new ObservableCollection<TradingStock>();
			Stocks.Add(parameters.GetValue<TradingStock>("st"));
		}

		private ObservableCollection<TradingStock> stocks;

		public ObservableCollection<TradingStock> Stocks
		{
			get { return stocks; }
			set { stocks = value; }
		}

		private List<string> portfolios;

		public List<string> Portfolios
		{
			get { return portfolios; }
			set
			{
				portfolios = value;
				OnPropertyChanged();
			}
		}

	}
}

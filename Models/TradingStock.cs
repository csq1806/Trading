using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Models
{
	public class TradingStock : ModelBase
	{
		public string ID { get; set; }
		private int quantities;

		public int Quantities
		{
			get { return quantities; }
			set
			{
				quantities = value;
				OnPropertyChanged();
			}
		}
		private decimal price;

		public decimal Price
		{
			get { return price; }
			set
			{
				price = value;
				OnPropertyChanged();
			}
		}
		private string portfolio;

		public string Portfolio
		{
			get { return portfolio; }
			set
			{
				portfolio = value;
				OnPropertyChanged();
			}
		}
		public string State { get; set; }
	}
}

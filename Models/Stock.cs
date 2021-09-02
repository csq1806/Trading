using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Models
{
	public class Stock : ModelBase
	{
		public string ID { get; set; }
		private int buyQuantities;

		public int BuyQuantities
		{
			get { return buyQuantities; }
			set
			{
				buyQuantities = value;
				OnPropertyChanged();
			}
		}
		private int sellQuantities;

		public int SellQuantities
		{
			get { return sellQuantities; }
			set
			{
				sellQuantities = value;
				OnPropertyChanged();
			}
		}
		private decimal buyPrice;

		public decimal BuyPrice
		{
			get { return buyPrice; }
			set
			{
				buyPrice = value;
				OnPropertyChanged();
			}
		}
		private decimal sellPrice;

		public decimal SellPrice
		{
			get { return sellPrice; }
			set
			{
				sellPrice = value;
				OnPropertyChanged();
			}
		}

	}
}

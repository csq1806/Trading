using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Models
{
	public static class IEnumerableEx
	{
		public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
		{
			foreach (var item in collection)
			{
				action(item);
			}
		}

		public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
		{
			return new ObservableCollection<T>(collection);
		}
	}
}

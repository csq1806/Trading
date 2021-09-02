using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Trading.Converters
{
	public class OKNG2ColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value?.ToString().ToLower() switch
		{
			"ok" => Brushes.Green,
			"ng" => Brushes.Red,
			_ => Brushes.Black
		};

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

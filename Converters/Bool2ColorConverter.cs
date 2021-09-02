using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Trading.Converters
{
	public class Bool2ColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return Brushes.White;
			return bool.Parse(value.ToString()) switch
			{
				true => Brushes.Green,
				false => Brushes.Red
			};
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Trading.Converters
{
	public class Text2ColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return new SolidColorBrush(Color.FromRgb(0, 102, 204));
			if (value.ToString().Contains("成功")) return Brushes.Green;
			else if (value.ToString().Contains("失败")) return Brushes.Red;
			else return new SolidColorBrush(Color.FromRgb(0, 102, 204));
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

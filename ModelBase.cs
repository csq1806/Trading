using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Trading
{
	public class ModelBase : INotifyPropertyChanged
	{
		public void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var eventHandler = this.PropertyChanged;
			if (eventHandler != null)
				eventHandler(this, new PropertyChangedEventArgs(propertyName));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}

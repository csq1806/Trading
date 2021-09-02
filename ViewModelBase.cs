using log4net;
using Prism.Events;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Unity;

namespace Trading
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		private SynchronizationContext current = SynchronizationContext.Current;
		protected IUnityContainer Container;
		protected ILog Logger => Container.Resolve<ILog>();
		protected IEventAggregator EventAggregator => Container.Resolve<IEventAggregator>();

		public ViewModelBase(
			ContentControl view,
			IUnityContainer container)
		{
			if (view != null) View = view;
			Container = container;
		}

		public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		public event PropertyChangedEventHandler PropertyChanged;

		private ContentControl view;

		public ContentControl View
		{
			get => view;
			set
			{
				if (view != value)
				{
					view = value;
					view.DataContext = this;
				}
			}
		}


		private bool isBusy;

		public bool IsBusy
		{
			get => isBusy;
			set
			{
				isBusy = value;
				OnPropertyChanged();
			}
		}

		public virtual void HideWindow()
		{
			var window = View as Window;
			if (window != null) window.Close();
		}

		public virtual void ShowWindow()
		{
			var window = View as Window;
			if (window != null) window.Show();
		}

		public virtual void ShowDialogWindow()
		{
			var window = View as Window;
			if (window != null) window.ShowDialog();
		}

		protected void InvokeOnUIThread(Action action) =>
			current.Send(new SendOrPostCallback(p => action.Invoke()), null);

		protected void InvokeOnUIThreadAsync(Action action) =>
			current.Post(new SendOrPostCallback(p => action.Invoke()), null);

		protected void InvokeOnUIThread<T>(Action<T> action, T payload) =>
			current.Send(new SendOrPostCallback(p => action.Invoke(payload)), payload);

		protected void InvokeOnUIThreadAsync<T>(Action<T> action, T payload) =>
			current.Post(new SendOrPostCallback(p => action.Invoke(payload)), payload);
	}
}

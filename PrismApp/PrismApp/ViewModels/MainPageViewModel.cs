using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace PrismApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
		public MainPageViewModel(INavigationService navigationService) 
            : base (navigationService)
        {
            Title = "Main Page";
	
			Items.Add(new SomeItem() { Title = "One", Command = new DelegateCommand(NavigateToSpeakPage) });
			Items.Add(new SomeItem() { Title = "Two", Command = new DelegateCommand(NavigateToSpeakPage) });
			Items.Add(new SomeItem() { Title = "Three", Command = new DelegateCommand(NavigateToSpeakPage) });
		}

		private ObservableCollection<SomeItem> _items = new ObservableCollection<SomeItem>();

		public ObservableCollection<SomeItem> Items
		{
			get { return _items; }
		}


		private async void NavigateToSpeakPage()
		{
			var args = new NavigationParameters();
			args.Add("date", DateTime.UtcNow.Date);
			args.Add("time", DateTime.UtcNow.TimeOfDay);

			await NavigationService.NavigateAsync("SpeakPage", args);
		}
	}
}

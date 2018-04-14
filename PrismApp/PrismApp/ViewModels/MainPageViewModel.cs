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
		private readonly ObservableCollection<SomeItem> _items = new ObservableCollection<SomeItem>();


		public MainPageViewModel(INavigationService navigationService) 
            : base (navigationService)
        {
            Title = "Main Menu";
	
			Items.Add(new SomeItem() { Title = "API", Command = new DelegateCommand(NavigateToApiPage) });
			Items.Add(new SomeItem() { Title = "Print Label", Command = new DelegateCommand(NavigateToPrintLabelPage) });
			Items.Add(new SomeItem() { Title = "Log History", Command = new DelegateCommand(NavigateToLogHistoryPage) });
			Items.Add(new SomeItem() { Title = "Modal", Command = new DelegateCommand(NavigateToModalPage) });

			for (int i = 0; i < 5; i++)
			{
				Items.Add(new SomeItem() { Title = $"pj  - {i:000}", Command = new DelegateCommand(NavigateToSpeakPage) });
			}
		}


		public ObservableCollection<SomeItem> Items => _items;


		private async void NavigateToApiPage()
		{
			await NavigationService.NavigateAsync("PostApiPage");
		}

		private async void NavigateToPrintLabelPage()
		{
			await NavigationService.NavigateAsync("PrintLabelPage");
		}

		private async void NavigateToLogHistoryPage()
		{
			await NavigationService.NavigateAsync("LogHistoryPage");
		}

		private async void NavigateToModalPage()
		{
			await NavigationService.NavigateAsync("ModalPage", useModalNavigation: true);
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

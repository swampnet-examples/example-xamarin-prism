using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Prism.Plugin.Popups;
using Prism.Services;
using System.Threading.Tasks;
using Serilog;

namespace PrismApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
		private readonly ObservableCollection<SomeItem> _items = new ObservableCollection<SomeItem>();
		private readonly IPageDialogService _pageDialog;

		public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialog) 
            : base (navigationService)
        {
			_pageDialog = pageDialog;

			Title = "Main Menu";
	
			Items.Add(new SomeItem() { Title = "API", Command = new DelegateCommand(NavigateToApiPage) });
			Items.Add(new SomeItem() { Title = "Print Label", Command = new DelegateCommand(NavigateToPrintLabelPage) });
			Items.Add(new SomeItem() { Title = "Log History", Command = new DelegateCommand(NavigateToLogHistoryPage) });
			Items.Add(new SomeItem() { Title = "Modal", Command = new DelegateCommand(NavigateToModalPage) });
            Items.Add(new SomeItem() { Title = "Sqlite Test", Command = new DelegateCommand(async () => await NavigationService.NavigateAsync("SqliteTestPage"))});
            Items.Add(new SomeItem() { Title = "Nav R&D", Command = new DelegateCommand(NavigateToNavRandDPage) });

            for (int i = 0; i < 5; i++)
			{
				Items.Add(new SomeItem() { Title = $"pj  - {i:000}", Command = new DelegateCommand(NavigateToSpeakPage) });
			}

		}


		public ObservableCollection<SomeItem> Items => _items;


		public override void OnNavigatedFrom(INavigationParameters parameters)
		{
			base.OnNavigatedFrom(parameters);
		}

        private async void NavigateToNavRandDPage()
        {
            await NavigationService.NavigateAsync("BrowsePage/BrowseContentPage", null, true);
        }


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
			await AuthenticateAsync(
				async () => await NavigationService.NavigateAsync("PostApiPage"),
				async () => await NavigationService.NavigateAsync("LogHistoryPage")
				);
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

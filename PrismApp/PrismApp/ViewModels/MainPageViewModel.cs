using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrismApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
		public DelegateCommand NavigateToSpeakPageCommand { get; private set; }

		public MainPageViewModel(INavigationService navigationService) 
            : base (navigationService)
        {
            Title = "Main Page";
			NavigateToSpeakPageCommand = new DelegateCommand(NavigateToSpeakPage);
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

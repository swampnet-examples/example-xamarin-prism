using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismApp.ViewModels
{
	public class ModalPageViewModel : ViewModelBase
	{
		private Action _onAuth;
		private Action _onNoAuth;

		public ModalPageViewModel(INavigationService navigationService)
			: base(navigationService)
        {
			//Pin = "0123";
		}


		public override void OnNavigatedTo(INavigationParameters parameters)
		{
			_onAuth = parameters.GetValue<Action>("authenticated");
			_onNoAuth = parameters.GetValue<Action>("failed-authentication");
		}

		private string _pin;

		public string Pin
		{
			get { return _pin; }
			set { SetProperty(ref _pin, value); }
		}


		public DelegateCommand CloseCommand => new DelegateCommand(Authenticated);


		public DelegateCommand CancelCommand => new DelegateCommand(UnAuthenticated);


		private async void Authenticated()
		{
			await NavigationService.GoBackAsync(
				new NavigationParameters()
				{
					{ "authenticated", _onAuth }
				});
		}

		private async void UnAuthenticated()
		{
			await NavigationService.GoBackAsync(
				new NavigationParameters()
				{
					{ "failed-authentication", _onNoAuth }
				});
		}
	}
}

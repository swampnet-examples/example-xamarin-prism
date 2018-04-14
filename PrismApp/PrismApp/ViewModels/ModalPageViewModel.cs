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
		private string _destination;

		public ModalPageViewModel(INavigationService navigationService)
			: base(navigationService)
        {
		}


		public override void OnNavigatedTo(INavigationParameters parameters)
		{
			Destination = parameters.GetValue<string>("destination");
		}


		public string Destination
		{
			set { SetProperty(ref _destination, value); }
			get { return _destination; }
		}


		public DelegateCommand CloseCommand => new DelegateCommand(Close);


		public DelegateCommand CancelCommand => new DelegateCommand(async () => await NavigationService.GoBackAsync());


		private async void Close()
		{
			if (!string.IsNullOrEmpty(_destination))
			{
				await NavigationService.GoBackAsync(
					new NavigationParameters()
					{
						{ "destination", _destination }
					});
			}
			else
			{
				await NavigationService.GoBackAsync();
			}
		}
	}
}

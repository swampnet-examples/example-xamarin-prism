using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismApp.ViewModels
{
	public class ModalPageViewModel : BindableBase
	{
		private DelegateCommand _closeCommand;
		private readonly INavigationService _navigationService;

		public ModalPageViewModel(INavigationService navigationService)
        {
			_navigationService = navigationService;
			_closeCommand = new DelegateCommand(Close);
		}

		public DelegateCommand CloseCommand => _closeCommand;

		private async void Close()
		{
			await _navigationService.GoBackAsync(new NavigationParameters{
				{ "message", "Hello from the Popup View" }
			});
		}
	}
}

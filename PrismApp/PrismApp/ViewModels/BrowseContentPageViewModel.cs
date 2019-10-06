using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismApp.ViewModels
{
    public class BrowseContentPageViewModel : BindableBase, IInitialize
    {
        private readonly INavigationService _navigationService;

        public BrowseContentPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void Initialize(INavigationParameters parameters)
        {
            Id = parameters["id"] as string;
        }

        private string _id;
        public string Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private DelegateCommand _cancelCommand;

        public DelegateCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new DelegateCommand(Cancel));

        private async void Cancel()
        {
            await _navigationService.GoBackToRootAsync();
            await _navigationService.GoBackAsync();
        }
    }
}

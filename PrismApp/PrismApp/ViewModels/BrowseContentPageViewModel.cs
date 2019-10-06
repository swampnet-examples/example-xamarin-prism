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
        public BrowseContentPageViewModel()
        {

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
    }
}

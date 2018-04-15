using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrismApp.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
		{
			HandleAuthenticationResult(parameters);
		}


		public virtual void OnNavigatingTo(INavigationParameters parameters)
        {
            
        }

        public virtual void Destroy()
        {
            
        }


		internal virtual async Task AuthenticateAsync(Action authenticated, Action failed)
		{
			await NavigationService.NavigateAsync(
				"ModalPage",
				new NavigationParameters
				{
					{ "authenticated", authenticated },
					{ "failed-authentication", failed }
				});
		}


		private void HandleAuthenticationResult(INavigationParameters parameters)
		{
			// Handle authentication stuff
			var auth = parameters.GetValue<Action>("authenticated");
			if (auth != null)
			{
				Log.Information("Authenticated");
				auth.Invoke();
			}
			else
			{
				var noauth = parameters.GetValue<Action>("failed-authentication");
				if (noauth != null)
				{
					Log.Information("Failed authentication");
					noauth.Invoke();
				}
			}
		}
	}
}

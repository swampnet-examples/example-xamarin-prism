using Prism.Commands;
using Prism.Mvvm;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismApp.ViewModels
{
	public class PostApiPageViewModel : BindableBase
	{
        public PostApiPageViewModel()
        {
			BooshCommand = new DelegateCommand(Boosh);
        }

		public DelegateCommand BooshCommand { get; private set; }

		private string _information = "Hello Prism";

		public string Information
		{
			get { return _information; }
			set { SetProperty(ref _information, value); }
		}


		private void Boosh()
		{
			Log.Information(Information);
		}
	}
}

using Plugin.Media;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace PrismApp.ViewModels
{
	public class SpeakPageViewModel : ViewModelBase
	{
		private string _textToSay = "Hello Prism";
		public string TextToSay
		{
			get { return _textToSay; }
			set { SetProperty(ref _textToSay, value); }
		}

		public DelegateCommand SpeakCommand { get; set; }
		private readonly IPageDialogService _pageDialog;

		public SpeakPageViewModel(INavigationService navigationService, IPageDialogService pageDialog, IBatteryService batteryService)
			: base(navigationService)
		{
			SpeakCommand = new DelegateCommand(Speak);
			_pageDialog = pageDialog;
			_batteryService = batteryService;
		}

		public override void OnNavigatedTo(NavigationParameters parameters)
		{
			base.OnNavigatedTo(parameters);

			Date = (DateTime)parameters["date"];
			Time = (TimeSpan)parameters["time"];

			Log.Information("OnNavigatedTo {page} - {Date} / {Time}",
				GetType().Name,
				Date,
				Time);
		}

		private DateTime _date;

		public DateTime Date
		{
			get { return _date; }
			set { SetProperty(ref _date, value); }
		}

		private TimeSpan _time;
		private readonly IBatteryService _batteryService;

		public TimeSpan Time
		{
			get { return _time; }
			set { SetProperty(ref _time, value); }
		}



		private async void Speak()
		{
			//await _pageDialog.DisplayAlertAsync("My Alert", "The message " + _batteryService.GetBatteryStatus(), "ok");
			//await _pageDialog.DisplayActionSheetAsync("My Action Sheet", "cancel", "destroy");

			try
			{
				await CrossMedia.Current.Initialize();

				if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
				{
					await _pageDialog.DisplayAlertAsync("No camera", ":( No camera available.", "ok");
					return;
				}

				var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
				{
					//Directory = "Sample",
					//Name = "test.jpg",
					SaveMetaData = true,
					SaveToAlbum = true
				});

				if (file == null)
					return;

				Log.Information("File location {path}", file.Path);
				await _pageDialog.DisplayAlertAsync("File Location", file.Path, "ok");

								
				var imageSource = ImageSource.FromStream(() =>
				{
					var stream = file.GetStream();
					return stream;
				});

			}
			catch (Exception ex)
			{
				await _pageDialog.DisplayAlertAsync("Error", ex.Message, "oops");
				Log.Error(ex, ex.Message);
			}
		}
	}
}

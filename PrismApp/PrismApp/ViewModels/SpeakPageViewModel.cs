using Plugin.Connectivity;
using Plugin.Media;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

		public SpeakPageViewModel(
			INavigationService navigationService,
			IPageDialogService pageDialog,
			IBatteryService batteryService,
			IScanBarcode scanBarcode)
			: base(navigationService)
		{
			SpeakCommand = new DelegateCommand(Speak);
			_pageDialog = pageDialog;
			_batteryService = batteryService;
			_scanBarcode = scanBarcode;
		}

		public override void OnNavigatedTo(NavigationParameters parameters)
		{
			base.OnNavigatedTo(parameters);

			Date = (DateTime)parameters["date"];
			Time = (TimeSpan)parameters["time"];
			
			Log.Information("OnNavigatedTo {page} - {Date} / {Time} (IsConnected: {isConnected})",
				GetType().Name,
				Date,
				Time,
				CrossConnectivity.Current.IsConnected);
		}

		private DateTime _date;

		public DateTime Date
		{
			get { return _date; }
			set { SetProperty(ref _date, value); }
		}

		private TimeSpan _time;
		private readonly IBatteryService _batteryService;
		private readonly IScanBarcode _scanBarcode;

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
				await Scan();
				//await Photo();
			}
			catch (Exception ex)
			{
				await _pageDialog.DisplayAlertAsync("Error", ex.Message, "oops");
				Log.Error(ex, ex.Message);
			}
		}


		private async Task Scan()
		{
			var code = await _scanBarcode.Scan();
			await _pageDialog.DisplayAlertAsync("Result", code, "ok");
		}

		private async Task Photo()
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
	}
}

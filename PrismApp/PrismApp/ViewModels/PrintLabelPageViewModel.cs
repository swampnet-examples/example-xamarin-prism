using Prism.Commands;
using Prism.Mvvm;
using Prism.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismApp.ViewModels
{
	public class PrintLabelPageViewModel : BindableBase
	{
		public PrintLabelPageViewModel(ILabelPrintService printService, IPageDialogService pageDialog)
		{
			BooshCommand = new DelegateCommand(Boosh);

			_printService = printService;
			_pageDialog = pageDialog;
		}

		public DelegateCommand BooshCommand { get; private set; }

		private string _information = "Test-1234";
		private readonly ILabelPrintService _printService;
		private readonly IPageDialogService _pageDialog;

		public string Information
		{
			get { return _information; }
			set { SetProperty(ref _information, value); }
		}


		private async void Boosh()
		{
			Log.Information(Information);

			try
			{
				string zpl = @"
					^XA

					^FX test lines, various point sizes
					^CFA,15
					^FO50,10^FD" + Information + @"^FS
					^CFA,30
					^FO50,30^FD" + Information + @"^FS
					^CFA,60
					^FO50,60^FD" + Information + @"^FS

					^FX line
					^FO50,130^GB1000,1,3^FS

					^FX barcode
					^BY5,2,300
					^FO100,150^BC^FD" + Information + @"^FS

					^XZ
				";

				await _printService.Print(zpl, "192.168.0.29:9100");
			}
			catch (Exception ex)
			{
				Log.Error(ex, ex.Message);

				await _pageDialog.DisplayAlertAsync("Error", ex.Message, ":o(");
			}
		}
	}
}

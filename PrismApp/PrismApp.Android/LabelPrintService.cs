using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LinkOS.Plugin;
using LinkOS.Plugin.Abstractions;
using PrismApp;
using Serilog;

namespace PrismApp.Droid
{
	class LabelPrintService : ILabelPrintService
	{
		public Task Print(string zpl, string address)
		{
			return Task.Run(() => {
				Log.Information("Print zpl: {zpl}", zpl);

				var connection = ConnectionBuilder.Current.Build("TCP:" + address);
				connection.Open();

				connection.Write(Encoding.ASCII.GetBytes(zpl));
				//if ((SetPrintLanguage(connection)) && (CheckPrinterStatus(connection)))
				//{
				//	connection.Write(Encoding.ASCII.GetBytes(zpl));
				//}

				connection.Close();
			});
		}

		private bool SetPrintLanguage(IConnection connection)
		{
			string setLanguage = "! U1 setvar \"device.languages\" \"zpl\"\r\n\r\n! U1 getvar \"device.languages\"\r\n\r\n";
			byte[] response = connection.SendAndWaitForResponse(Encoding.ASCII.GetBytes(setLanguage), 500, 500);
			string s = Encoding.ASCII.GetString(response);
			if (!s.Contains("zpl"))
			{
				Log.Information("Not a ZPL printer.");
				return false;
			}
			return true;
		}

		private bool CheckPrinterStatus(IConnection connection)
		{
			IZebraPrinter printer = ZebraPrinterFactory.Current.GetInstance(PrinterLanguage.ZPL, connection);
			IPrinterStatus status = printer.CurrentStatus;
			if (!status.IsReadyToPrint)
			{
				Log.Information("Printer in Error: " + status);
			}
			return true;
		}
	}
}
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

using PrismApp;
using ZXing.Mobile;

namespace PrismApp.Droid
{
	class ScanBarcode : IScanBarcode
	{
		public async Task<string> Scan()
		{
			var scanner = new ZXing.Mobile.MobileBarcodeScanner();

			var result = await scanner.Scan();

			return result?.Text;
		}
	}
}
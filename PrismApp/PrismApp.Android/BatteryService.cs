using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using PrismApp;

namespace PrismApp.Droid
{
	public class BatteryService : IBatteryService
	{
		public string GetBatteryStatus()
		{
			return "@todo: Battery status";
		}
	}
}
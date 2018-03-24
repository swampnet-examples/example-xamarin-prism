using PrismApp;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace PrismApp.iOS
{
	public class BatteryService : IBatteryService
	{
		public string GetBatteryStatus()
		{
			return UIDevice.CurrentDevice.BatteryState.ToString();
		}
	}
}

using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PrismApp.HistorySink
{
	public class Thing
	{
		public DateTime Timestamp { get; set; }
		public string Message { get; set; }
		public LogEventLevel Level { get; internal set; }
	}

	public interface ILogHistory
	{
		ObservableCollection<Thing> Events { get; }
	}

	class LogHistory : ILogHistory
	{
		public LogHistory()
		{
			Events = new ObservableCollection<Thing>();
			Events.CollectionChanged += Events_CollectionChanged;
		}


		public ObservableCollection<Thing> Events { get; private set; }

		private void Events_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			// @TODO Trunc history, enter recursive hell!
		}
	}
}

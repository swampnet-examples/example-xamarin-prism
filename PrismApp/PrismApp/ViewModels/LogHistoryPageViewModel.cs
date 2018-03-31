using Prism.Commands;
using Prism.Mvvm;
using PrismApp.HistorySink;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PrismApp.ViewModels
{
	public class LogHistoryPageViewModel : BindableBase
	{
		private readonly ILogHistory _logHistory;

		public LogHistoryPageViewModel(ILogHistory logHistory)
        {
			_logHistory = logHistory;
		}

		public ObservableCollection<Thing> History => _logHistory.Events;
	}
}

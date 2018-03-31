using PrismApp.HistorySink;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismApp.Serilog.HistorySink
{
	class HistorySink : ILogEventSink
	{
		private readonly IFormatProvider _formatProvider;
		private readonly ILogHistory _logHistory;

		public HistorySink(ILogHistory logHistory, IFormatProvider formatProvider)
		{
			_logHistory = logHistory;
			_formatProvider = formatProvider;
		}

		public void Emit(LogEvent logEvent)
		{
			_logHistory.Events.Add(new Thing() {
				Timestamp = logEvent.Timestamp.DateTime,
				Message = logEvent.RenderMessage(_formatProvider),
				Level = logEvent.Level
			});
		}
	}
}

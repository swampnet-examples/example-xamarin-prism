using PrismApp.HistorySink;
using PrismApp.Serilog.HistorySink;
using Serilog.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serilog
{
    static class HistorySinkExtensions
    {
		public static LoggerConfiguration HistorySink(
			  this LoggerSinkConfiguration loggerConfiguration,
			  ILogHistory logHistory,
			  IFormatProvider formatProvider = null)
		{
			return loggerConfiguration.Sink(new HistorySink(logHistory, formatProvider));
		}
	}
}

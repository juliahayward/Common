using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace JuliaHayward.Common.Logging
{
    public class EventLogLogger : ILogger
    {
        public void Error(string appName, string message, string detail)
        {
            // Warning - this may require elevated permissions
            if (!EventLog.SourceExists(appName))
                EventLog.CreateEventSource(appName, "Application");

            var text = message + System.Environment.NewLine + message;
            EventLog.WriteEntry(appName, text, EventLogEntryType.Error);
        }
    }
}

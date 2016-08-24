using JuliaHayward.Common.Environment;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace JuliaHayward.Common.Logging
{
    public class EventLogLogger : ILogger
    {
        public void Error(string appName, Exception exception)
        {
            var nestedStackTrace = exception.StackTrace;
            var nestedMessage = exception.Message;
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
                nestedStackTrace = exception.StackTrace + System.Environment.NewLine + "-----"
                    + System.Environment.NewLine + nestedStackTrace;
                nestedMessage = exception.Message;
            }

            Error(appName, nestedMessage, nestedStackTrace);
        }

        public void Error(string appName, string message, string detail)
        {
            if (JuliaEnvironment.CurrentEnvironment == EnvironmentType.Dev) return;

            // Warning - this may require elevated permissions
            if (!EventLog.SourceExists(appName))
                EventLog.CreateEventSource(appName, "Application");

            var text = message + System.Environment.NewLine + message;
            EventLog.WriteEntry(appName, text, EventLogEntryType.Error);
        }
    }
}

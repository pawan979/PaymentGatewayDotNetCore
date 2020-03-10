using PaymentGateway.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PaymentGateway.Services
{
    public class EventViewerLogger : ILogger
    {
        public void LogError(string message)
        {
            using (EventLog eventLog = new EventLog(PaymentGateway.Services.Common.Constants.ApplicationName))
            {
                eventLog.Source = "Application";
                eventLog.WriteEntry(message, EventLogEntryType.Error);
            }
        }

        public void LogInfo(string message)
        {
            using (EventLog eventLog = new EventLog(PaymentGateway.Services.Common.Constants.ApplicationName))
            {
                eventLog.Source = "Application";
                eventLog.WriteEntry(message, EventLogEntryType.Information);
            }
        }
    }
}

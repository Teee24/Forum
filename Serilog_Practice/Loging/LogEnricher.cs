using Serilog.Core;
using Serilog.Events;
using System.Security.Cryptography.X509Certificates;

namespace Serilog_Practice.Loging
{
    public class LogEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            //var logEventLevel = logEvent.Level;
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("LogLevel",logEvent.Level));

        }
    }
}

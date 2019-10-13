using System;
using Abp.Dependency;
using Abp.Logging;
using Abp.UI;
using Microsoft.Extensions.Options;

namespace ResponsibleSystem.Exceptions
{
    public class CriticalException : UserFriendlyException
    {
        private static ErrorLogsSettings LogsSettings
        {
            get {
                var settings = IocManager.Instance.Resolve<IOptions<ErrorLogsSettings>>();
                return settings.Value;
            }
        }

        public string MessageToLog { get; }

        public CriticalException() 
            : base(LogsSettings.DefaultUserFriendlyMessage, LogsSettings.DefaultUserFriendlyDescription)
        {
            
        }

        public CriticalException(string message) 
            : base(LogsSettings.DefaultUserFriendlyMessage, LogsSettings.DefaultUserFriendlyDescription)
        {
            MessageToLog = message;
        }

        public CriticalException(string message, Exception exception) 
            : base(LogsSettings.DefaultUserFriendlyMessage, LogsSettings.DefaultUserFriendlyDescription, exception)
        {
            MessageToLog = message;
            Severity = LogSeverity.Fatal;
        }
    }
}

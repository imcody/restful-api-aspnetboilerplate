using System;
using Abp.UI;
using Microsoft.Extensions.Logging;

namespace ResponsibleSystem.Exceptions
{
    public class ResponsibleSystemUserFriendlyException : UserFriendlyException
    {
        public string MessageToLog { get; }
        public string MessageToShow => Message;
        public LogLevel LogLevel { get; set; }

        public ResponsibleSystemUserFriendlyException()
        { }

        public ResponsibleSystemUserFriendlyException(string messageToShow, LogLevel logLevel = LogLevel.Warning) : base(messageToShow)
        {
            MessageToLog = messageToShow;
            LogLevel = logLevel;
        }

        public ResponsibleSystemUserFriendlyException(string messageToLog, string messageToShow, LogLevel logLevel = LogLevel.Warning) : base(messageToShow) 
        {
            MessageToLog = messageToLog;
            LogLevel = logLevel;
        }

        public ResponsibleSystemUserFriendlyException(string messageToLog, string messageToShow, string descriptionToShow, LogLevel logLevel = LogLevel.Warning) : base(messageToShow, descriptionToShow)
        {
            MessageToLog = messageToLog;
            LogLevel = logLevel;
        }

        public ResponsibleSystemUserFriendlyException(string messageToLog, Exception exception, string messageToShow, LogLevel logLevel = LogLevel.Warning)
            : base(messageToShow, exception)
        {
            MessageToLog = messageToLog;
            LogLevel = logLevel;
        }

        public ResponsibleSystemUserFriendlyException(string messageToLog, Exception exception, string messageToShow, string descriptionToShow, LogLevel logLevel = LogLevel.Warning) 
            : base(messageToShow, descriptionToShow, exception)
        {
            MessageToLog = messageToLog;
            LogLevel = logLevel;
        }
    }
}

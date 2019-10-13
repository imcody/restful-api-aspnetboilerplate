using System;
using Abp.Events.Bus.Exceptions;
using Abp.Events.Bus.Handlers;
using Abp.UI;
using Microsoft.Extensions.Logging;

namespace ResponsibleSystem.Exceptions
{
    public class ExceptionEventHandler : IEventHandler<AbpHandledExceptionData>
    {
        private readonly ILogger _logger;

        public ExceptionEventHandler(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ExceptionEventHandler>();
        }

        public void HandleEvent(AbpHandledExceptionData eventData)
        {
            if (eventData.Exception is CriticalException)
            {
                var ex = eventData.Exception as CriticalException;
                Log(LogLevel.Critical, ex, ex.MessageToLog);
            }
            else if (eventData.Exception is ResponsibleSystemUserFriendlyException)
            {
                var ex = eventData.Exception as ResponsibleSystemUserFriendlyException;
                Log(ex.LogLevel, eventData.Exception, ex.MessageToLog);
            }
            else if (eventData.Exception is UserFriendlyException)
            {
                Log(LogLevel.Warning, eventData.Exception, eventData.Exception.Message);
            }
            else
            {
                Log(LogLevel.Error, eventData.Exception, eventData.Exception.Message);
            }
        }

        protected void Log(LogLevel level, Exception ex, string message)
        {
            switch (level)
            {
                case LogLevel.Critical:
                    _logger.LogCritical(ex, message);
                    break;

                case LogLevel.Warning:
                    _logger.LogWarning(ex, message);
                    break;

                case LogLevel.Information:
                    _logger.LogInformation(ex, message);
                    break;

                case LogLevel.Debug:
                    _logger.LogDebug(ex, message);
                    break;

                case LogLevel.Trace:
                    _logger.LogTrace(ex, message);
                    break;

                default:
                    _logger.LogError(ex, message);
                    break;
            }
        }
    }
}

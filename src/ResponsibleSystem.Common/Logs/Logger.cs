using Common.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResponsibleSystem.Common.Logs
{
    public class Logger : ILogger
    {
        private delegate void LoggerDelegate(string log);
        Dictionary<LogLevel, LoggerDelegate> _loggers = new Dictionary<LogLevel, LoggerDelegate>();

        public Logger(ILog log)
        {
            _loggers.Add(LogLevel.Debug, CreateLogger(LogLevel.Debug, log.Debug));
            _loggers.Add(LogLevel.Error, CreateLogger(LogLevel.Error, log.Error));
            _loggers.Add(LogLevel.Fatal, CreateLogger(LogLevel.Fatal, log.Fatal));
            _loggers.Add(LogLevel.Info, CreateLogger(LogLevel.Info, log.Info));
            _loggers.Add(LogLevel.Trace, CreateLogger(LogLevel.Trace, log.Trace));
            _loggers.Add(LogLevel.Warn, CreateLogger(LogLevel.Warn, log.Warn));
        }

        public void Debug(string msg, Exception e = null) => LogMessage(msg, e, LogLevel.Debug);
        public void Error(string msg, Exception e = null) => LogMessage(msg, e, LogLevel.Error);
        public void Fatal(string msg, Exception e = null) => LogMessage(msg, e, LogLevel.Fatal);
        public void Info(string msg) => LogMessage(msg, null, LogLevel.Info);
        public void Trace(string msg) => LogMessage(msg, null, LogLevel.Trace);
        public void Warn(string msg, Exception e = null) => LogMessage(msg, e, LogLevel.Warn);

        private void LogMessage(string message, Exception e, LogLevel logLevel)
        {
            LoggerDelegate logger;
            _loggers.TryGetValue(logLevel, out logger);
            if (e != null)
            {
                message += "\n" + LogException(e);
            }
            logger?.Invoke(message);
        }

        protected virtual string LogException(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            var curr = ex;
            while (curr != null)
            {
                sb.AppendLine(curr.Message);
                curr = curr.InnerException;
            }
            return sb.ToString();
        }

        LoggerDelegate CreateLogger(LogLevel logLevel, Action<string> logger)
        {
            return (message) =>
            {
                Console.WriteLine($"{DateTime.Now.ToString()} [{logLevel.ToString().ToUpper()}] " + message);
                logger(message);
            };
        }
    }
}

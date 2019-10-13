using System;

namespace ResponsibleSystem.Common.Logs
{
    public interface ILogger
    {
        void Debug(string msg, Exception e = null);
        void Error(string msg, Exception e = null);
        void Fatal(string msg, Exception e = null);
        void Info(string msg);
        void Trace(string msg);
        void Warn(string msg, Exception e = null);
    }
}

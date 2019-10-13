//using System;
//using System.Collections.Generic;
//using System.Text;
//using Microsoft.Azure.WebJobs.Host;

//namespace ResponsibleSystem.Common.Logs
//{
//    public class TraceWriterLogger : ILogger
//    {
//        private readonly ILogger logger;
//        private readonly TraceWriter tw;

//        public TraceWriterLogger(ILogger logger, TraceWriter tw)
//        {
//            this.logger = logger;
//            this.tw = tw;
//        }
//        public void Debug(string msg, Exception e = null)
//        {
//            logger.Debug(msg, e);
//            tw.Info(msg);
//        }

//        public void Error(string msg, Exception e = null)
//        {
//            logger.Error(msg, e);
//            tw.Error(msg);
//        }

//        public void Fatal(string msg, Exception e = null)
//        {
//        {
//            logger.Fatal(msg, e);
//            tw.Error(msg);
//        }

//        public void Info(string msg)
//        {
//            logger.Info(msg);
//            tw.Info(msg);
//        }

//        public void Trace(string msg)
//        {
//            logger.Trace(msg);
//            tw.Info(msg);
//        }

//        public void Warn(string msg, Exception e = null)
//        {
//            logger.Warn(msg,e);
//            tw.Warning(msg);
//        }
//    }
//}

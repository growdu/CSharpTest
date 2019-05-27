using System;
using System.Collections.Generic;
using System.Text;

namespace Helper
{
    /// <summary>
    /// 使用Log4net插件的log日志对象
    /// </summary>
    public static class Log
    {
        private static log4net.ILog error;
        private static log4net.ILog info;

        static Log()
        {
            log4net.Config.XmlConfigurator.Configure();
            error = log4net.LogManager.GetLogger("logerror");
            info = log4net.LogManager.GetLogger("loginfo");
        }

        public static void Debug(object message)
        {
            info.Debug(message);
        }

        public static void DebugFormatted(string format, params object[] args)
        {
            info.DebugFormat(format, args);
        }

        public static void Info(object message)
        {
            info.Info(message);
        }

        public static void InfoFormatted(string format, params object[] args)
        {
            info.InfoFormat(format, args);
        }

        public static void Warn(object message)
        {
            info.Warn(message);
        }

        public static void Warn(object message, Exception exception)
        {
            info.Warn(message, exception);
        }

        public static void WarnFormatted(string format, params object[] args)
        {
            info.WarnFormat(format, args);
        }

        public static void Error(object message)
        {
            error.Error(message);
        }

        public static void Error(object message, Exception exception)
        {
            error.Error(message, exception);
        }

        public static void ErrorFormatted(string format, params object[] args)
        {
            error.ErrorFormat(format, args);
        }

        public static void Fatal(object message)
        {
            error.Fatal(message);
        }

        public static void Fatal(object message, Exception exception)
        {
            error.Fatal(message, exception);
        }

        public static void FatalFormatted(string format, params object[] args)
        {
            error.FatalFormat(format, args);
        }
    }
}

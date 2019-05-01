using System;
using System.IO;
using log4net;
using log4net.Config;

namespace BusinessInfoSpider.Utility
{
    /// <summary>
    /// 使用Log4net插件的log日志对象
    /// </summary>
    public static class AppLog
    {
        private static ILog Log;

        static AppLog()
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile));
            Log = LogManager.GetLogger(typeof(AppLog));
        }

        public static void Debug(string message)
        {
            Log.Debug(message);
        }

        public static void DebugFormatted(string format, params object[] args)
        {
            Log.DebugFormat(format, args);
        }

        public static void Info(string message)
        {
            Log.Info(message);
        }

        public static void InfoFormatted(string format, params object[] args)
        {
            Log.InfoFormat(format, args);
        }

        public static void Warn(string message)
        {
            Log.Warn(message);
        }

        public static void Warn(string message, Exception exception)
        {
            Log.Warn(message, exception);
        }

        public static void WarnFormatted(string format, params object[] args)
        {
            Log.WarnFormat(format, args);
        }

        public static void Error(string message)
        {
            Log.Error(message);
        }

        public static void Error(string message, Exception exception)
        {
            Log.Error(message, exception);
        }

        public static void ErrorFormatted(string format, params object[] args)
        {
            Log.ErrorFormat(format, args);
        }

        public static void Fatal(string message)
        {
            Log.Fatal(message);
        }

        public static void Fatal(string message, Exception exception)
        {
            Log.Fatal(message, exception);
        }

        public static void FatalFormatted(string format, params object[] args)
        {
            Log.FatalFormat(format, args);
        }
    }
}

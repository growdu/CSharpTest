using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileTest.Log
{
    public class LogTest
    {
        static void Main(string[] args)
        {
            Log.Info(" login success");
            Log.Error("This is an error");
            //Log.Warn("This is a warning");
            Log.Fatal("This is a fatal");
            //AppLog.Info("Info log");
            //AppLog.Error("Error log");
            //XmlConfigurator.Configure();
            ////XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile));
            //log4net.ILog log = log4net.LogManager.GetLogger("testApp.Logging");
            //log.Info(DateTime.Now.ToString() + ": login success");
        }
    }
}

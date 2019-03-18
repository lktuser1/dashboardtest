using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using NLog;

namespace EDS.Intranet.Common
{
    public class Logger
    {
        //Instantiate a default logger.
        private static NLog.Logger nLogger = LogManager.GetLogger("dashboard");

        public static void writeLog(string message)
        {
            nLogger.Info(message);
        }

        public static void writeDebug(string message)
        {
            nLogger.Debug(message);
        }

        public static void writeError(string message)
        {
            nLogger.Error(message);
        }

        public static void writeError(string message, Exception ex)
        {
            nLogger.Error(message);
            processException(ex);
        }

        public static void processException(Exception ex)
        {
            string message = "Exception Details " + "\r\n" +
                ex.Message + "\r\n" + ex.Source + "\r\n" + ex.StackTrace + "\r\n" + ex.TargetSite;
            nLogger.Error(message);
        }
    }
}

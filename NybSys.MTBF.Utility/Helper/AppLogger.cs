using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace NybSys.MTBF.Utility.Helper
{
    public static class AppLogger
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger("NybSys.MTBF.Logger");
        private static object _locker = new object();

        /// <summary>
        /// Method to write log into log file.
        /// </summary>
        /// <param name="message"></param>
        public static void WriteLog(string message, object sender)
        {
            Action<string> asynCall = new Action<string>(Write);
            asynCall.BeginInvoke(sender.GetType() + Environment.NewLine + message, null, null);
        }

        /// <summary>
        /// Method to write log.
        /// </summary>
        /// <param name="message"></param>
        public static void WriteLog(string message)
        {
            Action<string> asynCall = new Action<string>(Write);
            asynCall.BeginInvoke(message, null, null);
        }

        /// <summary>
        /// Method use log4net to write log.
        /// </summary>
        /// <param name="message"></param>
        private static void Write(string message)
        {
            lock (_locker)
            {
                try
                {
                    _log.Error(message);
                }
                catch (System.Exception ex)
                {
                    EventLog.WriteEntry("NybSys.MTBF", ex.Message);
                }

            }
        }
    }
}

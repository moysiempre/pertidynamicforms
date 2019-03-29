using NLog;
using System;

namespace FormsAdminGP.Services
{
    public class LoggerService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static void LogToFile(Exception ex)
        {
            logger.Error("Error on {0} \n {1} \n {2} \n {3}", ex.Message, ex.StackTrace, ex.InnerException != null ? ex.InnerException.Message : string.Empty, ex.InnerException != null ? ex.InnerException.StackTrace : string.Empty);
        }
    }
}

using NLog;

namespace FormsAdminGP.Services
{
    public class LoggerService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public void LogToFileTest()
        {
            logger.Info("HELLO WORLD");
        }
    }
}

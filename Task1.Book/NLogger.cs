using System;
using NLog;

namespace Task1.Book
{
    public class NLogger : ILogger
    {
        private Logger logger;

        public NLogger(Logger logger = null)
        {

            this.logger = logger ?? LogManager.GetCurrentClassLogger();
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Debug(string message, params object[] args)
        {
            logger.Debug(message, args);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Error(string message, params object[] args)
        {
            logger.Error(message, args);
        }

        public void Fatal(string message)
        {
            logger.Fatal(message);
        }

        public void Fatal(string message, params object[] args)
        {
            logger.Fatal(message, args);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Info(string message, params object[] args)
        {
            logger.Info(message, args);
        }

        public void Trace(string message)
        {
            logger.Trace(message);
        }

        public void Trace(string message, params object[] args)
        {
            logger.Trace(message, args);
        }
    }
}

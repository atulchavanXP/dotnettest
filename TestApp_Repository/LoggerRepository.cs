using System;

namespace TestApp_Repository
{
    public interface ILoggerRepository
    {
        void Log(string message, Exception ex);
        void Log(string message);
    }

    public class LoggerRepository : ILoggerRepository
    {
        public void Log(string message, Exception ex)
        {
            throw new NotImplementedException();
        }

        public void Log(string message)
        {
            throw new NotImplementedException();
        }
    }
}

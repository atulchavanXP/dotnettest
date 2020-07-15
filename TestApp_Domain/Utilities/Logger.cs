using System;
using System.IO;
using System.Text;

namespace TestApp_Domain.Utilities
{
    public interface ILogger
    {
        void Log(string message, Exception ex);
        void Log(string message);
    }

    public class Logger : ILogger
    {
        /// <summary>
        /// log error with exception details
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void Log(string message, Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Message: {message}");
            sb.AppendLine($"Inner Exception: {ex.InnerException}");
            sb.AppendLine($"Stack Trace: {ex.StackTrace}");

            WriteLog(sb.ToString());
        }

        /// <summary>
        /// log error message
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {
            WriteLog(message);
        }

        /// <summary>
        /// write logs to text file
        /// </summary>
        /// <param name="message"></param>
        private void WriteLog(string message)
        {
            string logFilePath = @"C:\Test_MVC_Logs\Log-" + DateTime.Today.ToString("MM-dd-yyyy") + "." + "txt";
            FileInfo logFileInfo = new FileInfo(logFilePath);
            DirectoryInfo logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);

            //create log directory if not present
            if (!logDirInfo.Exists) logDirInfo.Create();

            //create logs, if present append logs to error file
            using (FileStream fileStream = new FileStream(logFilePath, FileMode.Append))
            {
                using (StreamWriter log = new StreamWriter(fileStream))
                {
                    log.WriteLine($"----- {DateTime.Now} -----");
                    log.WriteLine(message);
                }
            }
        }
    }
}

using PaymentGateway.Services.Interfaces;
using System;
using System.IO;

namespace PaymentGateway.Services
{
    public class FileLogger : ILogger
    {
        public void LogError(string message)
        {
            string filePath = "C:\\Logs\\PaymentGateway";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            using (StreamWriter sw = File.AppendText(string.Format(@"{0}\LogError.txt", filePath)))
            {
                sw.WriteLine(string.Format("Error On: {0} - {1}", DateTime.Now, message));
            }
        }

        public void LogInfo(string message)
        {
            string filePath = "C:\\Logs\\PaymentGateway";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            using (StreamWriter sw = File.AppendText(string.Format(@"{0}\LogError.txt", filePath)))
            {
                sw.WriteLine(string.Format("Logged On: {0} - {1}", DateTime.Now, message));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PaymentGateway.Services.Interfaces;

namespace PaymentGateway.Services.Log
{
    public class SystemLog : ILogger
    {
        public void LogError(string message)
        {
            string filePath = "C:\\Logs\\PaymentGateway\\SystemError";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            using (StreamWriter sw = File.AppendText(string.Format(@"{0}\LogError.txt", filePath)))
            {
                sw.WriteLine(string.Format("Error on: {0} - {1}", DateTime.Now, message));
            }
        }

        public void LogInfo(string message)
        {
            string filePath = "C:\\Logs\\PaymentGateway\\SystemError";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            using (StreamWriter sw = File.AppendText(string.Format(@"{0}\LogError.txt", filePath)))
            {
                sw.WriteLine(string.Format("Logged on: {0} - {1}", DateTime.Now, message));
            }
        }
    }
}

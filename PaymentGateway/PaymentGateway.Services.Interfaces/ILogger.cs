using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Services.Interfaces
{
    public interface ILogger
    {
        public void LogError(string Message);
        public void LogInfo(string Message);
    }
}

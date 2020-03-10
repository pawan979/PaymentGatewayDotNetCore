using PaymentGateway.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Services.Common
{
    public static class CommonMethods
    {
        
        public static string MaskCardNumber(ILogger log, string cardNumber)
        {
            string maskedCardNumber = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(cardNumber))
                {
                    maskedCardNumber = string.Format("{0}-xxxx-xxxx-{1}", cardNumber.Substring(0, 4), cardNumber.Substring(cardNumber.Length - 4, 4));
                }
                else
                {
                    log.LogError(string.Format(Constants.InvalidCardNumber));
                }

            }
            catch (Exception ex)
            {
                log.LogError(string.Format(Constants.DateValidationError, ex.StackTrace));
            }

            return maskedCardNumber;
        }
    }
}

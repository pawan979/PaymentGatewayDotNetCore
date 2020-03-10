using PaymentGateway.Domain.Entities;
using PaymentGateway.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace PaymentGateway.Services.DataService
{
    public static class DataRetrieval
    {
        public static TransactionSummary RetrieveTransaction(ILogger log, string transactionId)
        {
            TransactionSummary response = null;

            try
            {
                response = Common.CommonMethods.GetTransaction(log, transactionId);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);

            }
            return response;
        }
    }
}

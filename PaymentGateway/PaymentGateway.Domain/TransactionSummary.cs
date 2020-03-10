using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Domain.Entities
{
    public class TransactionSummary : Transaction
    {
        public string TransactionId { get; set; }
        public int HttpStatusCode { get; set; }
        public bool TransactionSucceed { get; set; }
        public string Message { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Domain.Entities
{
    public class Transaction
    {
        public double Amount { get; set; }
        public CardDetails CardDetail { get; set; }
    }
}

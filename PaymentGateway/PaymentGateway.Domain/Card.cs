using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Domain.Entities
{
    public class CardDetails
    {
        public string CCV { get; set; }
        public string ExpiryDate { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }

    }
}

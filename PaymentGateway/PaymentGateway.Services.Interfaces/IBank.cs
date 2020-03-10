using PaymentGateway.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Services.Interfaces
{
    public interface IBank
    {
        public BankResponse ProcessPayment(Transaction transaction);
    }
}

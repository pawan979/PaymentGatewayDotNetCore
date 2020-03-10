using PaymentGateway.Domain.Entities;
using PaymentGateway.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Services.Bank
{
    public class MCB : IBank
    {
        private readonly ILogger _Log;

        public MCB(ILogger log)
        {
            _Log = log;
        }

        public BankResponse ProcessPayment(Transaction transaction)
        {
            BankResponse response = null;

            try
            {
                transaction.TransactionTimeStamp = DateTime.Now;
                //Call Bank API, for this POC, I mocked the response to be either pass or fail
                if (Common.Validation.IsCardExpired(_Log, Convert.ToDateTime(transaction.CardDetail.ExpiryDate)))
                {
                    //Initiate Bank API call
                    // for this POC, I simulated transaction to pass if amount if positive
                    if (transaction.Amount % 2 == 0)
                    {
                        response = new BankResponse() { TransactionId = Guid.NewGuid(), HttpStatusCode = 200, TransactionSucceed = true };
                    }
                    else
                    {
                        response = new BankResponse() { TransactionId = Guid.NewGuid(), HttpStatusCode = 400, TransactionSucceed = false };
                    }
                }
                else
                {
                    response = new BankResponse() { TransactionId = Guid.NewGuid(), HttpStatusCode = 422, TransactionSucceed = false, Message = Common.Constants.CardExipred };
                }

            }
            catch (Exception ex)
            {
                _Log.LogError(ex.Message);
            }
            return response;
        }

    }
}

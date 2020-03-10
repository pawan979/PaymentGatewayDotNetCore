using PaymentGateway.Domain.Entities;
using PaymentGateway.Services.Bank;
using PaymentGateway.Services.Interfaces;
using System;
using System.Threading.Tasks.Sources;

namespace PaymentGateway.Services.BankServices
{
    public class BankServices
    {
        public static IBank GetBank(Transaction transaction)
        {
            string bankCode = string.Empty;
            IBank bank = null;

            if (transaction.CardDetail == null)
                throw new Exception(Common.Constants.CardNotAvailable);

            var cardNumber = transaction.CardDetail.CardNumber;

            if (!string.IsNullOrEmpty(cardNumber))
            {
                bankCode = cardNumber.Substring(0, 4);

                switch (bankCode)
                {
                    case "1234":
                        bank = new HSBC(new FileLogger());
                        break;

                    case "4567":
                        bank = new MCB(new EventViewerLogger());
                        break;

                    default:
                        throw new Exception(Common.Constants.BankUnavailable);
                }
            }

            return bank;
        }

    }
}

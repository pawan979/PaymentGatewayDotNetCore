using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Services.Common
{
    public static class Constants
    {

        public const string ApplicationName = "Payment Gateway";

        public const string CardExipred = "The card has Expired.  Please contact your Bank.";
        public const string CardNotAvailable = "The card details could not be retrieved.  Please contact your Bank.";

        public const string DateNotAvailable = "The card Expiry date could not be retrieved.";
        public const string DateValidationError = "An error occured while validating the date.  Please see stacktrace below {0}.";

        public const string InvalidCardNumber = "The card number is invalid.";
        public const string BankUnavailable = "The bank details could not be retrieved.  Please contact your bank.";
        public const string TransactionNotAvailable = "The transaction could not be found.";
        public const string UserBankAccountNotFound= "User bank detail could not be retrieved.";
        public const string InsufficientAmountInAccount = "Insufficient fund in Account to perfrom transaction.";
    }
}

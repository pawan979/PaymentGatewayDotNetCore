using Newtonsoft.Json;
using PaymentGateway.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Services.Common
{
    public static class CommonMethods
    {
        public static void SaveTransaction(ILogger log, Transaction transaction, BankResponse response)
        {
            List<TransactionSummary> transactions = new List<TransactionSummary>();
            try
            {
                string filePath = @"C:\Transactions";

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                string file = string.Format("{0}\\{1}", filePath, "Transactions.json");

                if (File.Exists(file))
                {
                    var json = File.ReadAllText(file);
                    transactions = JsonConvert.DeserializeObject<List<TransactionSummary>>(json);
                }

                transactions.Add(new TransactionSummary()
                {
                    Amount = transaction.Amount,
                    TransactionTimeStamp = transaction.TransactionTimeStamp,
                    CardDetail = new CardDetails()
                    {
                        CardNumber = transaction.CardDetail.CardNumber,
                        CardType = transaction.CardDetail.CardType
                    },
                    TransactionId = response.TransactionId.ToString(),
                    HttpStatusCode = response.HttpStatusCode,
                    Message = response.Message,
                    TransactionSucceed = response.TransactionSucceed
                });

                File.WriteAllText(file, JsonConvert.SerializeObject(transactions));
            }
            catch (Exception ex)
            {
                log.LogError(string.Format(Constants.TransactionNotAvailable, ex.StackTrace));

            }

        }

        public static TransactionSummary GetTransaction(ILogger log, string transactionId)
        {
            List<TransactionSummary> transactions = new List<TransactionSummary>();

            TransactionSummary transaction = null;

            try
            {
                string filePath = @"C:\Transactions";


                string file = string.Format("{0}\\{1}", filePath, "Transactions.json");

                if (File.Exists(file))
                {
                    var json = File.ReadAllText(file);
                    transactions = JsonConvert.DeserializeObject<List<TransactionSummary>>(json);

                    transaction = transactions.Where(t => t.TransactionId == transactionId).First();

                    if (transaction != null)
                        transaction.CardDetail.CardNumber = MaskCardNumber(log, transaction.CardDetail.CardNumber);

                }

            }
            catch (Exception ex)
            {
                log.LogError(string.Format(Constants.ErrorWhenRetrievingTransaction, Common.Constants.TransactionNotAvailable));

                transaction = new TransactionSummary()
                {
                    TransactionSucceed = false,
                    HttpStatusCode = 403,
                    Message = Common.Constants.TransactionNotAvailable
                };
            }

            return transaction;
        }

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

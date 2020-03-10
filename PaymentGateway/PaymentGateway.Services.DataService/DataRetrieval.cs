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

            //Mocked data for stored transaction in Database
            IList<TransactionSummary> allTransactions = new List<TransactionSummary>()
            {
                new TransactionSummary(){TransactionSucceed=true, HttpStatusCode = 200, TransactionId = "a2b6dac5-f4bf-45f1-9619-809f3c5a2309" ,Amount = 112.5, CardDetail = new CardDetails() { CardNumber = Common.CommonMethods.MaskCardNumber(log, "1236-5478-9369-8527"), CardType = "Visa"} },
                new TransactionSummary(){TransactionSucceed=true, HttpStatusCode = 200,TransactionId = "dbd71761-771b-44db-9fed-5c4adbd91fb9" ,Amount = 1.5, CardDetail = new CardDetails() { CardNumber = Common.CommonMethods.MaskCardNumber(log, "9876-5432-1147-2583"), CardType = "MasterCard"} },
                new TransactionSummary(){TransactionSucceed=true, HttpStatusCode = 200,TransactionId = "a7694f88-d254-46d5-8c9c-32c461f48c00" ,Amount = 14.5, CardDetail = new CardDetails() { CardNumber = Common.CommonMethods.MaskCardNumber(log, "4567-8912-3741-8529"), CardType = "Visa" } },
                new TransactionSummary(){TransactionSucceed=true, HttpStatusCode = 200,TransactionId = "fdfa2aae-3ba1-436d-8c91-e93e2026009a" ,Amount = 10.5,CardDetail = new CardDetails() {  CardNumber = Common.CommonMethods.MaskCardNumber(log, "6549-8732-1963-8527"), CardType = "Visa"} },
                new TransactionSummary(){TransactionSucceed=false, HttpStatusCode = 430, Message = Common.Constants.UserBankAccountNotFound,TransactionId = "fd9b4963-2dc7-4da1-a447-9297b1433980" ,Amount = 5.0, CardDetail = new CardDetails() { CardNumber = Common.CommonMethods.MaskCardNumber(log, "7891-2546-3147-2586"), CardType = "MasterCard"} },
                new TransactionSummary(){TransactionSucceed=true,HttpStatusCode = 200, TransactionId = "9c3d559f-0979-4a3a-8eca-0d08f1ca0b68" ,Amount = 12.5,CardDetail = new CardDetails() {  CardNumber = Common.CommonMethods.MaskCardNumber(log, "3216-5497-8148-2593"), CardType = "MasterCard" } },
                new TransactionSummary(){TransactionSucceed=true, HttpStatusCode = 200,TransactionId = "cffdbba5-58f4-459e-a679-3a2920b66578" ,Amount = 13, CardDetail = new CardDetails() { CardNumber = Common.CommonMethods.MaskCardNumber(log, "1231-2312-3547-3265"), CardType = "MasterCard" } },
                new TransactionSummary(){TransactionSucceed=false, HttpStatusCode = 435,Message =Common.Constants.InsufficientAmountInAccount  ,TransactionId = "d41ce179-4765-4752-9fed-2b91c7e7f670" ,Amount = 1000,CardDetail = new CardDetails() {  CardNumber = Common.CommonMethods.MaskCardNumber(log, "9586-3652-8748-8456"), CardType = "Visa"} }
            };

            try
            {
                response = allTransactions.Where(t => t.TransactionId == transactionId).First();
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                response = new TransactionSummary()
                    {
                        TransactionSucceed = false,
                        HttpStatusCode = 403,
                        Message = Common.Constants.TransactionNotAvailable
                    };
            }
            return response;
        }
    }
}

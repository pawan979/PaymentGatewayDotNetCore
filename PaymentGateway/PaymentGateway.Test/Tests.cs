using NUnit.Framework;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Infrastructure.API.Controllers;
using PaymentGateway.Services;
using PaymentGateway.Services.Common;
using System;
using FluentAssertions;
using PaymentGateway.Services.Log;

namespace PaymentGateway.Test
{
    public class Tests
    {
        private ProcessPaymentController _processPaymentController;
        private RetrieveTransactionDetailController _retrieveTransactionDetailController;

        [SetUp]
        public void Setup()
        {
            _processPaymentController = new ProcessPaymentController();
            _retrieveTransactionDetailController = new RetrieveTransactionDetailController();
        }

        [TestCase("1234-5678-9123-4567")]
        [Test]
        public void MaskCardNumber(string cardNumber)
        {
            FileLogger log = new FileLogger();

            try
            {
                var maskedCardNumber = CommonMethods.MaskCardNumber(log, cardNumber);
                Assert.AreEqual(maskedCardNumber, "1234-xxxx-xxxx-4567");

            }
            catch (Exception ex)
            {
                log.LogError(ex.Message + Environment.NewLine + ex.StackTrace);
            }

        }

        [Test]
        public void ProcessExpiredCard()
        {
            var transaction = new Transaction()
            {
                Amount = 10.00,
                CardDetail = new CardDetails()
                {
                    CardNumber = "1234567893692584",
                    CardType = "Visa",
                    CCV = "123",
                    ExpiryDate = "12/11/1981"
                }
            };

            var bankResponse = _processPaymentController.Post(transaction);

            var expectedResponse = new BankResponse()
            {
                HttpStatusCode = 422,
                Message = Constants.CardExipred,
                TransactionSucceed = false
            };
            bankResponse.Should().BeEquivalentTo(expectedResponse, options => options.Excluding(o => o.TransactionId));
        }

        [Test]
        public void PaymentProcessSuccessful()
        {
            var transaction = new Transaction()
            {
                Amount = 10.00,
                CardDetail = new CardDetails()
                {
                    CardNumber = "1234567893692584",
                    CardType = "Visa",
                    CCV = "123",
                    ExpiryDate = "12/11/2020"
                }
            };

            var bankResponse = _processPaymentController.Post(transaction);

            var expectedResponse = new BankResponse()
            {
                HttpStatusCode = 200,
                TransactionSucceed = true
            };

            bankResponse.Should().BeEquivalentTo(expectedResponse, options => options.Excluding(o => o.TransactionId));
        }

        [Test]
        public void PaymentProcessFailed()
        {
            var transaction = new Transaction()
            {
                Amount = 11.00,
                CardDetail = new CardDetails()
                {
                    CardNumber = "1234567893692584",
                    CardType = "Visa",
                    CCV = "123",
                    ExpiryDate = "12/11/2020"
                }
            };

            var bankResponse = _processPaymentController.Post(transaction);

            var expectedResponse = new BankResponse()
            {
                HttpStatusCode = 400,
                TransactionSucceed = false,
                Message = null,
            };

            bankResponse.Should().BeEquivalentTo(expectedResponse, options => options.Excluding(o => o.TransactionId));
        }

        [Test]
        public void GetAvailableTransaction()
        {

            var transactionAdd = new Transaction()
            {
                Amount = 10.00,
                CardDetail = new CardDetails()
                {
                    CardNumber = "1234567893692584",
                    CardType = "Visa",
                    CCV = "123",
                    ExpiryDate = "12/11/2020"
                }
            };

            var bankResponse = _processPaymentController.Post(transactionAdd);

            SystemLog log = new SystemLog();

            var transaction = _retrieveTransactionDetailController.Get(bankResponse.TransactionId.ToString());

            var expectedTransaction = new TransactionSummary()
            {
                TransactionSucceed = bankResponse.TransactionSucceed,
                HttpStatusCode = bankResponse.HttpStatusCode,
                TransactionId = bankResponse.TransactionId.ToString(),
                Amount = transactionAdd.Amount,
                TransactionTimeStamp = transactionAdd.TransactionTimeStamp,
                CardDetail = new CardDetails()
                {
                    CardNumber = CommonMethods.MaskCardNumber(log, transactionAdd.CardDetail.CardNumber),
                    CardType = transactionAdd.CardDetail.CardType
                }
            };

            transaction.Should().BeEquivalentTo(expectedTransaction);

        }

        [Test]
        public void GetNonExistantTransaction()
        {
            SystemLog log = new SystemLog();

            var transaction = _retrieveTransactionDetailController.Get("a2b6dac5-f4bf-45f1-5485-809f3c5a2309");

            var expectedTransaction = new TransactionSummary()
            {
                TransactionSucceed = false,
                HttpStatusCode = 403,
                Message = Constants.TransactionNotAvailable
            };

            transaction.Should().BeEquivalentTo(expectedTransaction);

        }
    }
}
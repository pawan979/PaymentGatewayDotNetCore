using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Services.Log;
using System;

namespace PaymentGateway.Infrastructure.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RetrieveTransactionDetailController : ControllerBase
    {
        // GET: api/ProcessPayment
        [HttpGet]
        public TransactionSummary Get(string transactionId)
        {
            SystemLog log = new SystemLog();

            TransactionSummary response = null;

            try
            {
                response = Services.DataService.DataRetrieval.RetrieveTransaction(log,transactionId);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
            }

            return response;
        }

    }
}
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Services.Interfaces;
using PaymentGateway.Services.Log;
using System;

namespace PaymentGateway.Infrastructure.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProcessPaymentController : ControllerBase
    {
        [HttpPost]
        public BankResponse Post([FromBody] Transaction transaction)
        {
            SystemLog log = new SystemLog();
            IBank bank = null;

            BankResponse response = null;

            try
            {
                bank = Services.BankServices.BankServices.GetBank(transaction); 

                if(bank != null)
                    response = bank.ProcessPayment(transaction);

            }
            catch(Exception ex)
            {
                if(bank == null)
                    response = new BankResponse() { Message = ex.Message , HttpStatusCode = 502 };

                log.LogError(ex.Message);
                response = new BankResponse() { HttpStatusCode = 501 };
            }

            return response;
        }
    }
}

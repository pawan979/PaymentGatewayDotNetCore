using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PaymentGateway.Domain.Entities;
using System;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Infrastructure.API
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<UserCredential> _userCredential;

        public AuthenticationMiddleware(RequestDelegate next, IOptions<UserCredential> userCredential)
        {
            _next = next;
            _userCredential = userCredential;
        }

        public async Task Invoke(HttpContext context, IConfiguration configuration)
        {
            string authHeader = context.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                //Extract credentials
                string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

                int seperatorIndex = usernamePassword.IndexOf(':');

                var username = usernamePassword.Substring(0, seperatorIndex);
                var password = usernamePassword.Substring(seperatorIndex + 1);

                if (username == _userCredential.Value.Username && password == _userCredential.Value.Password)
                {
                    await _next.Invoke(context);
                }
                else
                {
                    context.Response.StatusCode = 401; //Unauthorized
                    return;
                }
            }
            else
            {
                // no authorization header
                context.Response.StatusCode = 401; //Unauthorized
                return;
            }
        }
    }
}

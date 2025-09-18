using System;
using System.Net;

namespace MANDUU.Services.PaymentService
{
    public class PaystackApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public PaystackApiException(HttpStatusCode statusCode, string message)
            : base($"Paystack API Error ({statusCode}): {message}")
        {
            StatusCode = statusCode;
        }
    }
}
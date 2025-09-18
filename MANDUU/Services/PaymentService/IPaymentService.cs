using MANDUU.Models.PaymentModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MANDUU.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<List<MobileMoneyProvider>> GetAvailableMobileMoneyProviders(string countryCode = "GH");

        Task<PaymentTransactionResponse> InitializeMobileMoneyPayment(
            decimal amount,
            string email,
            string phone,
            string providerCode);

        Task<PaymentTransactionResponse> VerifyTransaction(string reference);

        Task<PaymentTransactionResponse> ChargeAuthorization(
            string authorizationCode,
            decimal amount,
            string email);
    }
}
using MANDUU.Models.PaymentModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://api.paystack.co";
        private readonly string _secretKey;
        private static List<MobileMoneyProvider> _cachedProviders;
        private static DateTime _lastFetchTime = DateTime.MinValue;

        public PaymentService(IConfiguration config)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };

            _secretKey = config["Paystack:SecretKey"];
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _secretKey);
        }

        public async Task<List<MobileMoneyProvider>> GetAvailableMobileMoneyProviders(string countryCode = "GH")
        {
            // Cache for an hour
            if (_cachedProviders != null && (DateTime.Now - _lastFetchTime).TotalHours < 1)
                return _cachedProviders;

            try
            {
                var response = await _httpClient.GetAsync($"/bank?currency={GetCurrencyCode(countryCode)}&type=mobile_money");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<BankPaymentResponse>(json);

                    _cachedProviders = result?.Data?
                        .Where(b => b.Type == "mobile_money")
                        .Select(b => new MobileMoneyProvider
                        {
                            Name = b.Name,
                            Code = b.Code,
                            CountryCode = countryCode
                        })
                        .ToList() ?? GetDefaultProviders(countryCode);

                    _lastFetchTime = DateTime.Now;
                    return _cachedProviders;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching providers: {ex.Message}");
            }

            return GetDefaultProviders(countryCode);
        }

        public async Task<PaymentTransactionResponse> InitializeMobileMoneyPayment(
            decimal amount,
            string email,
            string phone,
            string providerCode)
        {
            var request = new
            {
                amount = (int)(amount * 100), // Paystack uses the smallest unit
                email,
                mobile_money = new
                {
                    phone,
                    provider = providerCode
                },
                currency = GetCurrencyCode("GH") // Only Ghana
            };

            return await MakePaymentRequest("/charge", request);
        }

        public async Task<PaymentTransactionResponse> VerifyTransaction(string reference)
        {
            var response = await _httpClient.GetAsync($"/transaction/verify/{reference}");
            return await HandleResponse<PaymentTransactionResponse>(response);
        }

        public async Task<PaymentTransactionResponse> ChargeAuthorization(
            string authorizationCode,
            decimal amount,
            string email)
        {
            var request = new
            {
                amount = (int)(amount * 100),
                email,
                authorization_code = authorizationCode
            };

            return await MakePaymentRequest("/transaction/charge_authorization", request);
        }

        private async Task<PaymentTransactionResponse> MakePaymentRequest(string endpoint, object request)
        {
            var payload = JsonConvert.SerializeObject(request);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);
            return await HandleResponse<PaymentTransactionResponse>(response);
        }

        private async Task<T> HandleResponse<T>(HttpResponseMessage response) where T : class
        {
            var json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(json);
            }

            throw new PaystackApiException(response.StatusCode, json);
        }

        private List<MobileMoneyProvider> GetDefaultProviders(string countryCode)
        {
            return countryCode switch
            {
                "GH" => new List<MobileMoneyProvider>
                {
                    new() { Name = "MTN Mobile Money", Code = "MTN", CountryCode = "GH" },
                    new() { Name = "Vodafone Cash", Code = "VOD", CountryCode = "GH" },
                    new() { Name = "AirtelTigo Money", Code = "ATL", CountryCode = "GH" }
                },
                _ => new List<MobileMoneyProvider>()
            };
        }

        private string GetCurrencyCode(string countryCode)
        {
            return countryCode switch
            {
                "GH" => "GHS",
                _ => "GHS" // Default fallback
            };
        }
    }
}
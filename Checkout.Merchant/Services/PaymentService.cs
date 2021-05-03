using Checkout.Merchant.Shared;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Checkout.Merchant
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PaymentService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<PaymentDto> GetPayment(Guid id)
        {
            using var response = await _httpClient.GetAsync($"Payments/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var exMessage = await response.Content.ReadAsStringAsync();
                throw new Exception(exMessage);
            }

            using var stream = await response.Content.ReadAsStreamAsync();
            PaymentDto result = await JsonSerializer.DeserializeAsync<PaymentDto>(stream,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result;
        }

        public async Task<PaymentGatewayResponseDto> Process(PaymentRequestDto dto)
        {
            string merchantUniqueToken = _configuration[Constants.PaymentGatewayUniqueToken];

            RequestToPaymentProviderDto requestToPaymentGateway = new RequestToPaymentProviderDto
            {
                MerchantUniqueToken = new Guid(merchantUniqueToken),
                CardHolderName = dto.CardHolderName,
                CardNumber = dto.CardNumber,
                Cvc = dto.Cvc,
                ExpirationDate = dto.ExpirationDate,
                Amount = dto.Amount,
                TimeStamp = DateTime.Now
            };

            string json = JsonSerializer.Serialize(requestToPaymentGateway);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            using var response = await _httpClient.PostAsync("Payments", data);
            if (!response.IsSuccessStatusCode)
            {
                var exMessage = await response.Content.ReadAsStringAsync();
                throw new Exception(exMessage);
            }

            using var stream = await response.Content.ReadAsStreamAsync();
            PaymentGatewayResponseDto result = await JsonSerializer.DeserializeAsync<PaymentGatewayResponseDto>(stream,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result;
        }
    }
}

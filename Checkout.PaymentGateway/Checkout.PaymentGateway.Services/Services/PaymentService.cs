using AutoMapper;
using Checkout.PaymentGateway.Data;
using Checkout.PaymentGateway.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public PaymentService(HttpClient httpClient, AppDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<PaymentDto> GetPaymentDetails(Guid id)
        {
            Payment payment = await _context.Payments
                .Include(x => x.Card).ThenInclude(x => x.Client)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (payment is null)
            {
                throw new Exception($"Payment is not exist.");
            }

            PaymentDto result = _mapper.Map<PaymentDto>(payment);
            return result;
        }

        public async Task<ResponseDto> Process(PaymentRequestDto dto)
        {
            string paymentProviderUniqueToken = _configuration[Constants.PaymentProviderUniqueToken];
            ResponseDto result = new ResponseDto();

            //save request in Db
            Request request = _mapper.Map<Request>(dto);
            await _context.Requests.AddAsync(request);

            //validate merchant
            Merchant merchant = _context.Merchants.SingleOrDefault(x => x.MerchantUniqueToken == dto.MerchantUniqueToken);
            if (merchant is null)
            {
                result.Status = PaymentStatus.Failed;
                result.ResultCode = ResultCode.PaymentProviderError;
                return result;
            }

            //send request to bank
            BankPaymentRequestDto requestToBank = _mapper.Map<BankPaymentRequestDto>(dto);
            requestToBank.PaymentProviderUniqueToken = new Guid(paymentProviderUniqueToken);

            string json = JsonSerializer.Serialize(requestToBank);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            using var response = await _httpClient.PostAsync("Operations", data);
            if (!response.IsSuccessStatusCode)
            {
                var exMessage = await response.Content.ReadAsStringAsync();
                throw new Exception(exMessage);
            }

            using var stream = await response.Content.ReadAsStreamAsync();
            result = await JsonSerializer.DeserializeAsync<ResponseDto>(stream,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (result.ResultCode != ResultCode.Success)
            {
                return result;
            }

            //save changes in db
            Guid paymentId = await UpdatePaymentData(dto, result);
            request.Status = result.Status;
            await _context.SaveChangesAsync();

            result.OperationId = paymentId;
            return result;
        }

        private async Task<Guid> UpdatePaymentData(PaymentRequestDto dto, ResponseDto result)
        {
            Card card = _context.Cards.Include(x => x.Client)
                .SingleOrDefault(x => x.CardNumber == dto.CardNumber.Encrypt());

            Merchant merchant = _context.Merchants
                .SingleOrDefault(x => x.MerchantUniqueToken == dto.MerchantUniqueToken);

            if (card is null)
            {
                //create new card and new client
            }

            Payment payment = new Payment
            {
                MerchantId = merchant.Id,
                CardId = card.Id,
                BankOperationId = result.OperationId,
                Amount = dto.Amount,
                PaymentStatus = result.Status,
                TimeStamp = DateTime.Now
            };
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();

            return payment.Id;
        }
    }
}

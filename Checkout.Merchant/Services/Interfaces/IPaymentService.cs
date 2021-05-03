using Checkout.Merchant.Shared;
using System;
using System.Threading.Tasks;

namespace Checkout.Merchant
{
    public interface IPaymentService
    {
        Task<PaymentGatewayResponseDto> Process(PaymentRequestDto request);
        Task<PaymentDto> GetPayment(Guid id);
    }
}

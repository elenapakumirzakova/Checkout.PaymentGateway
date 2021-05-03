using Checkout.PaymentGateway.Shared;
using System;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Services
{
    public interface IPaymentService
    {
        Task<ResponseDto> Process(PaymentRequestDto request);
        Task<PaymentDto> GetPaymentDetails(Guid id);
    }
}

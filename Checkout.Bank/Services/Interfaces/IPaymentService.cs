using Checkout.Bank.Shared;
using System.Threading.Tasks;

namespace Checkout.Bank
{
    public interface IPaymentService
    {
        Task<ResponseDto> MakePayment(PaymentRequestDto requestPayment);
    }
}

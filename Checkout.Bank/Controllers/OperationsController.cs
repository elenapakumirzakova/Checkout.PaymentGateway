using Checkout.Bank.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Checkout.Bank.Controllers 
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    public class OperationsController: ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public OperationsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost()]
        public async Task<IActionResult> MakePayment(PaymentRequestDto requestPayment)
        {
            var paymentResult = await _paymentService.MakePayment(requestPayment);

            return Ok(paymentResult);
        }
    }
}

using Checkout.Merchant.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Checkout.Merchant.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost()]
        public async Task<IActionResult> MakePayment(PaymentRequestDto request)
        {
            var paymentResult = await _paymentService.Process(request);
            var response = new ResponseDto
            {
                OperationId = paymentResult.OperationId,
                Status = paymentResult.Status.ToString(),
                Result = paymentResult.ResultCode.ToString()
            };

            return Ok(response);
        }

        [HttpGet("id:guid")]
        public async Task<IActionResult> GetPayment(Guid id)
        {
            var payment = await _paymentService.GetPayment(id);

            if(payment is null)
            {
                return NotFound();
            }

            return Ok(payment);
        }
    }
}

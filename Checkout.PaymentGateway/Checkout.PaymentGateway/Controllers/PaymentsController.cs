using Checkout.PaymentGateway.Services;
using Checkout.PaymentGateway.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentProcessService;
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(IPaymentService paymentProcessService, ILogger<PaymentsController> logger)
        {
            _paymentProcessService = paymentProcessService;
            _logger = logger;
        }

        [HttpPost()]
        public async Task<IActionResult> MakePayment(PaymentRequestDto request)
        {
            _logger.LogInformation("Run endpoint {endpoint} {verb}", "/api/article-hit/most-viewed", "GET");
            var paymentResult = await _paymentProcessService.Process(request);

            return Ok(paymentResult);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPayment(Guid id)
        {
            _logger.LogInformation("Run endpoint {endpoint} {verb}", "/api/article-hit", "POST");
            var paymentResult = await _paymentProcessService.GetPaymentDetails(id);

            return Ok(paymentResult);
        }
    }
}

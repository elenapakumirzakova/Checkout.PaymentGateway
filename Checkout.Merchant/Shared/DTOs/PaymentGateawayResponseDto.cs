using Checkout.Merchant.Shared;
using System;

namespace Checkout.Merchant
{
    public class PaymentGatewayResponseDto
    {
        public Guid OperationId { get; set; }
        public PaymentStatus Status { get; set; }
        public ResultCode ResultCode { get; set; }
    }
}

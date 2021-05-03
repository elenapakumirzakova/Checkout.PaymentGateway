using System;
using System.Text.Json.Serialization;

namespace Checkout.PaymentGateway.Shared
{
    public class ResponseDto
    {
        public Guid OperationId { get; set; }
        public PaymentStatus Status { get; set; }
        public ResultCode ResultCode { get; set; }
    }
}

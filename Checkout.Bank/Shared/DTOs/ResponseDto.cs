using Checkout.Bank.Shared;
using System;

namespace Checkout.Bank
{
    public class ResponseDto
    {
        public Guid OperationId { get; set; }
        public PaymentStatus Status { get; set; }
        public ResultCode ResultCode { get; set; }
    }
}

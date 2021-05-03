using System;

namespace Checkout.Merchant.Shared
{
    public class ResponseDto
    {
        public Guid OperationId { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
    }
}

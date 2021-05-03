using Checkout.Merchant.Shared;
using System;

namespace Checkout.Merchant
{
    public class PaymentDto
    {
        public Guid TransactionId { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}

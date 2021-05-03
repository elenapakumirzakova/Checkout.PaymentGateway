using Checkout.PaymentGateway.Shared;
using System;

namespace Checkout.PaymentGateway.Data
{
    public class Request : EntityBase
    {
        public Guid MerchantUniqueToken { get; set; }
        public DateTime TimeStamp { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string Cvc { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Process;
    }
}

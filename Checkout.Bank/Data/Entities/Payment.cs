using System;

namespace Checkout.Bank.Data
{
    public class Payment : EntityBase
    {
        public DateTime TimeStamp { get; set; }
        public decimal Amount { get; set; }
        public Guid PaymentProvider { get; set; }
        public Guid CardId { get; set; }
    }
}

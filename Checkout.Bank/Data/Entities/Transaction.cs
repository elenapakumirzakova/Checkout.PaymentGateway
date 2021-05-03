using Checkout.Bank.Shared;
using System;

namespace Checkout.Bank.Data
{
    public class Transaction : EntityBase
    {
        public DateTime TimeStamp { get; set; }
        public decimal Amount { get; set; }
        public Guid PaymentProvider { get; set; }
        public Guid CardId { get; set; }
        public Guid PaymentProviderToken { get; set; }
        public PaymentStatus Status { get; set; }
        public ResultCode ResultCode { get; set; }
    }
}

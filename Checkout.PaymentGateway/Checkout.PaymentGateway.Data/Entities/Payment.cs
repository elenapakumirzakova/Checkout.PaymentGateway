using Checkout.PaymentGateway.Shared;
using System;
using System.Text.Json.Serialization;

namespace Checkout.PaymentGateway.Data
{
    public class Payment : EntityBase
    {
        public Guid MerchantId { get; set; }
        public Guid CardId { get; set; }
        public Guid BankOperationId { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime TimeStamp { get; set; }
        [JsonIgnore]
        public Card Card { get; set; }
        [JsonIgnore]
        public Merchant Merchant { get; set; }
    }
}

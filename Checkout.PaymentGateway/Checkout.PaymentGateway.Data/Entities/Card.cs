using System;
using System.Collections.Generic;

namespace Checkout.PaymentGateway.Data
{
    public class Card : EntityBase
    {
        public string CardNumber { get; set; }
        public string Cvc { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
        public List<Payment> Payments { get; set; }
    }
}

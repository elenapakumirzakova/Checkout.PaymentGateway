using System;
using System.Collections.Generic;

namespace Checkout.PaymentGateway.Data
{
    public class Merchant : EntityBase
    {
        public Guid MerchantUniqueToken { get; set; }
        public string Name { get; set; }
        public List<Payment> Payments { get; set; }
    }
}

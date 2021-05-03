using System;

namespace Checkout.Bank.Data
{
    public class PaymentProvider : EntityBase
    {
        public string Name { get; set; }
        public Guid PaymentProviderUniqueToken { get; internal set; }
    }
}

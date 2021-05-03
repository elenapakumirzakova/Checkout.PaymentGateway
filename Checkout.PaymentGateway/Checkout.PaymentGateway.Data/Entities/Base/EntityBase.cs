using System;

namespace Checkout.PaymentGateway.Data
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
    }
}

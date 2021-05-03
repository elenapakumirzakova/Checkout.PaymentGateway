using System.Collections.Generic;

namespace Checkout.PaymentGateway.Data
{
    public class Client : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Card> Cards { get; set; }
    }
}

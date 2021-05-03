using System;

namespace Checkout.PaymentGateway.Shared
{
    public class BankPaymentRequestDto
    {
        public Guid PaymentProviderUniqueToken { get; set; }

        public string CardHolderName { get; set; }

        public string CardNumber { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string Cvc { get; set; }

        public decimal Amount { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}

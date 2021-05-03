using System;
using System.ComponentModel.DataAnnotations;

namespace Checkout.Merchant.Shared
{
    public class RequestToPaymentProviderDto
    {
        [Required(ErrorMessage = "Merchant Unique Token is required.")]
        public Guid MerchantUniqueToken { get; set; }

        [Required(ErrorMessage = "Cardholder Name is required")]
        public string CardHolderName { get; set; }

        [Required(ErrorMessage = "Card number is required")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Cvc is required")]
        [StringLength(3)]
        public string Cvc { get; set; }

        [Required(ErrorMessage = "Expiration date is required")]
        public DateTime ExpirationDate { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Range(0.1, int.MaxValue, ErrorMessage = "Only positive amount allowed.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Timestamp is required")]
        public DateTime TimeStamp { get; set; }
    }
}
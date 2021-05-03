using System;
using System.ComponentModel.DataAnnotations;

namespace Checkout.Bank.Shared
{
    public class PaymentRequestDto
    {

        [Required(ErrorMessage = "Payment Provider Unique Token is required.")]
        public Guid PaymentProviderUniqueToken { get; set; }

        [Required(ErrorMessage = "Cardholder Name is required")]
        public string CardHolderName { get; set; }

        [Required(ErrorMessage = "Card number is required")]
        [RegularExpression(@"^[1-9][0-9]{3}-[1-3]{4}-[0-9]{4}-[0-9]{4}$", ErrorMessage = "Card number is not valid.")]
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

using System.ComponentModel.DataAnnotations;

namespace Checkout.PaymentGateway.Shared
{
    public enum ResultCode
    {
        [Display(Name = "Success.")]
        Success = 0,

        [Display(Name = "Payment Provider does not exist.")]
        PaymentProviderError = -900,

        [Display(Name = "Card does not exist.")]
        CardError = -901,

        [Display(Name = "Card balance error.")]
        CardBalanceError = -902
    }
}

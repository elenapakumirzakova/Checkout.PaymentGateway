using AutoMapper;
using Checkout.PaymentGateway.Shared;

namespace Checkout.PaymentGateway.Services.Profiles
{
    public class RequestProfile : Profile
    {
        public RequestProfile()
        {
            CreateMap<PaymentRequestDto, BankPaymentRequestDto>()
                    .ForMember(d => d.PaymentProviderUniqueToken, opts => opts.Ignore())
                    .ForMember(d => d.CardHolderName, opts => opts.MapFrom(s => s.CardHolderName))
                    .ForMember(d => d.CardNumber, opts => opts.MapFrom(s => s.CardNumber))
                    .ForMember(d => d.Cvc, opts => opts.MapFrom(s => s.Cvc))
                    .ForMember(d => d.ExpirationDate, opts => opts.MapFrom(s => s.ExpirationDate))
                    .ForMember(d => d.TimeStamp, opts => opts.MapFrom(s => s.TimeStamp))
                    .ForMember(d => d.Amount, opts => opts.MapFrom(s => s.Amount));
        }
    }
}

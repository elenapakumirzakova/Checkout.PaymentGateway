using AutoMapper;
using Checkout.PaymentGateway.Data;
using Checkout.PaymentGateway.Shared;

namespace Checkout.PaymentGateway.Services
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<PaymentRequestDto, Request>()
                .ForMember(d => d.MerchantUniqueToken, opts => opts.MapFrom(s => s.MerchantUniqueToken))
                .ForMember(d => d.CardHolderName, opts => opts.MapFrom(s => s.CardHolderName))
                .ForMember(d => d.CardNumber, opts => opts.MapFrom(s => s.CardNumber))
                .ForMember(d => d.Cvc, opts => opts.MapFrom(s => s.Cvc))
                .ForMember(d => d.ExpirationDate, opts => opts.MapFrom(s => s.ExpirationDate))
                .ForMember(d => d.TimeStamp, opts => opts.MapFrom(s => s.TimeStamp))
                .ForMember(d => d.Amount, opts => opts.MapFrom(s => s.Amount))
                .ForMember(d => d.Status, opts => opts.UseDestinationValue());

            CreateMap<Payment, PaymentDto>()
                .ForMember(d => d.CardHolderName, opts => opts.MapFrom(s =>
                    string.Format("{0} {1}", s.Card.Client.FirstName, s.Card.Client.LastName)))
                .ForMember(d => d.CardNumber, opts => opts.MapFrom(s => s.Card.CardNumber.Decrypt().Mask()))
                .ForMember(d => d.TimeStamp, opts => opts.MapFrom(s => s.TimeStamp))
                .ForMember(d => d.Amount, opts => opts.MapFrom(s => s.Amount))
                .ForMember(d => d.PaymentStatus, opts => opts.MapFrom(s => s.PaymentStatus))
                .ForMember(d => d.TransactionId, opts => opts.MapFrom(s => s.BankOperationId));
        }
    }
}

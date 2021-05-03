using Checkout.PaymentGateway.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Checkout.PaymentGateway.Data
{
    public class PaymentTypeConfiguration : EntityBaseTypeConfiguration<Payment>
    {
        public override void Configure(EntityTypeBuilder<Payment> builder)
        {
            base.Configure(builder);
            builder.ToTable(nameof(Payment), "dbo");

            builder.Property(x => x.PaymentStatus)
                .HasColumnType("tinyint")
                .HasConversion(
                    x => (byte)x,
                    x => (PaymentStatus)x)
                .HasDefaultValue(PaymentStatus.Process);

            builder.Property(x => x.Amount)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.TimeStamp)
                .HasColumnType("datetime2(0)")
                .IsRequired();

            builder.Property(t => t.BankOperationId)
                .HasColumnType("UniqueIdentifier")
                .IsRequired();

            builder.HasOne(x => x.Card)
                .WithMany(x => x.Payments)
                .HasPrincipalKey(x => x.Id)
                .HasForeignKey(x => x.CardId);

            builder.HasOne(x=>x.Merchant)
                .WithMany(x=>x.Payments)
                .HasPrincipalKey(x => x.Id)
                .HasForeignKey(x => x.MerchantId);

            builder.HasData(new List<Payment>
            {
                new Payment
                {
                    Id = new Guid("7c0d6f2b-3899-485d-ad6b-6c22d6ee6750"),
                    Amount = 22.60M,
                    PaymentStatus = PaymentStatus.Paid,
                    TimeStamp = new DateTime(2021, 1, 1),
                    MerchantId = new Guid("745be9cf-7c3f-4c51-b91b-2c8c037c67de"),
                    CardId = new Guid("42d6f985-096a-4697-8d93-6e28e5a822e9"),
                    BankOperationId = new Guid("fbc9d5ea-239a-48f3-a6bc-8adda6507e04")
                }
            });
        }
    }
}

using Checkout.PaymentGateway.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Checkout.PaymentGateway.Data
{
    public class CardTypeConfiguration : EntityBaseTypeConfiguration<Card>
    {
        public override void Configure(EntityTypeBuilder<Card> builder)
        {
            base.Configure(builder);
            builder.ToTable(nameof(Card), "dbo");

            builder.Property(x => x.CardNumber)
                .IsRequired();

            builder.Property(x => x.Cvc)
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(x => x.ExpirationDate)
                .HasColumnType("datetime2(0)");

            builder.HasOne(x => x.Client)
                .WithMany(x => x.Cards)
                .HasPrincipalKey(x => x.Id)
                .HasForeignKey(x => x.ClientId);

            builder.HasMany(x => x.Payments)
                .WithOne(x => x.Card)
                .HasPrincipalKey(x => x.Id)
                .HasForeignKey(x => x.CardId);

            builder.HasData(new List<Card>
            {
                new Card
                {
                    Id = new Guid("42d6f985-096a-4697-8d93-6e28e5a822e9"),
                    CardNumber = "1111-1111-1111-1111".Encrypt(),
                    Cvc = "111",
                    ExpirationDate = new DateTime(2022, 1, 1),
                    ClientId = new Guid("5c032979-8fb2-443a-8667-6b29cf02ecc2")
                }
            });
        }
    }
}

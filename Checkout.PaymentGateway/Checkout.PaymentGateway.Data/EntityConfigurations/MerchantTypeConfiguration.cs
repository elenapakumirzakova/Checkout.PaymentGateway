using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Checkout.PaymentGateway.Data
{
    public class MerchantTypeConfiguration : EntityBaseTypeConfiguration<Merchant>
    {
        public override void Configure(EntityTypeBuilder<Merchant> builder)
        {
            base.Configure(builder);
            builder.ToTable(nameof(Merchant), "dbo");

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasMany(x => x.Payments)
                .WithOne(x => x.Merchant)
                .HasPrincipalKey(x => x.Id)
                .HasForeignKey(x => x.MerchantId);

            builder.HasData(new List<Merchant>
            {
                new Merchant
                {
                    Id = new Guid("745be9cf-7c3f-4c51-b91b-2c8c037c67de"),
                    MerchantUniqueToken = new Guid("8f9e6b51-3eb3-4025-a96f-4343a591bf1f"),
                    Name = "Merchant"
                }
            });
        }
    }
}

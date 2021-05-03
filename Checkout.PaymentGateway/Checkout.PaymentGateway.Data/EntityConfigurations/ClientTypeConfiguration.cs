using Checkout.PaymentGateway.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Checkout.Bank.Data
{
    public class ClientTypeConfiguration : EntityBaseTypeConfiguration<Client>
    {
        public override void Configure(EntityTypeBuilder<Client> builder)
        {
            base.Configure(builder);
            builder.ToTable(nameof(Client), "dbo");

            builder.Property(x => x.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasMany(x => x.Cards)
                .WithOne(x => x.Client)
                .HasPrincipalKey(x => x.Id)
                .HasForeignKey(x => x.ClientId);

            builder.HasData(new List<Client>
            {
                new Client
                {
                    Id = new Guid("5c032979-8fb2-443a-8667-6b29cf02ecc2"),
                    FirstName = "John",
                    LastName ="Doe"
                }
            });
        }
    }
}

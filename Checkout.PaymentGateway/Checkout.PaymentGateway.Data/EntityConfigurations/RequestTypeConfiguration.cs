using Checkout.PaymentGateway.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Checkout.PaymentGateway.Data
{
    public class RequestTypeConfiguration : EntityBaseTypeConfiguration<Request>
    {
        public override void Configure(EntityTypeBuilder<Request> builder)
        {
            base.Configure(builder);
            builder.ToTable(nameof(Request), "dbo");

            builder.Property(t => t.MerchantUniqueToken)
                .HasColumnType("UniqueIdentifier")
                .IsRequired();

            builder.Property(x => x.TimeStamp)
                .HasColumnType("datetime2(0)")
                .IsRequired();

            builder.Property(x => x.Status)
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
        }
    }
}

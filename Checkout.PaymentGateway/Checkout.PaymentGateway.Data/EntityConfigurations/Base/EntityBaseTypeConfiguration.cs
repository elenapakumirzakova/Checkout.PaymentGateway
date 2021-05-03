using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Checkout.PaymentGateway.Data
{
    public abstract class EntityBaseTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : EntityBase
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Id)
                .HasColumnType("UniqueIdentifier")
                .ValueGeneratedOnAdd();

            builder.HasKey(x => x.Id)
                .IsClustered();
        }
    }
}

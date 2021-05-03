using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Reflection;

namespace Checkout.PaymentGateway.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName(
                    $"{entityType.GetTableName().Substring(0, 1).ToLowerInvariant()}{entityType.GetTableName()[1..]}");

                var tableName = entityType.GetTableName();

                if (string.IsNullOrEmpty(tableName))
                    throw new Exception("TableName Empty");

                foreach (var property in entityType.GetDeclaredProperties())
                    property.SetColumnName(
                        $"{property.GetColumnName(StoreObjectIdentifier.Table(tableName, "dbo")).Substring(0, 1).ToLowerInvariant()}" +
                        $"{property.GetColumnName(StoreObjectIdentifier.Table(tableName, "dbo"))[1..]}");
            }
        }
    }
}

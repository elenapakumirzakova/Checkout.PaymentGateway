using Checkout.PaymentGateway.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Checkout.PaymentGateway.Data
{
    public static class ServicesExtensions
    {
        public static void AddDataDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionsString = GetConnectionString(configuration);
            services.AddDbContext<AppDbContext>(
                options =>
            {
                options.UseSqlServer(connectionsString,
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(2, TimeSpan.FromSeconds(5), null);
                        sqlOptions.MigrationsAssembly("Checkout.PaymentGateway.Data");
                    });
            });
        }

        private static string GetConnectionString(IConfiguration configuration)
        {
            string host = configuration[Constants.SqlHost];
            string database = configuration[Constants.SqlDatabase];
            string password = configuration[Constants.SqlPassword];
            string userId = configuration[Constants.SqlUserId];
            string connectionStringTemplate = configuration[Constants.ConnectionStringTemplate];

            if (string.IsNullOrEmpty(host))
                throw new ArgumentNullException(nameof(host), $"Host is null or empty.");

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password), $"Password is null or empty.");

            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId), $"User Id is null or empty.");

            if (string.IsNullOrEmpty(connectionStringTemplate))
                throw new ArgumentNullException(nameof(connectionStringTemplate), $"Template is null or empty.");

            return string.Format(connectionStringTemplate, host, database, userId.Decrypt(), password.Decrypt());
        }
    }
}

using Checkout.Bank.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Checkout.Bank
{
    public static class ServicesExtensioncs
    {
        public static void AddServicesDependencies(this IServiceCollection services)
        {
            services.AddSingleton<MockDbContext>();
            services.AddScoped<IPaymentService, PaymentService>();
        }
    }
}

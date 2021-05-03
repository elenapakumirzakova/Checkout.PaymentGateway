using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkout.Bank.Data
{
    //Mock data
    public class MockDbContext
    {
        internal List<Client> Clients = new List<Client>
        {
            new Client
                {
                    Id = new Guid("5c032979-8fb2-443a-8667-6b29cf02ecc2"),
                    FirstName = "John",
                    LastName ="Doe"
                }
        };

        internal List<Card> Cards = new List<Card>
        {
             new Card
                {
                    Id = new Guid("42d6f985-096a-4697-8d93-6e28e5a822e9"),
                    CardNumber="1111-1111-1111-1111",
                    Cvc = "111",
                    Balance = 2000000,
                    ExpirationDate = new DateTime(2022, 1, 1),
                    ClientId = new Guid("5c032979-8fb2-443a-8667-6b29cf02ecc2"),
                    Client = new Client
                    {
                        Id = new Guid("5c032979-8fb2-443a-8667-6b29cf02ecc2"),
                        FirstName = "John",
                        LastName ="Doe"
                    }
                }
        };

        internal List<PaymentProvider> PaymentProviders = new List<PaymentProvider>
        {
            new PaymentProvider
            {
                Id = new Guid("a93b2660-ed31-4d61-a17f-59d735727af6"),
                PaymentProviderUniqueToken = new Guid("51c92618-c179-4018-ba72-81057de1c94a"),
                Name = "PaymentGateway"
            }
        };

        internal List<Transaction> Transactions = new List<Transaction>();

        internal async Task MockSaveChangesAsync()
        {
            //mock db save changes
        }
    }
}

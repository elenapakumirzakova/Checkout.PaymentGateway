using System;
using System.Collections.Generic;

namespace Checkout.Bank.Data
{
    public class Card : EntityBase
    {
        public decimal Balance { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Cvc { get; set; }
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}

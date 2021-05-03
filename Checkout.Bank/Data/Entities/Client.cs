using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Bank.Data
{
    public class Client: EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Card> Cards { get; set; }

    }
}

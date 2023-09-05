using System;

namespace DemoWebAPI.DbSets
{
    public class BankAccount
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string AccountNumber { get; set; }

        public string DateOpened { get; set; }

        public decimal AccountBalance { get; set; }

        //Navigation property
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}

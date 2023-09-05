using System;

namespace DemoWebAPI.DbSets
{
    public class Customer
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string DateOfRegistration { get; set; } = DateTime.UtcNow.ToShortDateString();

        public string DateUpdated { get; set; }

        public string FirstName { get; set; }

        public string EmailAddress { get; set; }

        public string LastName { get; set; }
        
        public string Address { get; set; }
        
        public string PhoneNumber { get; set; }

        public BankAccount BankAccount { get; set; }
    }
}

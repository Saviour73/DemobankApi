using System;

namespace DemoWebAPI.DbSets
{
    public class SettlementAccount
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string AccountNumber { get; set; }
        
        public string DateOpened { get; set; }

        public decimal AccountBalance { get; set; } = 0;
    }
}
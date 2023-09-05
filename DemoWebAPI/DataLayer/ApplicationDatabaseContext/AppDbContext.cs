using DemoWebAPI.DbSets;
using Microsoft.EntityFrameworkCore;

namespace DemoWebAPI.DataLayer.ApplicationDatabaseContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<SettlementAccount> SettlementAccounts { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }
    }
}

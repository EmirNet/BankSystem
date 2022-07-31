using BankSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Persistence.Context
{
    public class BankSystemContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UrlPath> UrlPaths { get; set; }

        public BankSystemContext(DbContextOptions<BankSystemContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

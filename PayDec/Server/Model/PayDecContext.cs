using Microsoft.EntityFrameworkCore;
using PayDec.Shared.Model;


namespace PayDec.Server.Model
{
    public class PayDecContext : DbContext
    {

        public PayDecContext(DbContextOptions<PayDecContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        public DbSet<Admin> Admin { get; set; } 
        public DbSet<Item> Item { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
    }
}

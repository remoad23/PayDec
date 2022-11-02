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

        public ICollection<Admin> Admin { get; set; } = new List<Admin>();
        public ICollection<Item> Item { get; set; } = new List<Item>();
        public ICollection<Purchase> Purchase { get; set; } = new List<Purchase>();
    }
}

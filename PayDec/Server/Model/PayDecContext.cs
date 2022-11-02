using Microsoft.EntityFrameworkCore;
using PayDec.Shared.Model;


namespace PayDec.Server.Model
{
    public class PayDecContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        public ICollection<Admin> Admin { get; set; }
        public ICollection<Item> Item { get; set; }
        public ICollection<Purchase> Purchase { get; set; }
    }
}

using BlazingPizza;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.DBContext
{
    public class PizzaStoreContext: DbContext
    {
        public PizzaStoreContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        public DbSet<PizzaSpecial> Specials { get; set; }
    }
}

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
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.\SQLExpress;Database=SchoolDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        public DbSet<PizzaSpecial> Specials { get; set; }
    }
}

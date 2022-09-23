using BlazingPizza;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.DBContext
{
    public class PizzaStoreContext: DbContext
    {
        public PizzaStoreContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<PizzaSpecial> Specials { get; set; }
    }
}

using BlazingPizza;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.DBContext
{
    public class PizzaStoreContext: DbContext
    {
        public IConfiguration Configuration { get; }

        public PizzaStoreContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
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

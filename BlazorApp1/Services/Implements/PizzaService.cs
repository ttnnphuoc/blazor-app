using BlazingPizza;
using BlazorApp1.DBContext;
using BlazorApp1.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Services.Implements
{
    public class PizzaService : IPizzaService
    {
        private readonly PizzaStoreContext _db;

        public PizzaService(PizzaStoreContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<PizzaSpecial>> GetPizzasAsync()
        {
            return (await _db.Specials.ToListAsync()).OrderByDescending(x=>x.BasePrice);
        }
    }
}

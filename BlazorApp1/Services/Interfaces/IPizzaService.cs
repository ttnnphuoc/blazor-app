using BlazingPizza;

namespace BlazorApp1.Services.Interfaces
{
    public interface IPizzaService
    {
        Task<IEnumerable<PizzaSpecial>> GetPizzasAsync();
    }
}

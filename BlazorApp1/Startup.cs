using BlazorApp1.DBContext;
using BlazorApp1.Services.Implements;
using BlazorApp1.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BlazorApp1
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostingEnvironment { get; }

        public Startup(WebApplicationBuilder builder, IWebHostEnvironment env)
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddTransient<IPizzaService, PizzaService>();
            services.AddHttpClient();
            services.AddSqlite<PizzaStoreContext>("Data Source=pizza.db");
        }
    }
}

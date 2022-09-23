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
        private void ConfigureContext(IServiceCollection services)
        {
            var descriptorCore = new ServiceDescriptor(typeof(DbContextOptions<PizzaStoreContext>), PizzaStoreContextOptionsFactory, ServiceLifetime.Scoped);
            if (descriptorCore != null) services.Replace(descriptorCore);

            services.BuildServiceProvider();
        }

        private DbContextOptions<PizzaStoreContext> PizzaStoreContextOptionsFactory(IServiceProvider provider)
        {
            var httpContext = provider.GetService<HttpContext>();
            var optionsBuilder = new DbContextOptionsBuilder<PizzaStoreContext>();
            var connectionStringSettingInfo = Configuration.GetConnectionStringSettings(httpContext, DbContextEnum.CORE);
            if (connectionStringSettingInfo == null)
            {
                return null;
            }

            string connectionString = connectionStringSettingInfo.ConnectionStringSettings?.Connection;
            string containerCode = connectionStringSettingInfo.ContainerCode;


            if (!string.IsNullOrEmpty(connectionString) && !string.IsNullOrEmpty(containerCode))
            {
                optionsBuilder.UseSqlServerMSI(connectionString, Configuration);
                if (Configuration.EnableLog())
                {
                    optionsBuilder.UseLoggerFactory(_loggerFactory);
                }
            }
            return optionsBuilder.Options;
        }

    }
}

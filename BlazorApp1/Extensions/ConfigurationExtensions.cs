using BlazorApp1.Model.Common;

namespace BlazorApp1.Extensions
{
    public static class ConfigurationExtensions
    {
        public static ConnectionStringSettings GetConnectionStringSettings(this IConfiguration configuration, HttpContext context)
        {
            return new ConnectionStringSettings();
        }
    }
}

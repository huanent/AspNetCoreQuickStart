using ApplicationCore.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationCore
{
    public static class RegisterApplicationCoreExtensions
    {
        public static IServiceCollection AddApplicationCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDemoService, DemoService>();

            return services;
        }
    }
}

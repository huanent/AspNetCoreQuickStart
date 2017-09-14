using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Repositories;
using Infrastructure.RepositoriesImplment;

namespace Infrastructure
{
    public static class RegisterInfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDemoRepository, DemoRepository>();

            services.AddDbContextPool<AppDbContext>(b => b.UseSqlServer(configuration.GetConnectionString("")));

            return services;
        }
    }
}

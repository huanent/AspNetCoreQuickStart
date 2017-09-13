using ApplicationCore.Repositories;
using Infrastructure.Data;
using Infrastructure.RepositoriesImplment;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class RegisterInfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<AppDbContext>(b => b.UseSqlServer(configuration.GetConnectionString("")));

            services.AddScoped<IDemoRepository, DemoRepository>();

            return services;
        }
    }
}

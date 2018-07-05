using Microsoft.EntityFrameworkCore;
using MyCompany.MyProject.Infrastructure.Data;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class QueryDbContextExtensions
    {
        public static IServiceCollection AddQueryDbContext<T, TBase>(this IServiceCollection services, Action<DbContextOptionsBuilder> action) where TBase : DbContext where T : TBase
        {
            services.AddSingleton(provider =>
            {
                return new QueryDbContextOptions<TBase>(action);
            });

            services.AddDbContext<T>();

            return services;
        }
    }
}

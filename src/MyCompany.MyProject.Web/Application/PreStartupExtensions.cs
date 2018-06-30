using MyCompany.MyProject.ApplicationCore.SharedKernel;
using MyCompany.MyProject.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyCompany.MyProject.Web;

namespace Microsoft.AspNetCore.Builder
{
    public static class PreStartupExtensions
    {
        public static IApplicationBuilder UsePreStart(this IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var env = app.ApplicationServices.GetService<IHostingEnvironment>();

                //auto Migrate
                if (env.IsProduction())
                {
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    db.Database.Migrate();
                }

                //log currentEnv
                var logger = scope.ServiceProvider.GetRequiredService<IAppLogger<Startup>>();
                logger.Warn($"当前运行环境：{env.EnvironmentName}");
            }

            return app;
        }
    }
}

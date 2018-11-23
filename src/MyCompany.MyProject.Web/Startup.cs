using System;
using System.IO;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyCompany.MyProject.Application;
using MyCompany.MyProject.Persistence;
using MyCompany.MyProject.Persistence.Internal;
using MyCompany.MyProject.Web.Internal;

namespace MyCompany.MyProject.Web
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        private readonly Settings _settings;

        public Startup(IOptions<Settings> options, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            _env = env;
            _settings = options.Value;
        }

        public void Configure(IApplicationBuilder app)
        {
            UseCors(app);
            AutoMigrate(app);
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseMvc();
            app.UseAppSwagger();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(QueryPageCommand<>));
            services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
            services.AddSingleton<ISequentialGuidGenerator, SequentialGuidGenerator>();
            services.AddAppSwagger();
            services.AddAppAuthentication();
            services.AddAppAuthorization();
            services.AddDbContextPool<AppDbContext>(b => b.UseSqlServer(_settings.ConnectionStrings.Default));
            services.AddLoggingFileUI(o => o.Path = _settings.LogPath);
            services.AddScoped<ICurrentIdentity, CurrentIdentity>();
            AddMvc(services);
        }

        private static void AddMvc(IServiceCollection services)
        {
            services.AddMvc(o =>
            {
                o.Filters.Add<GlobalExceptionHandleFilter>();
                o.Filters.Add<HandlerIdentityFilter>();
                o.ModelMetadataDetailsProviders.Add(new RequiredBindingMetadataProvider());
            })
                        .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        private void AutoMigrate(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                if (!_env.IsProduction())
                {
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    db.Database.Migrate();
                }
            }
        }

        private void UseCors(IApplicationBuilder app)
        {
            app.UseCors(b =>
            {
                b.AllowAnyHeader().AllowAnyMethod();
                if (_env.IsProduction()) b.WithOrigins(_settings.CorsOrigins);
                else b.AllowAnyOrigin();
            });
        }
    }
}

using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyCompany.MyProject.Application;
using MyCompany.MyProject.Core;
using MyCompany.MyProject.Infrastructure;
using MyCompany.MyProject.Web.Internal;

namespace MyCompany.MyProject.Web
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        private readonly AppSettings _settings;

        public Startup(IOptions<AppSettings> options, IHostingEnvironment env, ILoggerFactory loggerFactory)
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
            app.UseSwagger();
            app.UseSwaggerUi3();
            app.UseMvc();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppAuthentication(_env);
            services.AddAppAuthorization();
            services.AddDbContext<AppDbContext>(o => o.UseSqlServer(_settings.ConnectionStrings.Default));
            services.AddLoggingFileUI(o => o.Path = Path.Combine(AppContext.BaseDirectory, Constants.DataPath, "logs"));
            services.AddSwaggerDocument(s => s.DocumentProcessors.Add(new SwaggerDocumentProcessor()));
            services.AddHttpContextAccessor();
            services.AddInject(new[] {
                Assembly.GetExecutingAssembly(),
                typeof(CoreServicesRegister).Assembly,
                typeof(ApplicationServicesRegister).Assembly,
                typeof(InfrastructureServicesRegister).Assembly,
            });
            AddMvc(services);
        }

        private void AddMvc(IServiceCollection services)
        {
            services.AddMvc(o =>
            {
                o.Filters.Add<GlobalExceptionHandleFilter>();
                o.ModelMetadataDetailsProviders.Add(new RequiredBindingMetadataProvider());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        private void AutoMigrate(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                if (_env.IsProduction())
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
                b.AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                if (_env.IsProduction()) b.WithOrigins(_settings.CorsOrigins);
                else b.AllowAnyOrigin();
            });
        }
    }
}

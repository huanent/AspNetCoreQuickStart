using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
            //LoggerMessage
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
            AddAuthentication(services);
            services.AddDbContext<AppDbContext>(o => o.UseSqlServer(_settings.ConnectionStrings.Default));
            services.AddLoggingFileUI(o => o.Path = Path.Combine(AppContext.BaseDirectory, Constants.DataPath, "logs"));
            services.AddSwaggerDocument(s => s.DocumentProcessors.Add(new SwaggerDocumentProcessor()));
            services.AddHttpContextAccessor();
            services.AddInject();
            AddMvc(services);
        }

        public void AddAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = Constants.AppName;
                options.Cookie.SameSite = _env.IsProduction() ? SameSiteMode.Lax : SameSiteMode.None;
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.FromResult(string.Empty);
                };
                options.Events.OnRedirectToLogout = context =>
                {
                    context.Response.StatusCode = 200;
                    return Task.FromResult(string.Empty);
                };
            });
        }

        private void AddMvc(IServiceCollection services)
        {
            services.AddMvc(o =>
            {
                o.Filters.Add<GlobalExceptionHandleFilter>();
                o.ModelMetadataDetailsProviders.Add(new RequiredBindingMetadataProvider());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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

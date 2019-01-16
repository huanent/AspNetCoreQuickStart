using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MyCompany.MyProject.Web.Internal;

namespace MyCompany.MyProject.Web
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        private readonly AppSettings _settings;

        public Startup(IOptions<AppSettings> options, IHostingEnvironment env)
        {
            _env = env;
            _settings = options.Value;
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(UseCors);
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseSwagger();
            app.UseSwaggerUi3();
            app.UseMvc();
            using (var scope = app.ApplicationServices.CreateScope())
            {
                if (_env.IsProduction())
                {
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    db.Database.Migrate();
                }
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(AddCookie);
            services.AddDbContext<AppDbContext>(o => o.UseSqlServer(_settings.ConnectionStrings.Default));
            services.AddSwaggerDocument(s => s.DocumentProcessors.Add(new SwaggerDocumentProcessor()));
            services.AddHttpContextAccessor();
            services.AddMvc(AddMvc).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddInject();
        }

        public void AddCookie(CookieAuthenticationOptions options)
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
        }

        private void AddMvc(MvcOptions options)
        {
            options.Filters.Add<GlobalExceptionHandleFilter>();
            options.ModelMetadataDetailsProviders.Add(new RequiredBindingMetadataProvider());
        }

        private void UseCors(CorsPolicyBuilder builder)
        {
            builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            if (_env.IsProduction()) //生产环境指定跨域域名
            {
                builder.WithOrigins(_settings.CorsOrigins);
            }
            else //其他环境允许所有跨所有域名
            {
                builder.AllowAnyOrigin();
            }
        }
    }
}

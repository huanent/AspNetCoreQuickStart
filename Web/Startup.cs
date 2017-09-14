using ApplicationCore;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Threading.Tasks;
using Web.Filters;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(o =>
            {
                o.Filters.Add<GlobalActionFilter>();
                o.Filters.Add<GlobalExceptionFilter>();
            });

            services.AddInfrastructure(Configuration);
            services.AddApplicationCore();

            services.AddAuthentication(o => o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o =>
                {
                    o.ExpireTimeSpan = new TimeSpan(0, 5, 0);
                    o.Events.OnRedirectToLogin = (context) =>
                    {
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    };
                });

            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("api", new Info());
                o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "docs.xml"));
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(o => o.SwaggerEndpoint("/swagger/api/swagger.json", "api"));

            //var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            //using (var scope = scopeFactory.CreateScope())
            //{
            //    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            //    db.Database.Migrate();
            //}
        }
    }
}

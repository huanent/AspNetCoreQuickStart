using ApplicationCore.IRepositories;
using ApplicationCore.SharedKernel;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Data.Common;
using System.IO;
using System.Threading.Tasks;
using Web.Filters;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _settings = configuration.GetSection("App").Get<Settings>();
            _env = env;
        }

        readonly IHostingEnvironment _env;
        readonly Settings _settings;
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            AddFilters(services);
            AddDbContext(services);
            AddAuth(services);
            AddSwagger(services);
            AddAppServices(services);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseMvc();
            UseSwagger(app);
            UseCors(app);
            Init(app);
        }

        #region 注册服务
        private void AddAppServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IAppLogger<>), typeof(AppLogger<>));
            services.AddSingleton<ISequenceGuidGenerator, SequenceGuidGenerator>();
            services.AddScoped<IDemoRepository, DemoRepository>();
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("api", new Info());
                o.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "web.xml"));
            });
        }

        private static void AddAuth(IServiceCollection services)
        {
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
        }

        private void AddDbContext(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("Default");
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(connectionString));
        }

        private static void AddFilters(IServiceCollection services)
        {
            services.AddMvc(o =>
            {
                o.Filters.Add<GlobalActionFilter>();
                o.Filters.Add<GlobalExceptionFilter>();
            });
        }
        #endregion

        #region 配置管道
        private void Init(IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();

                var logger = scope.ServiceProvider.GetRequiredService<IAppLogger<Startup>>();
                logger.Warn($"当前运行环境：{_env.EnvironmentName}");
            }
        }

        private void UseCors(IApplicationBuilder app)
        {
            if (!_env.IsProduction())
            {
                app.UseCors(o =>
                {
                    o.AllowAnyHeader();
                    o.AllowAnyMethod();
                    o.AllowAnyOrigin();
                });
            }
        }

        private void UseSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(o => o.SwaggerEndpoint("/swagger/api/swagger.json", _settings.AppName));
        }
        #endregion
    }
}

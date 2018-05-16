using ApplicationCore.SharedKernel;
using Infrastructure.Data;
using Infrastructure.Implements;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;
using Web.Application;
using Web.Auth;

namespace Web
{
    public class Startup
    {
        readonly IHostingEnvironment _env;
        readonly AppSettings _settings;
        readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _configuration = configuration;
            _settings = configuration.Get<AppSettings>();
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureOptions(services);
            AddSystemService(services);
            AddAppServices(services);
        }

        public void Configure(IApplicationBuilder app)
        {
            if (!_env.IsProduction()) app.UseCors(b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseAuthentication();
            app.UseFileServer();
            app.UseMvc();
            app.UseAppSwagger();
            PreStart(app);
        }

        private void PreStart(IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                //auto Migrate
                if (_env.IsProduction())
                {
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    db.Database.Migrate();
                }

                //log currentEnv
                var logger = scope.ServiceProvider.GetRequiredService<IAppLogger<Startup>>();
                logger.Warn($"当前运行环境：{_env.EnvironmentName}");
            }
        }

        private void ConfigureOptions(IServiceCollection services)
        {
            services.Configure<AppSettings>(_configuration);
            services.Configure<ConnectionStrings>(_configuration.GetSection("ConnectionStrings"));
            services.Configure<Jwt>(_configuration.GetSection("Jwt"));
        }

        private void AddSystemService(IServiceCollection services)
        {
            services.AddAppSwagger();
            services.AddAppAuthentication(_settings.Jwt.Key);
            services.AddAppAuthorization();
            services.AddMemoryCache();
            services.AddDbContext<AppDbContext>();
            services.AddDbContext<AppQueryDbContext>();

            services.AddMvc(o =>
            {
                o.Filters.Add<ModelStateValidFilter>();
                o.Filters.Add<GlobalExceptionHandleFilter>();
                o.Filters.Add<IdentityHandleFilter>();
                o.Filters.Add<JwtRefreshFilter>();
            });
        }

        private void AddAppServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IAppLogger<>), typeof(AppLogger<>));
            services.AddSingleton<ISequenceGuidGenerator, SequenceGuidGenerator>();
            services.AddSingleton<ISystemDateTime, SystemDateTime>();
            services.AddSingleton<ICache, MemoryCache>();
            services.AddScoped<ICurrentIdentity, CurrentIdentity>();
            services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
            services.AddSingleton<Func<EventId>>(() => new EventId(_settings.EventId));
            AutoInjectService(services, "Infrastructure", "Infrastructure.Repositories");
            AutoInjectService(services, "ApplicationCore", "ApplicationCore.Services");
        }

        private void AutoInjectService(IServiceCollection services, string assemblyName, string namespaceStartsWith)
        {
            var types = Assembly.Load(assemblyName).GetTypes().Where(w => !w.IsNested && !w.IsInterface && w.FullName.StartsWith(namespaceStartsWith));
            foreach (TypeInfo type in types)
            {
                var interfaceType = type.ImplementedInterfaces.First();
                services.AddScoped(interfaceType, type);
            }
        }

    }
}
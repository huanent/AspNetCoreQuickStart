using ApplicationCore.SharedKernel;
using FluentValidation.AspNetCore;
using Infrastructure.Data;
using Infrastructure.Implements;
using Infrastructure.ModelValidators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
            app.UseStaticFiles();
            app.UseFileServer();
            app.UseMvc();
            app.UseAppSwagger();
            app.UsePreStart();
        }

        private void ConfigureOptions(IServiceCollection services)
        {
            services.Configure<AppSettings>(_configuration);
            services.Configure<ConnectionStrings>(_configuration.GetSection("ConnectionStrings"));
            services.Configure<Cookie>(_configuration.GetSection("Cookie"));
        }

        private void AddSystemService(IServiceCollection services)
        {
            services.AddAppSwagger();
            services.AddAppAuthentication(_settings.Cookie);
            services.AddAppAuthorization();

            services.AddMemoryCache();
            services.AddLoggingFileUI();
            services.AddDbContext<AppDbContext>();
            services.AddDbContext<AppQueryDbContext>();

            services.Configure<ApiBehaviorOptions>(o =>
            {
                o.SuppressInferBindingSourcesForParameters = true;
            });

            services.AddMvc(o =>
            {
                o.Filters.Add<GlobalExceptionHandleFilter>();
                o.Filters.Add<IdentityHandleFilter>();
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<RegisterValidators>();
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
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
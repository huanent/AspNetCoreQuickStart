using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyCompany.MyProject.ApplicationCore.SharedKernel;
using MyCompany.MyProject.Infrastructure.Data;
using MyCompany.MyProject.Infrastructure.Implements;
using MyCompany.MyProject.Infrastructure.ModelValidators;
using MyCompany.MyProject.Web.Application;
using System;

namespace MyCompany.MyProject.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _env;
        private readonly AppSettings _settings;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _configuration = configuration;
            _settings = configuration.Get<AppSettings>();
            _env = env;
        }

        public void Configure(IApplicationBuilder app)
        {
            if (!_env.IsProduction())
            {
                app.UseCors(b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            }

            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseMvc();
            app.UseAppSwagger();
            app.UsePreStart();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureOptions(services);
            AddSystemService(services);
            AddAppServices(services);
        }

        private void AddAppServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IAppLogger<>), typeof(AppLogger<>));
            services.AddSingleton<ISequenceGuidGenerator, SequenceGuidGenerator>();
            services.AddSingleton<ISystemDateTime, SystemDateTime>();
            services.AddScoped<ICurrentIdentity, CurrentIdentity>();
            services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
            services.AddSingleton<Func<EventId>>(() => new EventId(_settings.EventId));
            services.AutoInject("MyCompany.MyProject.Infrastructure", "MyCompany.MyProject.Infrastructure.Repositories");
            services.AutoInject("MyCompany.MyProject.ApplicationCore", "MyCompany.MyProject.ApplicationCore.Services");
        }

        private void AddSystemService(IServiceCollection services)
        {
            services.AddAppSwagger();
            services.AddAppAuthentication(_settings.Cookie);
            services.AddAppAuthorization();

            services.AddMemoryCache();
            services.AddLoggingFileUI();

            services.AddDbContext<AppDbContext>(builder => builder.UseSqlServer(_settings.ConnectionStrings.Default));

            services.AddQueryDbContext<AppQueryDbContext, AppDbContext>(builder =>
            {
                builder.UseSqlServer(_settings.ConnectionStrings.DefaultQuery);
            });

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

        private void ConfigureOptions(IServiceCollection services)
        {
            services.Configure<AppSettings>(_configuration);
            services.Configure<ConnectionStrings>(_configuration.GetSection("ConnectionStrings"));
            services.Configure<Cookie>(_configuration.GetSection("Cookie"));
        }
    }
}
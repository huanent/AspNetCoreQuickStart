using ApplicationCore.Entities;
using ApplicationCore.IRepositories;
using ApplicationCore.SharedKernel;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using Web.Filters;
using Web.Utils;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _settings = configuration.Get<AppSettings>();
            _env = env;
        }

        private readonly IHostingEnvironment _env;
        private readonly AppSettings _settings;
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureOptions(services);
            AddSystemService(services);
            AddAppServices(services);
        }

        private void AddSystemService(IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
                o.OperationFilter<SwaggerFilter>();
                o.SwaggerDoc("api", new Info());
                o.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Web.xml"));
                o.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationCore.xml"));
                o.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Infrastructure.xml"));
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false, //不验证发行方
                    ValidateAudience = false, //不验证受众方
                                              //ValidateLifetime = false, //不验证过期时间
                    ClockSkew = TimeSpan.Zero, //时钟偏差设为0
                    IssuerSigningKey = JwtHandler.GetSecurityKey(_settings.JwtKey), //密钥
                };
            });

            services.AddMvc(o =>
            {
                o.Filters.Add<GlobalActionFilter>();
                o.Filters.Add<GlobalExceptionFilter>();
            });

            services.AddMemoryCache();
            services.AddDbContextPool<AppDbContext>(o => o.UseSqlServer(_settings.ConnectionStrings.Default));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(o => o.SwaggerEndpoint("/swagger/api/swagger.json", _settings.AppName));

            if (!_env.IsProduction())
            {
                app.UseCors(o =>
                {
                    o.AllowAnyHeader();
                    o.AllowAnyMethod();
                    o.AllowAnyOrigin();
                });
            }

            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                if (_env.IsProduction())
                {
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    db.Database.Migrate();
                }

                var logger = scope.ServiceProvider.GetRequiredService<IAppLogger<Startup>>();
                logger.Warn($"当前运行环境：{_env.EnvironmentName}");
            }
        }

        #region 注册服务

        private void ConfigureOptions(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration);
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
        }

        private void AddAppServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IAppLogger<>), typeof(AppLogger<>));
            services.AddSingleton<ISequenceGuidGenerator, SequenceGuidGenerator>();
            services.AddSingleton<ISystemDateTime, SystemDateTime>();
            services.AddSingleton<ICache, MemoryCache>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            AutoInjectService(services, "Infrastructure", "Infrastructure.Repositories");
        }

        /// <summary>
        /// 自动注入指定程序集的指定命名空间下的服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyName"></param>
        /// <param name="namespaceStartsWith"></param>
        private void AutoInjectService(IServiceCollection services, string assemblyName, string namespaceStartsWith)
        {
            var types = Assembly.Load(assemblyName).GetTypes().Where(w => !w.IsNested && !w.IsInterface && w.FullName.StartsWith(namespaceStartsWith));
            foreach (TypeInfo type in types)
            {
                var interfaceType = type.ImplementedInterfaces.First();
                services.AddScoped(interfaceType, type);
            }
        }

        #endregion 注册服务
    }
}
using ApplicationCore.ISharedKernel;
using Infrastructure.Data;
using Infrastructure.SharedKernel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
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
        private readonly string _AppCors = string.Empty;
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            ConfigureOptions(services);
            AddSystemService(services);
            AddAppServices(services);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseMvc();
            if (!_env.IsProduction()) app.UseCors(nameof(_AppCors));
            app.UseSwagger();
            app.UseSwaggerUI(o => o.SwaggerEndpoint("/swagger/api/swagger.json", _settings.AppName));

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
            services.Configure<Jwt>(Configuration.GetSection("Jwt"));
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

            services.AddAuthentication(options => options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false, //不验证发行方
                    ValidateAudience = false, //不验证受众方
                                              //ValidateLifetime = false, //不验证过期时间
                    ClockSkew = TimeSpan.Zero, //时钟偏差设为0
                    IssuerSigningKey = JwtHandler.GetSecurityKey(_settings.Jwt.Key), //密钥
                };
            });

            services.AddCors(options =>
                options.AddPolicy(nameof(_AppCors), builder =>
                     builder.AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin()
                )
            );

            services.AddMvc(o =>
            {
                o.Filters.Add<GlobalActionFilter>();
                o.Filters.Add<GlobalExceptionFilter>();
                o.Filters.Add<GlobalResourceFilter>();
                o.Filters.Add<GlobalResultFilter>();
                o.Filters.Add(new CorsAuthorizationFilterFactory(nameof(_AppCors)));
            });

            services.AddMemoryCache();
            services.AddDbContext<AppDbContext>(o => o.UseSqlServer(_settings.ConnectionStrings.Default));
        }

        private void AddAppServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IAppLogger<>), typeof(AppLogger<>));
            services.AddSingleton<ISequenceGuidGenerator, SequenceGuidGenerator>();
            services.AddSingleton<ISystemDateTime, SystemDateTime>();
            services.AddSingleton<ICache, MemoryCache>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICurrentIdentity, CurrentIdentity>();
            services.AddSingleton<Func<EventId>>(() => new EventId(_settings.EventId));
            AutoInjectService(services, "Infrastructure", "Infrastructure.Repositories");
            AutoInjectService(services, "ApplicationCore", "ApplicationCore.Services");
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
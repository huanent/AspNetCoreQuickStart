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
using System.IO;
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
            ConfigureOptions(services);
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
        private void ConfigureOptions(IServiceCollection services)
        {
            services.Configure<Settings>(Configuration.GetSection("App"));
        }
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
                o.OperationFilter<SwaggerFilter>();
                o.SwaggerDoc("api", new Info());
                o.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "web.xml"));
            });
        }

        private void AddAuth(IServiceCollection services)
        {
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

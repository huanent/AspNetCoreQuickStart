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
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Threading.Tasks;
using Web.Filters;
using AutoMapper;
using System.Reflection;
using ApplicationCore.IServices;
using ApplicationCore.Services;

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
            AddFilters(services);
            AddDbContext(services);
            AddAuth(services);
            AddSwagger(services);
            AddAutoMapper(services);
            AddAppSettings(services);
            AddAppServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseAuthentication();
            app.UseMvc();
            UseSwagger(app);
            UseCors(app, env);
            Init(app);
        }

        #region 注册服务

        private void AddAppSettings(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration);
        }
        private void AddAppServices(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICoding, Coding>();
            services.AddScoped<IDemoRepository, DemoRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPermissionService, PermissionService>();
        }

        private void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("api", new Info());
                o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "docs.xml"));
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
            services.AddDbContextPool<AppDbContext>(o =>
            {
                o.UseSqlServer(Configuration.GetConnectionString("Default"));
            });
        }

        private static void AddFilters(IServiceCollection services)
        {
            services.AddMvc(o =>
            {
                o.Filters.Add<GlobalActionFilter>();
                o.Filters.Add<GlobalExceptionFilter>();
                o.Filters.Add<GlobalAuthorizationFilter>();
            });
        }
        #endregion

        #region 配置管道
        private static void Init(IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();

                //var logger = scope.ServiceProvider.GetRequiredService<IAppLogger<Startup>>();
                //logger.Warn($"当前运行环境：{env.EnvironmentName}");
            }
        }

        private static void UseCors(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(o =>
            {
                o.AllowAnyHeader();
                o.AllowAnyMethod();
                if (env.IsProduction())
                {
                    o.WithOrigins("http://xxx.xxx.com/");
                    throw new Exception("替换上方http://xxx.xxx.com/为你的前端项目部署地址,并删除此异常");
                }
                else
                {
                    o.AllowAnyOrigin();
                }
            });
        }

        private static void UseSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(o => o.SwaggerEndpoint("/swagger/api/swagger.json", "api"));
        }
        #endregion
    }
}

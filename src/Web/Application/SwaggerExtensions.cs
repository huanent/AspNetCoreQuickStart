using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;

namespace Web.Application
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddAppSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
                var security = new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } }, };
                o.AddSecurityRequirement(security);

                o.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "权限认证(数据将在请求头中进行传输) 参数结构: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                o.SwaggerDoc("api", new Info());
                o.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Web.xml"));
                o.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationCore.xml"));
                o.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Infrastructure.xml"));
            });

            return services;
        }

        public static IApplicationBuilder UseAppSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(o => o.SwaggerEndpoint("/swagger/api/swagger.json", "api"));

            return app;
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;

namespace MyCompany.MyProject.Web.Application
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddAppSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("api", new Info());
                o.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyCompany.MyProject.Web.xml"));
                o.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyCompany.MyProject.ApplicationCore.xml"));
                o.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyCompany.MyProject.Infrastructure.xml"));
                o.AddFluentValidationRules();
                o.OperationFilter<SwaggerFileUploadFilter>();
                o.IgnoreObsoleteActions();
            });

            return services;
        }

        public static IApplicationBuilder UseAppSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/api/swagger.json", "api");
                o.DocExpansion(DocExpansion.None);
            });
            return app;
        }
    }
}

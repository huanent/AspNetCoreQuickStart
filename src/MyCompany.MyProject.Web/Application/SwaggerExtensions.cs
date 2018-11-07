using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace MyCompany.MyProject.Web.Application
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddAppSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("MyCompany.MyProject", new Info());
                o.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyCompany.MyProject.Web.xml"));
                o.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyCompany.MyProject.Dtos.xml"));
                o.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyCompany.MyProject.Data.xml"));
                o.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyCompany.MyProject.Handlers.xml"));
                o.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyCompany.MyProject.Commands.xml"));
                o.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyCompany.MyProject.Common.xml"));
                o.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyCompany.MyProject.Entities.xml"));
                o.OperationFilter<SwaggerFileUploadFilter>();
                o.IgnoreObsoleteProperties();
                o.IgnoreObsoleteActions();
            });

            return services;
        }

        public static IApplicationBuilder UseAppSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/MyCompany.MyProject/swagger.json", "MyCompany.MyProject");
                o.DocExpansion(DocExpansion.None);
                o.DocumentTitle = "MyCompany.MyProject";
            });
            return app;
        }
    }
}

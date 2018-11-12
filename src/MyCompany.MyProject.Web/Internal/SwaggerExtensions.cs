using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace MyCompany.MyProject.Web.Internal
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddAppSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc(Constants.AppName, new Info());
                var appAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(w => w.FullName.StartsWith(Constants.AppName));
                foreach (var item in appAssemblies)
                {
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{item.GetName().Name}.xml");
                    o.IncludeXmlComments(xmlPath);
                }
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
                o.SwaggerEndpoint($"/swagger/{Constants.AppName}/swagger.json", Constants.AppName);
                o.DocExpansion(DocExpansion.None);
            });
            return app;
        }
    }
}

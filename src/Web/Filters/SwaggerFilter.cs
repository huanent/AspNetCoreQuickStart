using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace Web.Filters
{
    public class SwaggerFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            operation.Parameters = operation.Parameters ?? new List<IParameter>();

            operation.Parameters.Add(new NonBodyParameter()
            {
                Name = "Authorization",
                In = "header", //query formData ..
                Description = "请在此处填写Jwt令牌，令牌通过/api/Demo/JwtToken申请",
                Required = false,
                Type = "string"
            });
        }
    }
}
using MyCompany.MyProject.ApplicationCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using MyCompany.MyProject.ApplicationCore.SharedKernel;

namespace MyCompany.MyProject.Web.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class CheckPermissionsAttribute : ActionFilterAttribute
    {
        public string Permission { get; private set; }

        public CheckPermissionsAttribute(string permission)
        {
            Permission = permission;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!string.IsNullOrWhiteSpace(Permission))
            {
                var identity = context.HttpContext.RequestServices.GetService<ICurrentIdentity>();

#warning 在此根据identity是否存在Permission指定的权限来鉴定是否有权限,此处默认不鉴定权限，请在开发时更改
                //if (true)
                //{
                await next();
                //}
                //else
                //{
                //    context.Result = new ForbiddenResult("此账户无权限访问此api");
                //}

            }
            else await next();
        }
    }
    public class ForbiddenResult : ObjectResult
    {
        public ForbiddenResult(object value) : base(value)
        {
            StatusCode = (int)HttpStatusCode.Forbidden;
        }
    }
}

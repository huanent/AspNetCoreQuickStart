using ApplicationCore.IServices;
using ApplicationCore.Values;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Filters
{
    public class GlobalAuthorizationFilter : IAsyncAuthorizationFilter
    {
        readonly AppSettings _appSettings;
        readonly IPermissionService _permissionService;
        public GlobalAuthorizationFilter(IOptionsMonitor<AppSettings> options, IPermissionService permissionService)
        {
            _appSettings = options.CurrentValue;
            _permissionService = permissionService;
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            string userName = context.HttpContext.User.Identity.Name;
            if (userName == _appSettings.SuperAdminUserName) return Task.CompletedTask;

            if (!context.ActionDescriptor.FilterDescriptors.Any(a => a.Filter is AllowAnonymousFilter))
            {
                dynamic actionDescriptor = context.ActionDescriptor;
                string controllerName = actionDescriptor.ControllerName;
                string actionName = actionDescriptor.ActionName;
                string method = context.HttpContext.Request.Method;

                var httpMethod = Enum.Parse<HttpMethod>(method.ToUpper().Trim());
                bool isValid = _permissionService.ValidPermission(controllerName, actionName, httpMethod, userName);
                if (!isValid) context.Result = new UnauthorizedResult();
            }

            return Task.CompletedTask;
        }
    }
}

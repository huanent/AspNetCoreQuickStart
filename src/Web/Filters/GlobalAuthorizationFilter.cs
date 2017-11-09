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
        public GlobalAuthorizationFilter(IOptionsMonitor<AppSettings> options)
        {
            _appSettings = options.CurrentValue;
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

                bool isValid = ValidPermission(controllerName, actionName, userName, method);
                if (!isValid) context.Result = new UnauthorizedResult();
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// 此方法中进行用户的权限验证
        /// </summary>
        /// <param name="controllerName">控制器</param>
        /// <param name="actionName">方法</param>
        /// <param name="userName">用户名</param>
        /// <param name="method">http方法</param>
        /// <returns></returns>
        private bool ValidPermission(string controllerName, string actionName, string userName, string method)
        {
            return false;
        }
    }
}

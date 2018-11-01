using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using MyCompany.MyProject.Common;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Web.Application
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAppAuthentication(this IServiceCollection services, Cookie cookie)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = cookie.Name;
                options.ExpireTimeSpan = cookie.ExpireTimeSpan;
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.FromResult(string.Empty);
                };
                options.Events.OnRedirectToLogout = context =>
                {
                    context.Response.StatusCode = 200;
                    return Task.FromResult(string.Empty);
                };
            });

            return services;
        }
    }
}

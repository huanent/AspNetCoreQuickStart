using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

namespace MyCompany.MyProject.Web.Internal
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAppAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = Constants.AppName;
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

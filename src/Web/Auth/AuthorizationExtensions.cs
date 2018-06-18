using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Web.Auth
{
    public static class AuthorizationExtensions
    {
        public static IServiceCollection AddAppAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                var schemes = new List<string>
                {
                    CookieAuthenticationDefaults.AuthenticationScheme
                };

                //if(混合鉴权时)schemes.Add(其他授权scheme)

                options.DefaultPolicy = new AuthorizationPolicy(options.DefaultPolicy.Requirements, schemes);
            });

            return services;
        }
    }
}

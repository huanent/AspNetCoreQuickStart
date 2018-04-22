using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
                    JwtBearerDefaults.AuthenticationScheme
                };

                //if(混合鉴权时)schemes.Add(其他授权scheme)

                options.DefaultPolicy = new AuthorizationPolicy(options.DefaultPolicy.Requirements, schemes);
            });

            return services;
        }
    }
}

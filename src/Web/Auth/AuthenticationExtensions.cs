using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Web.Auth
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAppAuthentication(this IServiceCollection services, string jwtKey)
        {
            services.AddAuthentication()
            .AddJwtBearer(options =>
            {
                byte[] keyAsBytes = Encoding.ASCII.GetBytes(jwtKey);
                var signKey = new SymmetricSecurityKey(keyAsBytes);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false, //不验证发行方
                    ValidateAudience = false, //不验证受众方
                    //ValidateLifetime = false, //不验证过期时间
                    ClockSkew = TimeSpan.Zero, //时钟偏差设为0
                    IssuerSigningKey = signKey, //密钥
                };
            });

            services.AddSingleton<JwtHandler>();
            return services;
        }
    }
}

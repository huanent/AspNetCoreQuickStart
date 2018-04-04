using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Web.Utils
{
    public class JwtHandler
    {
        public const string JWT_REF_DATE = nameof(JWT_REF_DATE);

        static string GetToken(
           string key,
           IEnumerable<Claim> claims = null,
           DateTime? expires = null,
           DateTime? notBefore = null,
           string securityAlgorithms = SecurityAlgorithms.HmacSha256,
           string audience = null,
           string issuer = null)
        {
            var _signKey = GetSecurityKey(key);
            var header = new JwtHeader(new SigningCredentials(_signKey, securityAlgorithms));
            var payload = new JwtPayload(issuer, audience, claims, notBefore, expires);
            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static SecurityKey GetSecurityKey(string key)
        {
            var keyAsBytes = Encoding.ASCII.GetBytes(key);
            return new SymmetricSecurityKey(keyAsBytes);
        }

        public static string CreateToken(string key, string sid, TimeSpan exp, TimeSpan Refresh)
        {
            string token = GetToken(
               key,
               new Claim[] {
                    new Claim(ClaimTypes.Sid,sid),
                    new Claim(JWT_REF_DATE,DateTime.UtcNow.Add(Refresh).ToString()),
               }, DateTime.UtcNow.Add(exp));

            token = $"Bearer {token}";

            return token;
        }
    }
}
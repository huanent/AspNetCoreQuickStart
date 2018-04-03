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

        public static string GetToken(
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
    }
}
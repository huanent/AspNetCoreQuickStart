using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Web.Services
{
    public class JwtService
    {
        readonly AppSettings _appSettings;
        public JwtService(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }

        public string GetToken(IEnumerable<Claim> claims)
        {
            var _signKey = GetSecurityKey(_appSettings.JwtKey);
            var header = new JwtHeader(new SigningCredentials(_signKey, SecurityAlgorithms.HmacSha256));
            var payload = new JwtPayload(null, null, claims, null, DateTime.UtcNow.AddHours(1));
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

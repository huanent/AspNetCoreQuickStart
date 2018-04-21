using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Web.Application;

namespace Web.Auth
{
    public class JwtHandler
    {
        readonly Jwt _jwt;

        public const string JWT_REFRESH_DATE = nameof(JWT_REFRESH_DATE);

        public JwtHandler(IOptions<Jwt> options)
        {
            _jwt = options.Value;
        }

        public string CreateToken(params Claim[] claims)
        {
            byte[] keyAsBytes = Encoding.ASCII.GetBytes(_jwt.Key);
            var signKey = new SymmetricSecurityKey(keyAsBytes);
            var claimsWithRefresh = claims.ToList();
            string refreshDate = DateTime.UtcNow.Add(_jwt.Refresh).ToBinary().ToString();
            claimsWithRefresh.Add(new Claim(JWT_REFRESH_DATE, refreshDate));
            var header = new JwtHeader(new SigningCredentials(signKey, _jwt.SecurityAlgorithm));

            var payload = new JwtPayload(
                null,
                null,
                claimsWithRefresh,
                DateTime.UtcNow.Add(_jwt.NotBefore),
                DateTime.UtcNow.Add(_jwt.Exp));

            var securityToken = new JwtSecurityToken(header, payload);
            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return $"Bearer {token}";
        }
    }
}
using ApplicationCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Utils;

namespace Web.Filters
{
    public class GlobalResultFilter : IAsyncResultFilter
    {
        readonly Jwt _jwt;
        readonly IAppLogger<GlobalResultFilter> _appLogger;
        public GlobalResultFilter(IOptions<Jwt> options, IAppLogger<GlobalResultFilter> appLogger)
        {
            _jwt = options.Value;
            _appLogger = appLogger;
        }
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var user = context.HttpContext.User;
            if (user.Identity.IsAuthenticated)
            {
                string refresh = user.FindFirstValue(nameof(_jwt.Refresh));
                if (refresh != null && DateTime.UtcNow > DateTime.Parse(refresh))
                {

                    string token = JwtHandler.GetToken(
                                       _jwt.Key,
                                       new Claim[] {
                                            new Claim(ClaimTypes.Sid,user.FindFirstValue(ClaimTypes.Sid)),
                                            new Claim(ClaimTypes.Expired,user.FindFirstValue(ClaimTypes.Expired)),
                                            new Claim(nameof(_jwt.Refresh),DateTime.UtcNow.Add(_jwt.Refresh).ToString()),
                                       }, DateTime.UtcNow.Add(TimeSpan.Parse(user.FindFirstValue(ClaimTypes.Expired))));

                    token = $"Bearer {token}";
                    context.HttpContext.Response.Headers.Add("jwt", token);
                    _appLogger.Info($"成功刷新Jwt令牌为 {token}");
                }
            }
            await next();
        }
    }
}

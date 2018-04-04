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
                string refresh = user.FindFirstValue(JwtHandler.JWT_REF_DATE);
                string sid = user.FindFirstValue(ClaimTypes.Sid);

                if (refresh != null && sid != null && DateTime.UtcNow > DateTime.Parse(refresh))
                {
                    string token = JwtHandler.CreateToken(_jwt.Key, sid, _jwt.Exp, _jwt.Refresh);
                    context.HttpContext.Response.Headers.Add(_jwt.HeaderName, token);
                    _appLogger.Info($"成功刷新Jwt令牌为 {token}");
                }
            }
            await next();
        }
    }
}

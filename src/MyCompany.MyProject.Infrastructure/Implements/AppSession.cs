using System;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace MyCompany.MyProject.Infrastructure.Implements
{
    [InjectScoped(typeof(IAppSession))]
    internal class AppSession : IAppSession
    {
        public AppSession(IHttpContextAccessor httpContextAccessor)
        {
            var ctx = httpContextAccessor.HttpContext;
            if (ctx == null) throw new Exception("请勿在Http请求之外使用此服务！");
            var sid = ctx.User.FindFirst(ClaimTypes.Sid);
            Guid.TryParse(sid.Value, out var userId);
            UserId = userId;
            CancellationToken = ctx.RequestAborted;
        }

        public bool Signed => UserId == default(Guid);

        public Guid UserId { get; }

        public CancellationToken CancellationToken { get; }
    }
}

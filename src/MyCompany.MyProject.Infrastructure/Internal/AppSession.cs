using System;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace MyCompany.MyProject.Infrastructure.Internal
{
    [InjectScoped(typeof(IAppSession))]
    internal class AppSession : IAppSession
    {
        public AppSession(IHttpContextAccessor httpContextAccessor)
        {
            var ctx = httpContextAccessor.HttpContext;
            if (ctx == null) throw new Exception("请勿在Http请求之外使用此服务！");
            CancellationToken = ctx.RequestAborted;
            Signed = ctx.User.Identity.IsAuthenticated;
            var sid = ctx.User.FindFirst(ClaimTypes.Sid);
            if (sid != null)
            {
                Guid.TryParse(sid.Value, out var userId);
                UserId = userId;
            }
        }

        public bool Signed { get; }

        public Guid UserId { get; }

        public CancellationToken CancellationToken { get; }
    }
}

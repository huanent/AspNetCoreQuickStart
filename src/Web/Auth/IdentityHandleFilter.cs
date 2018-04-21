using ApplicationCore;
using Infrastructure.SharedKernel;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Auth
{
    public class IdentityHandleFilter : IAsyncResourceFilter
    {
        CurrentIdentity _identity;
        public IdentityHandleFilter(ICurrentIdentity identity)
        {
            _identity = (CurrentIdentity)identity;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            bool isAuth = context.HttpContext.User.Identity.IsAuthenticated;
            string id = context.HttpContext.User.FindFirstValue(ClaimTypes.Sid);
            if (isAuth && id == null) throw new AppException("Token缺少关键信息");
            _identity.SetIdentity(isAuth, id == null ? Guid.Empty : new Guid(id));
            await next();
        }
    }
}

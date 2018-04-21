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
        readonly CurrentIdentity _identity;
        public IdentityHandleFilter(ICurrentIdentity identity)
        {
            _identity = (CurrentIdentity)identity;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            bool isAuth = context.HttpContext.User.Identity.IsAuthenticated;

            if (isAuth)
            {
                string id = context.HttpContext.User.FindFirstValue(ClaimTypes.Sid);
                if (id == null) throw new AppException("Token缺少关键信息Sid,sid为用户唯一识别码，一般为主键id");
                _identity.SetIdentity(true, new Guid(id));
            }
            else _identity.SetIdentity(false, Guid.Empty);

            await next();
        }
    }
}

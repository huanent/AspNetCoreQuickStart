using ApplicationCore;
using Infrastructure.SharedKernel;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Filters
{
    public class GlobalResourceFilter : IAsyncResourceFilter
    {
        CurrentIdentity _identity;
        public GlobalResourceFilter(ICurrentIdentity identity)
        {
            _identity = (CurrentIdentity)identity;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            bool isAuth = context.HttpContext.User.Identity.IsAuthenticated;
            string id = context.HttpContext.User.FindFirstValue(ClaimTypes.Sid);
            _identity.SetIdentity(isAuth && id != null, id == null ? Guid.Empty : new Guid(id));
            await next();
        }
    }
}

using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using MyCompany.MyProject.Infrastructure;

namespace MyCompany.MyProject.Web.Internal
{
    public class HandlerIdentityFilter : IAsyncResourceFilter
    {
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var identity = context.HttpContext.RequestServices.GetService<IIdentity>() as Identity;
                var id = context.HttpContext.User.FindFirstValue(ClaimTypes.Sid);
                if (!Guid.TryParse(id, out var gId)) throw new BadRequestException(Message.IdentityError);
                identity.SetClaims(gId);
            }

            await next();
        }
    }
}

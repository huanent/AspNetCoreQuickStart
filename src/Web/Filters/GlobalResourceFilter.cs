using ApplicationCore.ISharedKernel;
using Infrastructure.SharedKernel;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
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

#warning 此处后续应该添加对Id的有效性认证如果Id不能够在User表中找到，则返回一个401,如果isAuth为false则在User表创建一个匿名用户
            _identity.SetIdentity(isAuth && id != null, id == null ? Guid.Empty : new Guid(id));
            await next();
        }
    }
}

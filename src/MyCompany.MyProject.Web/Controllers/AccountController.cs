using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace MyCompany.MyProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        [HttpPost("SiginOut")]
        public async System.Threading.Tasks.Task SiginOutAsync()
        {
            await HttpContext.SignOutAsync();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost("Sign")]
        public async System.Threading.Tasks.Task SignAsync()
        {
            var claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString()));
            await HttpContext.SignInAsync(claimsPrincipal);
        }
    }
}

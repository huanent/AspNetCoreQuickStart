using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        public async Task SiginOutAsync([FromServices]ILogger<AccountController> logger)
        {
            logger.LogInformation("退出");
            await HttpContext.SignOutAsync();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost("Sign")]
        public async Task SignAsync()
        {
            var claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString()));
            await HttpContext.SignInAsync(claimsPrincipal);
        }
    }
}

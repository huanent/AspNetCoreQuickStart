using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Dtos;
using AutoMapper;
using ApplicationCore.Entities;
using ApplicationCore.IRepositories;
using Microsoft.AspNetCore.Authorization;
using ApplicationCore.IServices;
using Microsoft.Extensions.Options;
using ApplicationCore.SharedKernel;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Web.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        [HttpGet]
        public IEnumerable<User> Get([FromServices] IUserRepository userRepository)
        {
            return userRepository.AllUser();
        }

        [AllowAnonymous]
        [HttpPost]
        public void Post(
            [FromBody]UserDto userDto,
            [FromServices] IMapper mapper,
            [FromServices] IUserService userService,
            [FromServices] IOptionsMonitor<AppSettings> options,
            [FromServices] ICoding coding)
        {
            var appSettings = options.CurrentValue;
            var user = mapper.Map<User>(userDto);
            user.Pwd = coding.MD5Encoding(user.Pwd, appSettings.UserPwdSalt);
            userService.CreateUser(user);
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public void Login(
            [FromBody] LoginDto loginDto,
            [FromServices] IUserService userService,
            [FromServices] IOptionsMonitor<AppSettings> options,
            [FromServices] ICoding coding)
        {
            var appSettings = options.CurrentValue;
            string pwd = coding.MD5Encoding(loginDto.Pwd, appSettings.UserPwdSalt);
            if (loginDto.Name != appSettings.SuperAdminUserName) userService.ValidUserLogin(loginDto.Name, pwd);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Sid, loginDto.Name));
            identity.AddClaim(new Claim(ClaimTypes.Name, loginDto.Name));
            var principal = new ClaimsPrincipal(identity);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();
        }

        [HttpPut(nameof(SetPermission))]
        public void SetPermission()
        {

        }
    }
}
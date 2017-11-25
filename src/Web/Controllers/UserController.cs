using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
using ApplicationCore.Exceptions;
using Web.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Web.Services;

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

        [HttpPost]
        public void Post(
            [FromBody]UserViewModel userDto,
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
        [HttpPost("Login")]
        [Produces(typeof(string))]
        public string Login(
            [FromBody] LoginViewModel loginDto,
            [FromServices] IUserService userService,
            [FromServices] IOptionsMonitor<AppSettings> options,
            [FromServices] ICoding coding,
            [FromServices] JwtService jwtService)
        {
            var appSettings = options.CurrentValue;
            string pwd = coding.MD5Encoding(loginDto.Pwd, appSettings.UserPwdSalt);
            if (loginDto.Name != appSettings.SuperAdminUserName) userService.ValidUserLogin(loginDto.Name, pwd);

            return jwtService.GetToken(new Claim[] {
                new Claim(ClaimTypes.Name, loginDto.Name)
            });
        }

        [HttpPut("SetPermission/{userId}")]
        public void SetPermission(
            Guid userId,
            [FromBody]IEnumerable<PermissionViewModel> PermissionViewModels,
            [FromServices] IPermissionService permissionService,
            [FromServices] IMapper mapper)
        {
            var permissions = PermissionViewModels.Select(s =>
             {
                 var permission = mapper.Map<Permission>(s);
                 permission.UserId = userId;
                 permission.Action = permission.Action.Trim().ToLower();
                 permission.Controller = permission.Controller.Trim().ToLower();
                 return permission;
             });
            permissionService.ChangeUserPermission(userId, permissions);
        }
    }
}
using MyCompany.MyProject.ApplicationCore.Dtos;
using MyCompany.MyProject.ApplicationCore.Dtos.Common;
using MyCompany.MyProject.ApplicationCore.Entities;
using MyCompany.MyProject.ApplicationCore.IRepositories;
using MyCompany.MyProject.ApplicationCore.IServices;
using MyCompany.MyProject.ApplicationCore.SharedKernel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Extensions.Caching.Memory;

namespace MyCompany.MyProject.Web.Controllers
{
    /// <summary>
    /// 使用演示
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        readonly IAppLogger<DemoController> _logger;
        readonly IDemoRepository _demoRepository;
        readonly IDemoService _demoService;

        public DemoController(
            IAppLogger<DemoController> appLogger,
            IDemoRepository demoRepository,
            IDemoService demoService,
            IHostingEnvironment env)
        {
            if (env.IsProduction()) throw new AppException("当前为正式环境，不提供测试");
            _logger = appLogger;
            _demoRepository = demoRepository;
            _demoService = demoService;
        }

        #region 系统信息

        /// <summary>
        /// 获取系统当前时间
        /// </summary>
        /// <param name="systemDateTime"></param>
        /// <returns></returns>
        [HttpGet("NowDateTime")]
        [ProducesResponseType(typeof(string), 200)]
        public DateTime NowDateTime([FromServices] ISystemDateTime systemDateTime) => systemDateTime.Now;

        #endregion 系统信息

        #region 身份验证

        /// <summary>
        /// 获取当前身份信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("CurrentIdentity"), Authorize]
        [ProducesResponseType(typeof(string), 200)]
        public dynamic CurrentIdentity([FromServices]ICurrentIdentity identity) => identity.Id;


        [HttpPost("Login")]
        public async System.Threading.Tasks.Task LoginAsync()
        {
            var claimsPrincipal = new ClaimsPrincipal();
            var claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Sid, "huanent"));
            claimsPrincipal.AddIdentity(claimsIdentity);

            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddYears(10)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);
        }

        [HttpPost("Logout")]
        public async System.Threading.Tasks.Task LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        #endregion 身份验证

        #region 查询

        /// <summary>
        /// 使用EF查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUseEF")]
        public IEnumerable<DemoDto> GetUseEF()
        {
            return _demoRepository.All();
        }

        /// <summary>
        /// 使用Dapper查询
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        [HttpGet("GetUseDapper/{top}")]
        public IEnumerable<Demo> GetUseDapper(int top)
        {
            return _demoRepository.GetTopRecords(top);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="model">分页查询模型</param>
        /// <returns></returns>
        [HttpGet(nameof(GetPageList))]
        public PageDto<DemoDto> GetPageList(QueryDemoPageDto model)
        {
            return _demoRepository.GetPage(model.PageIndex, model.PageSize, model.Age, model.Name);
        }

        /// <summary>
        /// 从缓存读取实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="memoryCache"></param>
        /// <returns></returns>
        [HttpGet("FindOnCache/{id}")]
        public Demo FindOnCache(Guid id, [FromServices]IMemoryCache memoryCache)
        {
            return memoryCache.GetOrCreate(id, c =>
             {
                 c.SetAbsoluteExpiration(TimeSpan.FromMinutes(3));
                 return _demoRepository.FindByKey(id);
             });
        }
        #endregion 查询

        #region 增删改

        /// <summary>
        /// 添加实体示例
        /// </summary>
        [HttpPost]
        public async System.Threading.Tasks.Task PostAsync([FromBody] AddDemoDto model)
        {
            await _demoService.CreateDemoAsync(model);
        }

        /// <summary>
        /// 更新实体示例
        /// </summary>
        /// <param name="model"></param>
        [HttpPut]
        public void Put([FromBody] EditDemoDto model)
        {
            _demoService.UpdateDemo(model);
        }

        /// <summary>
        /// 删除实体示例
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _demoRepository.Delete(id);
        }

        #endregion 增删改
    }
}
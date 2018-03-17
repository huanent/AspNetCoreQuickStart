using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.IRepositories;
using ApplicationCore.Models;
using ApplicationCore.SharedKernel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Web.Controllers
{
    /// <summary>
    /// 使用演示
    /// </summary>
    [Produces("application/json")]
    [Route("api/Demo")]
    public class DemoController : Controller
    {
        readonly IAppLogger<DemoController> _logger;
        readonly IDemoRepository _demoRepository;

        public DemoController(
            IAppLogger<DemoController> appLogger,
            IDemoRepository demoRepository)
        {
            _logger = appLogger;
            _demoRepository = demoRepository;
        }


        #region 系统信息

        /// <summary>
        /// 获取系统当前时间
        /// </summary>
        /// <param name="systemDateTime"></param>
        /// <returns></returns>
        [HttpGet]
        public DateTime NowDateTime([FromServices] ISystemDateTime systemDateTime) => systemDateTime.Now;

        #endregion

        #region 身份验证
        /// <summary>
        /// 测试申请jwt令牌
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        [HttpGet(nameof(JwtToken)), Produces(typeof(String))]
        public string JwtToken(
            [FromServices] IOptions<Settings> settings,
            [FromServices]IHostingEnvironment env)
        {
            if (env.IsProduction()) return "当前为正式环境，此处不提供Token申请";
            var options = settings.Value;

            string token = JwtHandler.GetToken(
               options.JwtKey,
               new Claim[] {
                    new Claim(ClaimTypes.Name,"testAccount"),
               }, DateTime.UtcNow.AddDays(1));

            return $"Bearer {token}";
        }

        /// <summary>
        /// 获取当前身份信息
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(AuthIdentity)), Authorize]
        public dynamic AuthIdentity([FromServices]IHostingEnvironment env)
        {
            if (env.IsProduction()) return "当前为正式环境，不提供此查询";
            return User.Identity.Name;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 使用EF查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUseEF")]
        public IEnumerable<Demo> GetUseEF()
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
        /// <param name="dto">查询参数，如果有而外条件请继承此dto</param>
        /// <returns></returns>
        [HttpGet(nameof(GetPageList))]
        public PageModel<Demo> GetPageList(GetPageModel dto)
        {
            var list = _demoRepository.GetPage(dto.GetOffset(), dto.PageSize, out int total);

            return new PageModel<Demo>
            {
                List = list,
                Total = total
            };
        }
        #endregion

        #region 增删改
        /// <summary>
        /// 添加实体示例
        /// </summary>
        [HttpPost]
        public void Post([FromBody] DemoModel model)
        {
            _demoRepository.AddAsync(model).Wait();
        }

        /// <summary>
        /// 更新实体示例
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        [HttpPut("{id}")]
        public void Put([FromBody] DemoModel model, Guid id)
        {
            _demoRepository.Save(model, id);
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
        #endregion
    }
}
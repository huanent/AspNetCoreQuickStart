using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.IRepositories;
using ApplicationCore.ISharedKernel;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Web.Utils;

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

        #endregion 系统信息

        #region 身份验证

        /// <summary>
        /// 测试申请jwt令牌
        /// </summary>
        /// <param name="options"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        [HttpGet(nameof(JwtToken)), Produces(typeof(String))]
        public void JwtToken(
            [FromServices] IOptions<Jwt> options,
            [FromServices]IHostingEnvironment env)
        {
            if (env.IsProduction()) throw new AppException("当前环境为生产环境，不提供令牌申请");
            var settings = options.Value;

            string token = JwtHandler.GetToken(
               settings.Key,
               new Claim[] {
                    new Claim(ClaimTypes.Sid,Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Expired,settings.Exp.ToString()),
                    new Claim(nameof(settings.Refresh),DateTime.UtcNow.Add(settings.Refresh).ToString()),
               }, DateTime.UtcNow.Add(settings.Exp));

            token = $"Bearer {token}";
            Response.Headers.Add(settings.HeaderName, token);
        }

        /// <summary>
        /// 获取当前身份信息
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(AuthIdentity)), Authorize]
        public dynamic AuthIdentity(
            [FromServices]IHostingEnvironment env,
            [FromServices] ICurrentIdentity identity)
        {
            if (env.IsProduction()) return "当前为正式环境，不提供此查询";
            return identity.Id;
        }

        #endregion 身份验证

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
            return _demoRepository.GetPage(dto);
        }

        #endregion 查询

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

        #endregion 增删改

        [HttpGet("RunTransaction")]
        public void RunTransaction([FromServices]IUnitOfWork unitOfWork)
        {
            var tran = unitOfWork.BeginTransaction();
            _demoRepository.AddAsync(new DemoModel { Name = "张三" });
            var records = _demoRepository.GetTopRecords(10, tran);
            tran.Commit();
        }
    }
}
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.IRepositories;
using ApplicationCore.SharedKernel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Web.Dtos;

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
        #endregion

        #region 增删改
        /// <summary>
        /// 添加实体示例
        /// </summary>
        /// <param name="sequenceGuidGenerator"></param>
        /// <param name="demoDto">Demo传输对象</param>
        [HttpPost]
        public void Post(
            [FromServices]ISequenceGuidGenerator sequenceGuidGenerator,
            [FromBody] DemoDto demoDto)
        {
            var demo = demoDto.ToDemo();

            demo.CreateIdWhenIsEmpty(sequenceGuidGenerator.SqlServerKey());//这个步骤非必需，仅演示用

            _demoRepository.AddAsync(demo).Wait();
        }

        /// <summary>
        /// 更新实体示例
        /// </summary>
        /// <param name="demoDto"></param>
        /// <param name="id"></param>
        [HttpPut("{id}")]
        public void Put([FromBody] DemoDto demoDto, Guid id)
        {
            var demo = _demoRepository.FindByKey(id);

            _logger.Warn($"尝试更新Id为{id}的Demo失败，原因为未在数据库找到");
            if (demo == null) throw new AppException("未能找到指定的Demo");

            demoDto.ToDemo(demo);
            _demoRepository.Save(demo);
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
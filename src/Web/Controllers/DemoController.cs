using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.IRepositories;
using ApplicationCore.SharedKernel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        public DemoController(IAppLogger<DemoController> appLogger, IDemoRepository demoRepository)
        {
            _logger = appLogger;
            _demoRepository = demoRepository;
        }

        /// <summary>
        /// 日志输出
        /// </summary>
        [HttpGet("Log")]
        public void Log()
        {
            string msg = $"输出测试日志";
            _logger.Warn(msg);
        }

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

        [HttpPut("{id}")]
        public void Put([FromBody] DemoDto demoDto, Guid id)
        {
            var demo = _demoRepository.FindByKey(id);

            _logger.Warn($"尝试更新Id为{id}的Demo失败，原因为未在数据库找到");
            if (demo == null) throw new AppException("未能找到指定的Demo");

            demoDto.ToDemo(demo);
            _demoRepository.Save(demo);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _demoRepository.Delete(id);
        }
    }
}
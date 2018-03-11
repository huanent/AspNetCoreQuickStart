using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.SharedKernel;
using ApplicationCore.IRepositories;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using Microsoft.Extensions.Logging;
using System.Transactions;

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

        public DemoController(IAppLogger<DemoController> appLogger)
        {
            _logger = appLogger;
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
        /// <param name="demoRepository"></param>
        /// <returns></returns>
        [HttpGet("GetUseEF")]
        public IEnumerable<Demo> GetUseEF([FromServices]IDemoRepository demoRepository)
        {
            return demoRepository.AllDemo();
        }

        /// <summary>
        /// 使用Dapper查询
        /// </summary>
        /// <param name="demoRepository"></param>
        /// <returns></returns>
        [HttpGet("GetUseDapper")]
        public IEnumerable<Demo> GetUseDapper([FromServices]IDemoRepository demoRepository)
        {
            return demoRepository.GetTop10Demo();
        }

    }
}
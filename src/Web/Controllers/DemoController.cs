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
using Infrastructure;

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
        readonly UnitOfWorkFactory _unitOfWorkFactory;

        public DemoController(IAppLogger<DemoController> appLogger, UnitOfWorkFactory unitOfWorkFactory)
        {
            _logger = appLogger;
            _unitOfWorkFactory = unitOfWorkFactory;
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
            return demoRepository.All();
        }

        /// <summary>
        /// 使用Dapper查询
        /// </summary>
        /// <param name="demoRepository"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        [HttpGet("GetUseDapper/{top}")]
        public IEnumerable<Demo> GetUseDapper([FromServices]IDemoRepository demoRepository, int top)
        {
            return demoRepository.GetTopRecords(top);
        }

        /// <summary>
        /// 使用事务
        /// </summary>
        /// <param name="demoRepository"></param>
        [HttpPost("UseTransaction")]
        public void UseTransaction([FromServices]IDemoRepository demoRepository)
        {
            var tran = _unitOfWorkFactory.GetUnitOfWork<AppDbContext>().BeginTransaction();
            var entity = new Demo("张三");
            demoRepository.AddAsync(entity).Wait();
            demoRepository.Update(entity, tran);
            tran.Commit();
        }
    }
}
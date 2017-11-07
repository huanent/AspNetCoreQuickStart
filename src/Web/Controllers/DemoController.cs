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

namespace Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Demo")]
    public class DemoController : Controller
    {
        [HttpGet(nameof(RunTransaction))]
        public void RunTransaction([FromServices] IUnitOfWork unitOfWork, [FromServices] IDemoRepository demoRepository)
        {
            unitOfWork.RunTransaction((c, r) =>
            {
                try
                {
                    demoRepository.AddDemoAsync(new Demo
                    {
                        Name = "张三"
                    }).Wait();

                    demoRepository.AddDemoAsync(new Demo
                    {
                        Name = "李四"
                    }).Wait();
                    c();
                }
                catch (Exception e)
                {
                    r();
                    throw e;
                }
            });
        }


    }
}
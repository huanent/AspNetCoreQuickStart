using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCompany.MyProject.Application.Demos.Commands;
using MyCompany.MyProject.Application.Demos.Models;
using MyCompany.MyProject.Application.Demos.Queries;

namespace MyCompany.MyProject.Web.Controllers
{
    /// <summary>
    /// 使用演示
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        /// <summary>
        /// 删除Demo
        /// </summary>
        /// <param name="command"></param>
        [HttpDelete]
        public void Delete([FromQuery] DeleteDemoCommand command)
        {
            this.Mediator().Send(command, HttpContext.RequestAborted);
        }

        /// <summary>
        /// 根据Id查新Demo实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<DemoModel> GetByIdAsync([FromQuery]GetDemoByIdQuery query)
        {
            return await this.Mediator().Send(query, HttpContext.RequestAborted);
        }

        /// <summary>
        /// 创建Demo
        /// </summary>
        /// <param name="command"></param>
        [HttpPost]
        public void Post([FromBody] CreateDemoCommand command)
        {
            this.Mediator().Send(command, HttpContext.RequestAborted);
        }

        /// <summary>
        /// 修改Demo
        /// </summary>
        /// <param name="command"></param>
        [HttpPut]
        public void Put([FromBody] ModifyDemoCommand command)
        {
            this.Mediator().Send(command, HttpContext.RequestAborted);
        }
    }
}

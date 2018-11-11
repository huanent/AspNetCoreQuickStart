using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCompany.MyProject.Application.Demo.Commands;
using MyCompany.MyProject.Application.Demo.Models;
using MyCompany.MyProject.Application.Demo.Queries;
using MyCompany.MyProject.Commands.Demo;

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
        [HttpDelete("{id}")]
        public void Delete([FromRoute] DeleteDemoCommand command)
        {
            this.Mediator().Send(command);
        }

        /// <summary>
        /// 根据Id查新Demo实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<DemoModel> GetByIdAsync([FromRoute] GetDemoByIdQuery query)
        {
            return await this.Mediator().Send(query);
        }

        /// <summary>
        /// 创建Demo
        /// </summary>
        /// <param name="command"></param>
        [HttpPost]
        public void Post([FromBody] CreateDemoCommand command)
        {
            this.Mediator().Send(command);
        }

        /// <summary>
        /// 修改Demo
        /// </summary>
        /// <param name="command"></param>
        [HttpPut]
        public void Put([FromBody] ModifyDemoCommand command)
        {
            this.Mediator().Send(command);
        }
    }
}

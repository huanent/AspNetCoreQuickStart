using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyCompany.MyProject.Commands.Demo;
using MyCompany.MyProject.Dto.Demo;

namespace MyCompany.MyProject.Web.Controllers
{
    /// <summary>
    /// 使用演示
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DemoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 获取所有Demo实体
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<DemoDto>> GetAsync() => await _mediator.Send(new GetDemosRequest());

        /// <summary>
        /// 根据Id查新Demo实体
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<DemoDto> GetByIdAsync([FromRoute]FindDemoByIdRequest request) => await _mediator.Send(request);

        /// <summary>
        /// 创建Demo
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        public void Post([FromBody] CreateDemoRequest request) => _mediator.Send(request);

        /// <summary>
        /// 修改Demo
        /// </summary>
        /// <param name="request"></param>
        [HttpPut]
        public void Put([FromBody]ModifyDemoRequest request) => _mediator.Send(request);
    }
}

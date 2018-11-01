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
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<DemoDto>> GetAsync()
        {
            return await _mediator.Send(new GetDemosRequest());
        }

        /// <summary>
        /// 根据Id查新Demo实体
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<DemoDto> GetByIdAsync([FromRoute]FindDemoByIdRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}

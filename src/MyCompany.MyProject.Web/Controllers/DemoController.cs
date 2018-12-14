using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCompany.MyProject.Application.Dtos.Demo;
using MyCompany.MyProject.Application.Repositories;
using MyCompany.MyProject.Application.Services;

namespace MyCompany.MyProject.Web.Controllers
{
    /// <summary>
    /// 使用演示
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IDemoService _demoService;
        private readonly IDemoRepository _demoRepository;

        public DemoController(IDemoService demoService, IDemoRepository demoRepository)
        {
            _demoService = demoService;
            _demoRepository = demoRepository;
        }

        /// <summary>
        /// 获取Demo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<DemoDto> GetAsync([Required] Guid id)
        {
            return await _demoRepository.GetEditViewAsync(id);
        }

        /// <summary>
        /// 创建Demo
        /// </summary>
        /// <param name="addDemoDto"></param>
        [HttpPost]
        public async Task PostAsync(AddDemoDto addDemoDto)
        {
            await _demoService.AddDemoAsync(addDemoDto);
        }

        /// <summary>
        /// 更新Demo
        /// </summary>
        /// <param name="addDemoDto"></param>
        [HttpPut]
        public async Task PutAsync(UpdateDemoDto addDemoDto)
        {
            await _demoService.UpdateDemoAsync(addDemoDto);
        }

        /// <summary>
        /// 删除Demo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task DeleteAsync([Required]Guid id)
        {
            await _demoService.DeleteAsync(id);
        }

        /// <summary>
        /// 分页获取Demo
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<PageDto<DemoDto>> GetPageAsync([FromQuery]GetDemoPageDto dto)
        {
            return await _demoRepository.GetPageAsync(dto);
        }
    }
}

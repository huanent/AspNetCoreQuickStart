﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCompany.MyProject.Application.Dtos.Demo;
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

        public DemoController(IDemoService demoService)
        {
            _demoService = demoService;
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
    }
}

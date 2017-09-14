using ApplicationCore.Entities;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class DemoController : Controller
    {
        IDemoService _demoService;
        public DemoController(IDemoService demoService)
        {
            _demoService = demoService;
        }

        [HttpGet("{id}")]
        public Demo Get(Guid id)
        {
            return _demoService.GetDemoByKey(id);
        }
    }
}

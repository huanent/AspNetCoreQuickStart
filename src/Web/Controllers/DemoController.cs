using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Web.Dtos;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class DemoController : Controller
    {
        readonly IRepository<Demo> _repository;
        public DemoController(IRepository<Demo> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<Demo> Get()
        {
            return _repository.Query(new List<Expression<Func<Demo, object>>> {
                d=>d.DemoItems
            })
            .ToArray();
        }

        [HttpPut("{id}")]
        public bool Put([FromBody]DemoDto dto, Guid id, [FromServices]IUnitOfWork unitOfWork)
        {
            var entity = _repository.Find(id);
            if (entity == null) throw new AppException("未找到要更新的Demo");
            _repository.Update(dto.ToDemo(entity));
            return unitOfWork.SaveChanges() > 0;
        }

        [HttpPost]
        public bool Post([FromBody]DemoDto dto, [FromServices]IUnitOfWork unitOfWork)
        {
            _repository.Update(dto.ToDemo());
            return unitOfWork.SaveChanges() > 0;
        }

        [HttpDelete("id")]
        public bool Delete(Guid id, [FromServices]IUnitOfWork unitOfWork)
        {
            var entity = _repository.Find(id);
            if (entity == null) throw new AppException("未找到要删除的Demo");
            _repository.Delete(entity);
            return unitOfWork.SaveChanges() > 0;
        }

        [HttpGet(nameof(JsonReporting))]
        public string JsonReporting([FromServices] IDemoReportingService demoReportingService)
        {
            return demoReportingService.ExportDemoToJson();
        }
    }
}

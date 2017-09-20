using ApplicationCore;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            return _repository.Query(Include: q =>
            {
                q = q.Include(i => i.DemoItems);
                return q;
            });
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
    }
}

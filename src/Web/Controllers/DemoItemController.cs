using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Dtos;

namespace Web.Controllers
{
    [Produces("application/json")]
    [Route("api/DemoItem")]
    public class DemoItemController : Controller
    {
        IRepository<DemoItem> _repository;
        public DemoItemController(IRepository<DemoItem> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<DemoItem> Get()
        {
            return _repository.Query().ToArray();
        }

        // POST: api/DemoItem
        [HttpPost("{demoId}")]
        public bool Post([FromBody]DemoItemDto dto, Guid demoId, [FromServices]IUnitOfWork unitOfWork)
        {
            _repository.Add(dto.ToDemoItem(demoId));
            return unitOfWork.SaveChanges() > 0;
        }

        [HttpDelete("{id}")]
        public bool Delete(Guid id, [FromServices] IUnitOfWork unitOfWork)
        {
            var entity = _repository.Find(id);
            if (entity == null) throw new AppException("未找到要删除的DemoItem");
            _repository.Delete(entity);
            return unitOfWork.SaveChanges() > 0;
        }
    }
}

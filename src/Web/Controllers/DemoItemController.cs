using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Entities;
using ApplicationCore;
using Web.Dtos;
using ApplicationCore.Exceptions;
using Microsoft.EntityFrameworkCore;

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
            return _repository.Query
                .AsNoTracking()
                .ToArray();
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

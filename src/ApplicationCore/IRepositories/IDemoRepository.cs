using ApplicationCore.Dtos;
using ApplicationCore.Dtos.Common;
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.IRepositories
{
    public interface IDemoRepository
    {
        IEnumerable<DemoDto> All();

        Task AddAsync(Demo demo);

        IEnumerable<Demo> GetTopRecords(int count);

        Demo FindByKey(Guid id);

        Demo FindByKeyOnCache(Guid id);

        void Update(Demo entity);

        void Delete(Guid id);
        PageDto<DemoDto> GetPage(int pageIndex, int pageSize, int? age, string name);
    }
}
using MyCompany.MyProject.ApplicationCore.Dtos;
using MyCompany.MyProject.ApplicationCore.Dtos.Common;
using MyCompany.MyProject.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCompany.MyProject.ApplicationCore.IRepositories
{
    public interface IDemoRepository
    {
        IEnumerable<DemoDto> All();

        Task AddAsync(Demo demo);

        IEnumerable<Demo> GetTopRecords(int count);

        Demo FindByKey(Guid id);

        void Update(Demo entity);

        void Delete(Guid id);

        PageDto<DemoDto> GetPage(int pageIndex, int pageSize, int? age, string name);
    }
}
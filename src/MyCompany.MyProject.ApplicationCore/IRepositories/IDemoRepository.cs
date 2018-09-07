using MyCompany.MyProject.ApplicationCore.Dtos.Demo;
using MyCompany.MyProject.ApplicationCore.Dtos.Page;
using MyCompany.MyProject.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCompany.MyProject.ApplicationCore.IRepositories
{
    public interface IDemoRepository
    {
        Task AddAsync(Demo demo);

        IEnumerable<DemoDto> All();

        void Delete(Guid id);

        Demo FindByKey(Guid id);

        PageDto<DemoDto> GetPage(QueryDemoPageDto dto);

        IEnumerable<Demo> GetTopRecords(int count);

        void Update(Demo entity);
    }
}

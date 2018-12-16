using System;
using System.Threading.Tasks;
using MyCompany.MyProject.Application.Dtos.Demo;

namespace MyCompany.MyProject.Application.Entities.DemoAggregate
{
    public interface IDemoRepository
    {
        Task AddAsync(Demo demo);

        Task<Demo> GetByKeyAsync(Guid id);

        Task UpdateAsync(Demo demo);

        Task DeleteAsync(Demo entity);

        Task<PageDto<DemoDto>> GetPageAsync(GetDemoPageDto dto);

        Task<DemoDto> GetEditViewAsync(Guid id);
    }
}

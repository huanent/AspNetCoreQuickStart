using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyCompany.MyProject.Application.Entities.DemoAggregate;

namespace MyCompany.MyProject.Application.Repositories
{
    public interface IDemoRepository
    {
        Task AddAsync(Demo demo);

        Task<Demo> GetByKeyAsync(Guid id);

        Task UpdateAsync(Demo demo);

        Task DeleteAsync(Demo entity);
    }
}

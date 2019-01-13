using System;
using System.Threading.Tasks;
using MyCompany.MyProject.Application.Dtos.Demo;

namespace MyCompany.MyProject.Application.Services
{
    public interface IDemoService
    {
        Task AddDemoAsync(AddDemoDto dto);

        Task UpdateDemoAsync(UpdateDemoDto dto);

        Task DeleteAsync(Guid id);
    }
}

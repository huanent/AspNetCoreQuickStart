using System.Threading.Tasks;
using MyCompany.MyProject.Application.Dtos.Demo;
using MyCompany.MyProject.Application.Entities.DemoAggregate;
using MyCompany.MyProject.Application.Repositories;

namespace MyCompany.MyProject.Application.Services.Internal
{
    [InjectScoped(typeof(IDemoService))]
    internal class DemoService : IDemoService
    {
        private readonly IDemoRepository _demoRepository;

        public DemoService(IDemoRepository demoRepository)
        {
            _demoRepository = demoRepository;
        }

        public async Task AddDemoAsync(AddDemoDto dto)
        {
            var entity = new Demo(dto.Name);
            await _demoRepository.AddAsync(entity);
        }
    }
}

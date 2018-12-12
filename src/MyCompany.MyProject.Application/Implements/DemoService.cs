using System.Threading.Tasks;
using MyCompany.MyProject.Application.Dtos.Demo;
using MyCompany.MyProject.Application.Entities.DemoAggregate;
using MyCompany.MyProject.Application.Interfaces;

namespace MyCompany.MyProject.Application.Implements
{
    [InjectScoped(typeof(IDemoService))]
    internal class DemoService : IDemoService
    {
        private readonly IRepository<Demo> _demoRepository;

        public DemoService(IRepository<Demo> demoRepository)
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

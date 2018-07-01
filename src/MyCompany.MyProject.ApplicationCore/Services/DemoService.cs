using MyCompany.MyProject.ApplicationCore.Dtos;
using MyCompany.MyProject.ApplicationCore.Entities;
using MyCompany.MyProject.ApplicationCore.IRepositories;
using MyCompany.MyProject.ApplicationCore.IServices;
using System.Threading.Tasks;

namespace MyCompany.MyProject.ApplicationCore.Services
{
    public class DemoService : IDemoService
    {
        readonly IDemoRepository _demoRepository;
        public DemoService(IDemoRepository demoRepository)
        {
            _demoRepository = demoRepository;

        }

        public async Task CreateDemoAsync(AddDemoDto dto)
        {
            var entity = new Demo(dto.Name);
            await _demoRepository.AddAsync(entity);
        }

        public void UpdateDemo(EditDemoDto dto)
        {
            var entity = _demoRepository.FindByKey(dto.Id);
            entity.Update(dto.Name);
            _demoRepository.Update(entity);
        }
    }
}

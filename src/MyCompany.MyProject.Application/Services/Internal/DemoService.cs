using System;
using System.Threading.Tasks;
using MyCompany.MyProject.Application.Dtos.Demo;
using MyCompany.MyProject.Application.Entities.DemoAggregate;
using MyCompany.MyProject.Core.Exceptions;

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
            foreach (var item in dto.Items)
            {
                entity.AddItem(item.Name);
            }
            await _demoRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _demoRepository.GetByKeyAsync(id);
            if (entity == null) throw new NotFoundEntityException("示例");
            await _demoRepository.DeleteAsync(entity);
        }

        public async Task UpdateDemoAsync(UpdateDemoDto dto)
        {
            var demo = await _demoRepository.GetByKeyAsync(dto.Id);
            if (demo == null) throw new NotFoundEntityException("示例");
            demo.Update(dto.Name);
            await _demoRepository.UpdateAsync(demo);
        }
    }
}

using ApplicationCore.Entities;
using ApplicationCore.IRepositories;
using ApplicationCore.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Services
{
    public class DemoService : IDemoService
    {
        readonly IDemoRepository _demoRepository;
        public DemoService(IDemoRepository demoRepository)
        {
            _demoRepository = demoRepository;

        }
        public async System.Threading.Tasks.Task CreateDemoAsync(string name)
        {
            var entity = new Demo(name);
            await _demoRepository.AddAsync(entity);
        }

        public void UpdateDemo(Guid id, string name)
        {
            var entity = _demoRepository.FindByKey(id);
            entity.Update(name);
            _demoRepository.Update(entity);
        }
    }
}

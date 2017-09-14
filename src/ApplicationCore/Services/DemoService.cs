using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Dtos;
using ApplicationCore.Entities;
using ApplicationCore.Repositories;

namespace ApplicationCore.Services
{
    public class DemoService : IDemoService
    {
        IDemoRepository _demoRepository;
        public DemoService(IDemoRepository demoRepository)
        {
            _demoRepository = demoRepository;
        }

        public Demo GetDemoByKey(Guid key)
        {
            return _demoRepository.GetDemoByKey(key);
        }

        public bool SaveDemo(DemoDto demoDto)
        {
            var entity = demoDto.ToDemo();
            return _demoRepository.SaveDemo(entity);
        }
    }
}

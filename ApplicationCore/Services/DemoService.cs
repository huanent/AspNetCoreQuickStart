using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Entities;
using ApplicationCore.Repositories;

namespace ApplicationCore.Services
{
    internal class DemoService : IDemoService
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
    }
}

using ApplicationCore.Entities;
using ApplicationCore.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.IRepositories
{
    public interface IDemoRepository
    {
        IEnumerable<Demo> AllDemo();

        System.Threading.Tasks.Task AddDemoAsync(Demo demo);
    }
}

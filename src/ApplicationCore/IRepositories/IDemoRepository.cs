using ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.IRepositories
{
    public interface IDemoRepository
    {
        IEnumerable<Demo> AllDemo();

        Task AddDemoAsync(Demo demo);
    }
}

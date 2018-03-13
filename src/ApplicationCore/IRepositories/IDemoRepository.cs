using ApplicationCore.Entities;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace ApplicationCore.IRepositories
{
    public interface IDemoRepository
    {
        IEnumerable<Demo> All();

        Task AddAsync(Demo demo);

        IEnumerable<Demo> GetTopRecords(int count);

        void Update(Demo demo, DbTransaction dbTransaction = null);
    }
}

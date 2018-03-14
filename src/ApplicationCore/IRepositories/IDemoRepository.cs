using ApplicationCore.Entities;
using System;
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

        Demo FindByKey(Guid id);

        void Save(Demo demo);

        void Delete(Guid id);

        IEnumerable<Demo> GetPage(int offset, int pageSize, out int total);
    }
}

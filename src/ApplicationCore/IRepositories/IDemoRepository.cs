using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace ApplicationCore.IRepositories
{
    public interface IDemoRepository
    {
        IEnumerable<Demo> All();

        Task AddAsync(DemoModel demo);

        IEnumerable<Demo> GetTopRecords(int count, IDbTransaction dbTransaction = null);

        Demo FindByKey(Guid id);

        Demo FindByKeyOnCache(Guid id);

        void Save(DemoModel model, Guid id);

        void Delete(Guid id);

        PageModel<Demo> GetPage(GetDemoPageModel dto);
    }
}
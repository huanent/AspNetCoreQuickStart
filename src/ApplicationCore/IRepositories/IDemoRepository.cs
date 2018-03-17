﻿using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace ApplicationCore.IRepositories
{
    public interface IDemoRepository
    {
        IEnumerable<Demo> All();

        Task AddAsync(DemoModel demo);

        IEnumerable<Demo> GetTopRecords(int count);

        Demo FindByKey(Guid id);

        void Save(DemoModel model, Guid id);

        void Delete(Guid id);

        PageModel<Demo> GetPage(GetPageModel dto);
    }
}
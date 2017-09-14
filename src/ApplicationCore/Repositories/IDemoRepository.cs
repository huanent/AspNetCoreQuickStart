using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Repositories
{
    public interface IDemoRepository:IRepository
    {
        Demo GetDemoByKey(Guid key);
        bool SaveDemo(Demo entity);
    }
}

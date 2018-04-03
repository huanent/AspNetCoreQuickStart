using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ApplicationCore
{
    public interface IUnitOfWork
    {
        IDbTransaction BeginTransaction(IsolationLevel? isolationLevel = null);
    }
}

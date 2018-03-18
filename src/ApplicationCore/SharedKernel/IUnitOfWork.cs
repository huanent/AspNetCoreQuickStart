using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ApplicationCore.SharedKernel
{
    public interface IUnitOfWork
    {
        IDbTransaction BeginTransaction(IsolationLevel? isolationLevel = null);
    }
}

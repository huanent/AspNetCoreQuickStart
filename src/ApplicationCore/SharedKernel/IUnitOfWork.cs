using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.SharedKernel
{
    public delegate void Commit();
    public delegate void Rollback();
    public interface IUnitOfWork
    {
        void RunTransaction(Action<Commit, Rollback> action);

    }
}

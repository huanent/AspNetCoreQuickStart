using ApplicationCore.SharedKernel;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly AppDbContext _appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void RunTransaction(Action<Commit, Rollback> action)
        {
            using (var dbContextTransaction = _appDbContext.Database.BeginTransaction())
            {
                action.Invoke(
                    () => CommitTransaction(dbContextTransaction),
                    () => RollbackTransaction(dbContextTransaction));
            }
        }

        void CommitTransaction(IDbContextTransaction dbContextTransaction) => dbContextTransaction.Commit();

        void RollbackTransaction(IDbContextTransaction dbContextTransaction) => dbContextTransaction.Rollback();
    }
}

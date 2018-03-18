using ApplicationCore.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Infrastructure
{
    /// <summary>
    /// 在EF2.1时切换到TransactionScope来支持分布式事务
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        readonly AppDbContext _appDbContext;
        DbTransaction _dbTransaction;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IDbTransaction BeginTransaction(IsolationLevel? isolationLevel = null)
        {
            var connection = _appDbContext.Database.GetDbConnection();
            connection.Open();
            if (isolationLevel.HasValue) _dbTransaction = connection.BeginTransaction(isolationLevel.Value);
            else _dbTransaction = connection.BeginTransaction();
            _appDbContext.Database.UseTransaction(_dbTransaction);
            return _dbTransaction;
        }
    }
}

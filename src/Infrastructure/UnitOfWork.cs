using ApplicationCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace Infrastructure.SharedKernel
{
    /// <summary>
    /// 在EF2.1时切换到TransactionScope来支持分布式事务
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        readonly AppDbContext _appDbContext;


        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IDbTransaction BeginTransaction(IsolationLevel? isolationLevel = null)
        {
            var connection = _appDbContext.Database.GetDbConnection();
            connection.Open();

            DbTransaction dbTransaction;
            if (isolationLevel.HasValue) dbTransaction = connection.BeginTransaction(isolationLevel.Value);
            else dbTransaction = connection.BeginTransaction();
            _appDbContext.Database.UseTransaction(dbTransaction);
            return dbTransaction;
        }
    }
}

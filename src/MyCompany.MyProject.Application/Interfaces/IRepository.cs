using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.MyProject
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<T> FindAsync(Guid id);

        Task AddAsync(T entity);

        Task AddRangeAsync(params T[] Entities);

        Task UpdateAsync(T entity);

        Task UpdateRangeAsync(params T[] Entities);

        Task RemoveAsync(T entity);

        Task RemoveRangeAsync(params T[] Entities);
    }
}

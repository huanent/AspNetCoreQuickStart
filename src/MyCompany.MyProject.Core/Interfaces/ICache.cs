using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCompany.MyProject
{
    public interface ICache
    {
        Task RemoveAsync(object key, CancellationToken token = default(CancellationToken));

        Task SetAsync(object key, object value, TimeSpan? exp = null, CancellationToken token = default(CancellationToken));

        Task<(bool success, T value)> TryGetAsync<T>(object key, CancellationToken token = default(CancellationToken));
    }
}

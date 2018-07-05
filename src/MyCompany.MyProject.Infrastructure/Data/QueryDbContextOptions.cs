using Microsoft.EntityFrameworkCore;
using MyCompany.MyProject.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Infrastructure.Data
{
    public class QueryDbContextOptions<T> where T : DbContext
    {
        public DbContextOptions<T> Value { get; private set; }

        public QueryDbContextOptions(Action<DbContextOptionsBuilder<T>> action)
        {
            var builder = new DbContextOptionsBuilder<T>();
            action(builder);
            Value = builder.Options;
        }
    }
}

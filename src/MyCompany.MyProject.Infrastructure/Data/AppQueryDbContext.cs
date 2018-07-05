using System;
using System.Collections.Generic;
using System.Text;
using MyCompany.MyProject.ApplicationCore.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace MyCompany.MyProject.Infrastructure.Data
{
    public class AppQueryDbContext : AppDbContext
    {
        public AppQueryDbContext(QueryDbContextOptions<AppDbContext> options, ISystemDateTime systemDateTime) : base(options.Value, systemDateTime)
        {
        }
    }
}

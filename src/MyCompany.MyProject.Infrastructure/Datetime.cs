using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Infrastructure
{
    public class Datetime : IDatetime
    {
        public DateTime Now => DateTime.UtcNow.AddHours(8);
    }
}

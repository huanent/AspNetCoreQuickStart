using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompany.MyProject.Infrastructure.Internal
{
    [InjectSingleton(typeof(IDateTime))]
    internal class SystemDataTime : IDateTime
    {
        public DateTime Now => DateTime.UtcNow.AddHours(8);
    }
}

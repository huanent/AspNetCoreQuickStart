using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompany.MyProject.Infrastructure.Implements
{
    [InjectSingleton(typeof(IDateTime))]
    internal class SystemDatatime : IDateTime
    {
        public DateTime Now => DateTime.UtcNow.AddHours(8);
    }
}

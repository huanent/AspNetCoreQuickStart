using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Web.Internal
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }

        public string[] CorsOrigins { get; set; }
    }

    public class ConnectionStrings
    {
        public string Default { get; set; }
    }
}

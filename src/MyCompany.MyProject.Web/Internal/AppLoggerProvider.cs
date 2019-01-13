using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MyCompany.MyProject.Web.Internal
{
    [ProviderAlias("App")]
    public class AppLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new AppLogger(categoryName);
        }

        public void Dispose()
        {
        }
    }
}

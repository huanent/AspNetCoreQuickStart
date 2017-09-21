using ApplicationCore.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Utils
{
    public class AppLogger<T> : IAppLogger<T>
    {
        ILogger _logger;
        public AppLogger(ILoggerFactory factory)
        {
            _logger = factory.CreateLogger<T>();
        }

        public void Error(string msg, Exception exception = null)
        {
            if (exception == null)
            {
                _logger.LogError(msg);
            }
            else
            {
                _logger.LogError(exception, msg);
            }
        }

        public void Info(string msg)
        {
            _logger.LogInformation(msg);
        }

        public void Warn(string msg)
        {
            _logger.LogWarning(msg);
        }
    }
}

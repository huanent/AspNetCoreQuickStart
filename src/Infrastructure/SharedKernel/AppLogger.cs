using ApplicationCore.ISharedKernel;
using Microsoft.Extensions.Logging;
using System;

namespace Infrastructure.SharedKernel
{
    public class AppLogger<T> : IAppLogger<T>
    {
        readonly ILogger _logger;
        readonly EventId _eventId;

        public AppLogger(ILoggerFactory factory, Func<EventId> eventId)
        {
            _logger = factory.CreateLogger<T>();
            _eventId = eventId();
        }

        public void Error(string msg, Exception exception = null)
        {
            if (exception == null)
            {
                _logger.LogError(_eventId, msg);
            }
            else
            {
                _logger.LogError(_eventId, exception, msg);
            }
        }

        public void Info(string msg)
        {
            _logger.LogInformation(_eventId, msg);
        }

        public void Warn(string msg)
        {
            _logger.LogWarning(_eventId, msg);
        }
    }
}

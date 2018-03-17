﻿using ApplicationCore.SharedKernel;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Infrastructure
{
    public class AppLogger<T> : IAppLogger<T>
    {
        readonly ILogger _logger;
        readonly EventId _eventId;

        public AppLogger(ILoggerFactory factory, IOptions<AppSettings> settingsOptions)
        {
            _logger = factory.CreateLogger<T>();
            var settings = settingsOptions.Value;
            _eventId = new EventId(settings.EventId, settings.AppName);
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

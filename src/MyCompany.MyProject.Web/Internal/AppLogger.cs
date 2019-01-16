using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MyCompany.MyProject.Web.Internal
{
    public class AppLogger : ILogger
    {
        private readonly string _name;
        private readonly ConcurrentQueue<(LogLevel logLevel, string message, string name, Exception exception, EventId eventId, DateTime dateTime)> _queue;
        private readonly IDateTime _dateTime;

        public AppLogger(string name, ConcurrentQueue<(LogLevel logLevel, string message, string name, Exception exception, EventId eventId, DateTime dateTime)> queue, IDateTime dateTime)
        {
            _name = name ?? nameof(AppLogger);
            _queue = queue;
            _dateTime = dateTime;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return NoopDisposable.Instance;
        }

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter == null) throw new ArgumentNullException(nameof(formatter));
            string message = formatter(state, exception);
            if (string.IsNullOrEmpty(message)) return;
            _queue.Enqueue((logLevel, message, _name, exception, eventId, _dateTime.Now));
        }

        private class NoopDisposable : IDisposable
        {
            public static NoopDisposable Instance = new NoopDisposable();

            public void Dispose()
            {
            }
        }
    }
}

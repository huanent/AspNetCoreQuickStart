using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MyCompany.MyProject.Web.Internal
{
    public class AppLogger : ILogger
    {
        private readonly string _name;

        public AppLogger(string name)
        {
            _name = name ?? nameof(AppLogger);
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
            WriteLog(logLevel, message, _name, exception, eventId);
        }

        private void WriteLog(LogLevel logLevel, string message, string name, Exception exception, EventId eventId)
        {
#warning 接入要记录日志的适配程序
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

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MyCompany.MyProject.Web.Internal
{
    [ProviderAlias("App")]
    public class AppLoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentQueue<(LogLevel logLevel, string message, string name, Exception exception, EventId eventId, DateTime dateTime)> _queue = new ConcurrentQueue<(LogLevel logLevel, string message, string name, Exception exception, EventId eventId, DateTime dateTime)>();

        private readonly IDateTime _dateTime;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public AppLoggerProvider(IDateTime dateTime)
        {
            _dateTime = dateTime;
            new Task(() =>
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    var lastTake = WriteLog(100);
                    if (lastTake == 0) Thread.Sleep(100);
                }
            }, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning).Start();
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new AppLogger(categoryName, _queue, _dateTime);
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            WriteLog(int.MaxValue);
        }

        private int WriteLog(int take)
        {
            var list = new List<(LogLevel logLevel, string message, string name, Exception exception, EventId eventId, DateTime dateTime)>();
            if (list.Count < take && _queue.TryDequeue(out var log)) list.Add(log);

            #region MyRegion

#warning 请在此替换为你希望的日志记录方式
            var path = Path.Combine(AppContext.BaseDirectory, "logs.txt");
            var logBuilder = new StringBuilder();
            foreach (var item in list)
            {
                logBuilder.AppendLine(item.ToString());
                logBuilder.AppendLine();
            }
            File.AppendAllText(path, logBuilder.ToString());

            #endregion MyRegion

            return list.Count;
        }
    }
}

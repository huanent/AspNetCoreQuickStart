using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MyCompany.MyProject.Web.Application
{
    public class GlobalExceptionHandleFilter : IAsyncExceptionFilter
    {
        private readonly IHostingEnvironment _env;
        private readonly EventId _eventId;
        private readonly ILoggerFactory _loggerFactory;

        public GlobalExceptionHandleFilter(ILoggerFactory loggerFactory, IHostingEnvironment env, Func<EventId> eventId)
        {
            _loggerFactory = loggerFactory;
            _env = env;
            _eventId = eventId();
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var logger = _loggerFactory.CreateLogger(context.Exception.TargetSite.ReflectedType);

            if (context.Exception is DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                logger.LogWarning(dbUpdateConcurrencyException, "数据库存在并发问题");
                context.Result = new BadRequestObjectResult("网络故障,请重试");
            }
            else if (context.Exception is AppException appException)
            {
                logger.LogInformation(appException.Message);
                context.Result = new BadRequestObjectResult(appException.Message);
            }
            else
            {
                logger.LogError(
                    _eventId,
                    context.Exception,
                    context.Exception.Message);

                context.Result = new InternalServerErrorResult("未知错误,请重试");
            }

            context.ExceptionHandled = true;
            await Task.CompletedTask;
        }
    }

    public class InternalServerErrorResult : ObjectResult
    {
        public InternalServerErrorResult(object value) : base(value)
        {
            StatusCode = 500;
        }
    }
}

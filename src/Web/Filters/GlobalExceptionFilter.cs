using ApplicationCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Threading.Tasks;

namespace Web.Filters
{
    public class GlobalExceptionFilter : IAsyncExceptionFilter
    {
        readonly ILoggerFactory _loggerFactory;
        readonly IHostingEnvironment _env;
        readonly AppSettings _settings;

        public GlobalExceptionFilter(ILoggerFactory loggerFactory, IHostingEnvironment env, IOptions<AppSettings> settings)
        {
            _loggerFactory = loggerFactory;
            _env = env;
            _settings = settings.Value;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var logger = _loggerFactory.CreateLogger(context.Exception.TargetSite.ReflectedType);

            if (context.Exception is AppException appException)
            {
                logger.LogWarning(appException.Message);
                context.Result = new BadRequestObjectResult(appException.Message);
            }
            else
            {
                logger.LogError(
                    new EventId(_settings.EventId),
                    context.Exception,
                    context.Exception.Message);

                if (_env.IsProduction()) context.Result = new InternalServerErrorResult("未知错误,请重试");
                else context.Result = new InternalServerErrorResult(context.Exception);
            }
            context.ExceptionHandled = true;
            await Task.CompletedTask;
        }
    }

    public class InternalServerErrorResult : ObjectResult
    {
        public InternalServerErrorResult(object value) : base(value)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}
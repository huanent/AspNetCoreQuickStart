using ApplicationCore.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using Web.Infrastructure.Results;

namespace Web.Infrastructure.Filters
{
    public class GlobalExceptionFilter : IAsyncExceptionFilter
    {
        readonly ILoggerFactory _loggerFactory;
        readonly IHostingEnvironment _env;

        public GlobalExceptionFilter(ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            _loggerFactory = loggerFactory;
            _env = env;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var logger = _loggerFactory.CreateLogger(context.Exception.TargetSite.ReflectedType);

            if (context.Exception is AppException appException)
            {
                logger.LogWarning(appException.Message);
                var json = new ErrorResponse(appException.Message);
                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

                var json = new ErrorResponse("未知错误,请重试");

                if (_env.IsDevelopment()) json.DeveloperMessage = context.Exception;

                context.Result = new AppErrorResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            context.ExceptionHandled = true;
            await Task.CompletedTask;
        }
    }
}

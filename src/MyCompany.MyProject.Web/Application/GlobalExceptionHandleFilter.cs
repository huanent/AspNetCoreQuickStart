using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace MyCompany.MyProject.Web.Application
{
    public class GlobalExceptionHandleFilter : IAsyncExceptionFilter
    {
        private readonly IHostingEnvironment _env;
        private readonly ILoggerFactory _loggerFactory;

        public GlobalExceptionHandleFilter(ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            _loggerFactory = loggerFactory;
            _env = env;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var logger = _loggerFactory.CreateLogger(context.Exception.TargetSite.ReflectedType);

            if (context.Exception is BadRequestException appException)
            {
                logger.LogInformation(appException.Message);
                context.Result = new BadRequestObjectResult(appException.Message);
            }
            else
            {
                logger.LogError(context.Exception, context.Exception.Message);

                context.Result = _env.IsProduction() ?
                new InternalServerErrorResult("未知错误,请重试") :
                new InternalServerErrorResult(context.Exception);
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

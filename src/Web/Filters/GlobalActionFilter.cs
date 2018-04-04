using ApplicationCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Filters
{
    public class GlobalActionFilter : IAsyncActionFilter
    {
        readonly IHostingEnvironment _env;
        public GlobalActionFilter(IHostingEnvironment env)
        {
            _env = env;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ModelState.IsValid)
            {
                await next();
            }
            else
            {
                var modelState = context.ModelState.FirstOrDefault(f => f.Value.Errors.Any());
                var error = modelState.Value.Errors.First();
                string errorMessage = error.ErrorMessage == string.Empty ? "请求参数有误" : error.ErrorMessage;
                if (!_env.IsProduction()) errorMessage = $"{errorMessage}:{error.Exception.ToString()}";
                throw new ModelStateException(errorMessage, error.Exception);
            }
        }
    }
}
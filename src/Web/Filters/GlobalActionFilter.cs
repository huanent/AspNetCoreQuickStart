using ApplicationCore;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Filters
{
    public class GlobalActionFilter : IAsyncActionFilter
    {
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
                throw new AppException(error.ErrorMessage, error.Exception);
            }
        }
    }
}

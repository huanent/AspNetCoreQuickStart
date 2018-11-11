using System;
using System.Linq;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Mvc
{
    public static class ControllerExtensions
    {
        /// <summary>
        /// 中介者
        /// </summary>
        /// <param name="controllerBase"></param>
        /// <returns></returns>
        public static IMediator Mediator(this ControllerBase controllerBase)
        {
            return controllerBase.HttpContext.RequestServices.GetService<IMediator>();
        }
    }
}

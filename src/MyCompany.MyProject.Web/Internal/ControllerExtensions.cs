using System;
using System.Linq;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Mvc
{
    public static class ControllerExtensions
    {
        /// <summary>
        /// 中介者发布事件
        /// </summary>
        /// <param name="controllerBase"></param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task PublishAsync(this ControllerBase controllerBase, INotification notification)
        {
            var mediator = controllerBase.HttpContext.RequestServices.GetService<IMediator>();
            await mediator.Publish(notification, controllerBase.HttpContext.RequestAborted);
        }

        /// <summary>
        /// 中介者发送请求
        /// </summary>
        /// <param name="controllerBase"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task<T> SendAsync<T>(this ControllerBase controllerBase, IRequest<T> request)
        {
            var mediator = controllerBase.HttpContext.RequestServices.GetService<IMediator>();
            return await mediator.Send(request, controllerBase.HttpContext.RequestAborted);
        }
    }
}

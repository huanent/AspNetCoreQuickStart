using System;
using MediatR;
using Newtonsoft.Json;

namespace MyCompany.MyProject.Application.Demo.Commands
{
    public class CreateDemoCommand : IRequest
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [JsonRequired]
        public string Name { get; set; }
    }
}

using System;
using MediatR;
using Newtonsoft.Json;

namespace MyCompany.MyProject.Application.Demos.Commands
{
    public class ModifyDemoCommand : IRequest
    {
        [JsonRequired]
        public Guid Id { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [JsonRequired]
        public string Name { get; set; }
    }
}

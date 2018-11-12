using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Newtonsoft.Json;

namespace MyCompany.MyProject.Application.Demo.Commands
{
    public class ModifyDemoCommand : IRequest
    {
        /// <summary>
        /// 年龄
        /// </summary>
        [JsonRequired, Range(0, 100)]
        public int Age { get; set; }

        [JsonRequired]
        public Guid Id { get; set; }
    }
}

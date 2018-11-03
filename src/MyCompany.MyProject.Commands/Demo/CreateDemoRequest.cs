using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MediatR;
using Newtonsoft.Json;

namespace MyCompany.MyProject.Commands.Demo
{
    public class CreateDemoRequest : IRequest
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [JsonRequired]
        public string Name { get; set; }
    }
}

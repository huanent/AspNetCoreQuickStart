using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Dtos
{
    public class DemoDto
    {
        [Required(ErrorMessage = "名称是必填项")]
        public string Name { get; set; }

        public Demo ToDemo(Demo demo = null)
        {
            demo = demo ?? new Demo();
            demo.Name = Name;
            return demo;
        }
    }
}

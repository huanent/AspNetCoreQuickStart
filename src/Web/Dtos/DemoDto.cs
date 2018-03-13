using ApplicationCore.Entities;
using ApplicationCore.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Web.Dtos
{
    public class DemoDto
    {
        #region props
        [Required, StringLength(20, ErrorMessage = "name最大值为20字符")]
        public string Name { get; set; }
        #endregion


        public Demo ToDemo(Demo demo = null)
        {
            demo = demo ?? new Demo(Name);
            demo.Name = Name;
            return demo;
        }
    }
}

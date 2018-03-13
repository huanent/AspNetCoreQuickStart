using ApplicationCore.Entities;
using ApplicationCore.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Dtos
{
    public class DemoDto
    {
        #region props
        [Required, StringLength(20, ErrorMessage = "name最大值为20字符")]
        public string Name { get; set; }
        #endregion


        public Demo ToDemo()
        {
            var demo = new Demo(Name);
            return demo;
        }
    }
}

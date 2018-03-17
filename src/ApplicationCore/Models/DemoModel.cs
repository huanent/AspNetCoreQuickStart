using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Models
{
    public class DemoModel
    {
        [Required]
        public string Name { get; set; }

        public Demo ToDemo()
        {
            return new Demo(Name);
        }
    }
}

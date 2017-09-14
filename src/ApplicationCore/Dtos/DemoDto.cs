using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Dtos
{
    public class DemoDto
    {
        [Required]
        public string Name { get; set; }

        public Demo ToDemo()
        {
            return new Demo
            {
                Name = Name
            };
        }
    }
}

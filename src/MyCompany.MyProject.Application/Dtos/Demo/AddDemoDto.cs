using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyCompany.MyProject.Application.Dtos.Demo
{
    public class AddDemoDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public IEnumerable<Item> Items { get; set; }

        public class Item
        {
            [Required]
            public string Name { get; set; }
        }
    }
}

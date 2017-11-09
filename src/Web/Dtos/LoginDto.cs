using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Dtos
{
    public class LoginDto
    {
        [Required]
        [MinLength(6), MaxLength(32)]
        public string Name { get; set; }

        [Required]
        [MinLength(8), MaxLength(32)]
        public string Pwd { get; set; }
    }
}

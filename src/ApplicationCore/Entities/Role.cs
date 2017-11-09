using ApplicationCore.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Role:EntityBase
    {
        public string NickName { get; set; }
        public string Name { get; set; }
        public ICollection<Permission> Permissions { get; set; }
    }
}

using ApplicationCore.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class User : EntityBase
    {
        public string NickName { get; set; }

        public string Name { get; set; }

        public string Pwd { get; set; }

        public Guid? RoleId { get; set; }

        public Role Role { get; set; }

        public ICollection<Permission> Permissions { get; set; }
    }
}

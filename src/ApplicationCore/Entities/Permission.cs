using ApplicationCore.SharedKernel;
using ApplicationCore.Valuse;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Permission : EntityBase
    {
        public string Controller { get; set; }

        public string Action { get; set; }

        public HttpMethod HttpMethod { get; set; }

        public Guid? UserId { get; set; }

        public Guid? RoleId { get; set; }
    }
}

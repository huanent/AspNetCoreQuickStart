using ApplicationCore.Entities;
using ApplicationCore.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class PermissionViewModel
    {
        public string Controller { get; set; }

        public string Action { get; set; }

        public HttpMethod HttpMethod { get; set; }
    }
}

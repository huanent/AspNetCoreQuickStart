using ApplicationCore.Entities;
using ApplicationCore.Values;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.IServices
{
    public interface IPermissionService
    {
        void ChangeUserPermission(Guid userId, IEnumerable<Permission> permissions);
        bool ValidPermission(string controller, string action, HttpMethod httpMethod, string userName);
    }
}

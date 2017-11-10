using ApplicationCore.Entities;
using ApplicationCore.Values;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.IRepositories
{
    public interface IPermissionRepository
    {
        void ClearUserPermission(Guid userId);

        void AddUserPermission(IEnumerable<Permission> permissions);

        bool ExistsPermission(string controller, string action, HttpMethod httpMethod, Guid? userId, Guid? roleId);
    }
}

using ApplicationCore.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Values;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        AppDbContext _appDbContext;
        public PermissionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddUserPermission(IEnumerable<Permission> permissions)
        {
            _appDbContext.Permission.AddRange(permissions);
            _appDbContext.SaveChanges();
        }

        public void ClearUserPermission(Guid userId)
        {
            _appDbContext.Database.ExecuteSqlCommand($"delete from Permission where UserId={userId}");
        }

        public bool ExistsPermission(string controller, string action, HttpMethod httpMethod, Guid? userId, Guid? roleId)
        {
            return _appDbContext.Permission.Any(a =>
                            a.Controller == controller
                         && a.Action == action
                         && a.HttpMethod == httpMethod
                         && (a.UserId == userId || a.RoleId == roleId));
        }
    }
}

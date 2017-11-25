using ApplicationCore.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Entities;
using ApplicationCore.IRepositories;
using ApplicationCore.Exceptions;
using ApplicationCore.SharedKernel;
using ApplicationCore.Values;
using System.Linq;

namespace ApplicationCore.Services
{
    public class PermissionService : IPermissionService
    {
        IUserRepository _userRepository;
        IPermissionRepository _permissionRepository;
        IUnitOfWork _unitOfWork;

        public PermissionService(
            IUserRepository userRepository,
            IPermissionRepository permissionRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;
        }
        public void ChangeUserPermission(Guid userId, IEnumerable<Permission> permissions)
        {
            bool existsUser = _userRepository.ExistsById(userId);
            if (!existsUser) throw new AppException("要更新权限的用户不存在");

            _unitOfWork.RunTransaction((c, r) =>
            {
                try
                {
                    _permissionRepository.ClearUserPermission(userId);
                    _permissionRepository.AddUserPermission(permissions);
                    c();
                }
                catch (Exception e)
                {
                    r();
                    throw e;
                }
            });
        }

        public bool ValidPermission(string controller, string action, HttpMethod httpMethod, string userName)
        {
            try
            {
                var user = _userRepository.FindUserByName(userName);
                return _permissionRepository.ExistsPermission(controller, action, httpMethod, user.Id, user.RoleId);
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}

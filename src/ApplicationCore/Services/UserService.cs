using ApplicationCore.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Entities;
using ApplicationCore.IRepositories;
using ApplicationCore.Exceptions;
using ApplicationCore.SharedKernel;

namespace ApplicationCore.Services
{
    public class UserService : IUserService
    {
        readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void CreateUser(User user)
        {
            bool existUser = _userRepository.ExistUserByName(user.Name);
            if (existUser) throw new AppException($"已存在用户名为{user.Name}的用户");
            _userRepository.AddUser(user);
        }

        public void ValidUserLogin(string userName, string pwd)
        {
            var user = _userRepository.FindUserByName(userName);
            bool valid = user.Pwd == pwd;
            if (!valid) throw new AppException("密码错误！");
        }
    }
}

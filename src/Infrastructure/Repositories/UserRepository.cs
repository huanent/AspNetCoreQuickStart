using ApplicationCore.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Entities;
using System.Linq;
using ApplicationCore.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        readonly AppDbContext _appDbContext;
        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddUser(User user)
        {
            _appDbContext.Add(user);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<User> AllUser()
        {
            return _appDbContext.User.ToArray();
        }

        public bool ExistsById(Guid userId)
        {
            return _appDbContext.User.Any(a=>a.Id==userId);
        }

        public bool ExistUserByName(string name) => _appDbContext.User.Any(a => a.Name == name);

        public User FindUserByName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var user = _appDbContext.User.FirstOrDefault(f => f.Name == name);
            if (user == null) throw new AppException($"未能找到用户名为：{name}的用户信息");
            return user;
        }
    }
}

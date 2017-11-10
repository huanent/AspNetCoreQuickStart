using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.IRepositories
{
    public interface IUserRepository
    {
        User FindUserByName(string name);

        IEnumerable<User> AllUser();

        bool ExistUserByName(string name);

        void AddUser(User user);

        bool ExistsById(Guid userId);

    }
}

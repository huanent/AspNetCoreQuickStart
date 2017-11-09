using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.IServices
{
    public interface IUserService
    {
        void CreateUser(User user);

        void ValidUserLogin(string userName, string pwd);
    }
}

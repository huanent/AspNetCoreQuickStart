using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ApplicationCore.Entities;
using Web.ViewModels;

namespace Web.Mappers
{
    public class UserMapper: Profile
    {
        public UserMapper()
        {
            CreateMap<UserViewModel, User>();
        }
    }
}

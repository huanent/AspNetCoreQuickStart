using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Web.Dtos;
using ApplicationCore.Entities;

namespace Web.Mappers
{
    public class UserMapper: Profile
    {
        public UserMapper()
        {
            CreateMap<UserDto, User>();
        }
    }
}

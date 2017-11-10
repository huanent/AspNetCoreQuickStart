using ApplicationCore.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Mappers
{
    public class PermissionMapper : Profile
    {
        public PermissionMapper()
        {
            CreateMap<PermissionViewModel, Permission>();
        }
    }
}

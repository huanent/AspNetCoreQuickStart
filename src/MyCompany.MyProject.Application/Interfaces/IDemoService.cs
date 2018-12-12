using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyCompany.MyProject.Application.Dtos.Demo;

namespace MyCompany.MyProject.Application.Interfaces
{
    public interface IDemoService
    {
        Task AddDemoAsync(AddDemoDto dto);
    }
}

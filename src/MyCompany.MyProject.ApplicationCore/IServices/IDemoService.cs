using MyCompany.MyProject.ApplicationCore.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompany.MyProject.ApplicationCore.IServices
{
    public interface IDemoService
    {
        System.Threading.Tasks.Task CreateDemoAsync(AddDemoDto dto);
        void UpdateDemo(EditDemoDto dto);
    }
}

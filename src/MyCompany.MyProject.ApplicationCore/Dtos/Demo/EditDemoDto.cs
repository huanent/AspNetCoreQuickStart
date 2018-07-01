using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompany.MyProject.ApplicationCore.Dtos.Demo
{
    public class EditDemoDto : AddDemoDto
    {
        public Guid Id { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompany.MyProject.ApplicationCore.Dtos
{
    public class EditDemoDto : AddDemoDto
    {
        public Guid Id { get; set; }
    }
}

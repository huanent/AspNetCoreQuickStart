using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace MyCompany.MyProject.Commands.Demo
{
    public class DeleteDemoRequest : IRequest
    {
        public Guid Id { get; set; }
    }
}

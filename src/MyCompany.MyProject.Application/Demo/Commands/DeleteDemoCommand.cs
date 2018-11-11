using System;
using MediatR;

namespace MyCompany.MyProject.Application.Demo.Commands
{
    public class DeleteDemoCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}

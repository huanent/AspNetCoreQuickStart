using System;
using MediatR;
using MyCompany.MyProject.Application.Demo.Models;

namespace MyCompany.MyProject.Application.Demo.Queries
{
    public class GetDemoByIdQuery : IRequest<DemoModel>
    {
        public Guid Id { get; set; }
    }
}

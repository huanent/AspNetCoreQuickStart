using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using MyCompany.MyProject.Dto.Demo;

namespace MyCompany.MyProject.Commands.Demo
{
    public class FindDemoByIdRequest : IRequest<DemoDto>
    {
        public Guid Id { get; set; }
    }
}

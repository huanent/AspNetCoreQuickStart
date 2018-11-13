using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using MyCompany.MyProject.Application.Demos.Models;

namespace MyCompany.MyProject.Application.Demos.Queries
{
    public class GetDemoByIdQuery : IRequest<DemoModel>
    {
        [Required]
        public Guid Id { get; set; }
    }
}

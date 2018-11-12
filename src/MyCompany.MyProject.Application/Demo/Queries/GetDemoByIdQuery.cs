using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using MyCompany.MyProject.Application.Demo.Models;

namespace MyCompany.MyProject.Application.Demo.Queries
{
    public class GetDemoByIdQuery : IRequest<DemoModel>
    {
        [Required]
        public Guid Id { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace MyCompany.MyProject.Application.Demo.Commands
{
    public class DeleteDemoCommand : IRequest
    {
        [Required]
        public Guid Id { get; set; }
    }
}

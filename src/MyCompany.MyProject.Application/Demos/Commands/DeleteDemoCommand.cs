using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Newtonsoft.Json;

namespace MyCompany.MyProject.Application.Demos.Commands
{
    public class DeleteDemoCommand : IRequest
    {
        [Required]
        public Guid Id { get; set; }
    }
}

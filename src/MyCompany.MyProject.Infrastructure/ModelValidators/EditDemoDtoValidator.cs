using FluentValidation;
using MyCompany.MyProject.ApplicationCore.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompany.MyProject.Infrastructure.ModelValidators
{
    public class EditDemoDtoValidator : AddDemoDtoValidator<EditDemoDto>
    {
        public EditDemoDtoValidator()
        {
            RuleFor(r => r.Id).NotEmpty();
        }
    }
}

using MyCompany.MyProject.ApplicationCore.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompany.MyProject.Infrastructure.ModelValidators
{
    public class AddDemoDtoValidator<T> : AbstractValidator<T> where T : AddDemoDto
    {
        public AddDemoDtoValidator()
        {
            RuleFor(r => r.Name).NotEmpty();
        }
    }

    public class AddDemoDtoValidator : AddDemoDtoValidator<AddDemoDto>
    {
    }
}

using ApplicationCore.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.ModelValidators
{
    public class AddDemoDtoValidator : AbstractValidator<AddDemoDto>
    {
        public AddDemoDtoValidator()
        {
            RuleFor(r => r.Name).NotEmpty();
        }
    }
}

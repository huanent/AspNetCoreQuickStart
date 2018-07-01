using FluentValidation;
using MyCompany.MyProject.ApplicationCore.Dtos.Demo;

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

using FluentValidation;
using MyCompany.MyProject.ApplicationCore.Dtos.Demo;
using MyCompany.MyProject.Infrastructure.ModelValidators.Page;

namespace MyCompany.MyProject.Infrastructure.ModelValidators
{
    public class QueryDemoPageDtoValidator : QueryPageDtoValidator<QueryDemoPageDto>
    {
        public QueryDemoPageDtoValidator()
        {
            RuleFor(r => r.Name).NotNull().WithMessage("名称不能为空");
        }
    }
}

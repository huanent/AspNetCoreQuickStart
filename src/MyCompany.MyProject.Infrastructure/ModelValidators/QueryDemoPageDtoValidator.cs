using MyCompany.MyProject.ApplicationCore.Dtos;
using FluentValidation;
using MyCompany.MyProject.Infrastructure.ModelValidators.Common;

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

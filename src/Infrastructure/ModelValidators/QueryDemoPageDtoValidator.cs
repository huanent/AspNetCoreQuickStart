using ApplicationCore.Dtos;
using FluentValidation;
using Infrastructure.ModelValidators.Common;

namespace Infrastructure.ModelValidators
{
    public class QueryDemoPageDtoValidator : QueryPageDtoValidator<QueryDemoPageDto>
    {
        public QueryDemoPageDtoValidator()
        {
            RuleFor(r => r.Name).NotNull().WithMessage("名称不能为空");
        }
    }
}

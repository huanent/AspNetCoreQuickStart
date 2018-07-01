using MyCompany.MyProject.ApplicationCore.Dtos;
using MyCompany.MyProject.ApplicationCore.Dtos.Page;
using FluentValidation;

namespace MyCompany.MyProject.Infrastructure.ModelValidators.Page
{
    public class QueryPageDtoValidator<T> : AbstractValidator<T> where T : QueryPageDto
    {
        public QueryPageDtoValidator()
        {
            RuleFor(r => r.PageIndex).NotEmpty().InclusiveBetween(1, int.MaxValue);
            RuleFor(r => r.PageSize).NotEmpty().InclusiveBetween(1, int.MaxValue);
        }
    }

    public class QueryPageDtoValidator : QueryPageDtoValidator<QueryPageDto>
    {
    }
}

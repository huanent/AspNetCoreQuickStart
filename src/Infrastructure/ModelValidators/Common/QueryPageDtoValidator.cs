using ApplicationCore.Dtos;
using ApplicationCore.Dtos.Common;
using FluentValidation;

namespace Infrastructure.ModelValidators.Common
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

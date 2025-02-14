using FluentValidation;
using ZeroStoreApp.QueryApplication.Queries;

namespace ZeroStoreApp.QueryApplication.Validators;

public class GetPaginatedQueryValidator : AbstractValidator<GetPaginatedQuery>
{
    public GetPaginatedQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(PaginationDefinition.DefaultPage)
            .WithMessage(PaginationDefinition.PageRangeMessage);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, PaginationDefinition.MaxPageSize)
            .WithMessage(PaginationDefinition.PageSizeRangeMessage);
    }
}

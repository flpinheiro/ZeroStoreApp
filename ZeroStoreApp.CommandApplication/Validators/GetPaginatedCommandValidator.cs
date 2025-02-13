using FluentValidation;
using ZeroStoreApp.CommandApplication.Commands;
using static ZeroStoreApp.CrossCutting.Constants.Definitions;

namespace ZeroStoreApp.CommandApplication.Validators;

public class GetPaginatedCommandValidator: AbstractValidator<GetPaginatedCommand>
{
    public GetPaginatedCommandValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(PaginationDefinition.DefaultPage)
            .WithMessage(PaginationDefinition.PageRangeMessage);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, PaginationDefinition.MaxPageSize)
            .WithMessage(PaginationDefinition.PageSizeRangeMessage);
    }
}

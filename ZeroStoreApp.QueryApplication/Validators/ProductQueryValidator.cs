using FluentValidation;
using ZeroStoreApp.QueryApplication.Queries;
using ZeroStoreApp.QueryApplication.Validators;

namespace ZeroStoreApp.QueryApplication.Validaators;


public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
{
    public GetProductQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(IdRequiredMessage);
    }
}

public class GetPaginatedProductsQueryValidator : AbstractValidator<GetPaginatedProductsQuery>
{
    public GetPaginatedProductsQueryValidator()
    {
        Include(new GetPaginatedQueryValidator());
    }
}
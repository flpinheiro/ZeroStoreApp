using FluentValidation;
using ZeroStoreApp.CommandApplication.Commands;


namespace ZeroStoreApp.CommandApplication.Validators;

public class GetProductValidator : AbstractValidator<GetProductCommand>
{
    public GetProductValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(IdRequiredMessage);
    }
}

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand> 
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(IdRequiredMessage);
    }
}

public class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(ProductDefinition.NameRequiredMessage)
            .MaximumLength(ProductDefinition.NameMaxLength)
            .WithMessage(ProductDefinition.NameMaxLengthMessage);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(ProductDefinition.DescriptionRequiredMessage)
            .MaximumLength(ProductDefinition.DescriptionMaxLength)
            .WithMessage(ProductDefinition.DescriptionMaxLengthMessage);

        RuleFor(x => x.Price)
            .NotEmpty()
            .WithMessage(ProductDefinition.PriceRequiredMessage)
            .PrecisionScale(ProductDefinition.PricePrecision, ProductDefinition.PriceScale, true)
            .WithMessage(ProductDefinition.PricePrecisionScaleMessage);

        RuleFor(x => x.Stock)
            .NotEmpty()
            .WithMessage(ProductDefinition.StockRequiredMessage);
    }
}

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(IdRequiredMessage);

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(ProductDefinition.NameRequiredMessage)
            .MaximumLength(ProductDefinition.NameMaxLength)
            .WithMessage(ProductDefinition.NameMaxLengthMessage);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(ProductDefinition.DescriptionRequiredMessage)
            .MaximumLength(ProductDefinition.DescriptionMaxLength)
            .WithMessage(ProductDefinition.DescriptionMaxLengthMessage);

        RuleFor(x => x.Price)
            .NotEmpty()
            .WithMessage(ProductDefinition.PriceRequiredMessage)
            .PrecisionScale(ProductDefinition.PricePrecision, ProductDefinition.PriceScale, true)
            .WithMessage(ProductDefinition.PricePrecisionScaleMessage);

        RuleFor(x => x.Stock)
            .NotEmpty()
            .WithMessage(ProductDefinition.StockRequiredMessage);
    }
}

public class GetProductsCommandValidator : AbstractValidator<GetProductsCommand>
{
    public GetProductsCommandValidator()
    {
        Include(new GetPaginatedCommandValidator());
    }
}
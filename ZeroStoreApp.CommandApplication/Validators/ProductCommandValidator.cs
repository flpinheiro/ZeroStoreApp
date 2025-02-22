﻿using FluentValidation;
using ZeroStoreApp.CommandApplication.Commands;

namespace ZeroStoreApp.CommandApplication.Validators;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(IdRequiredMessage);
    }
}

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
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
            .WithMessage(ProductDefinition.PricePrecisionScaleMessage)
            .GreaterThan(0)
            .WithMessage(ProductDefinition.PricePositive);

        RuleFor(x => x.Stock)
            .NotEmpty()
            .WithMessage(ProductDefinition.StockRequiredMessage)
            .GreaterThan(0)
            .WithMessage(ProductDefinition.StockPositive);
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
            .WithMessage(ProductDefinition.PricePrecisionScaleMessage)
            .GreaterThan(0)
            .WithMessage(ProductDefinition.PricePositive);

        RuleFor(x => x.Stock)
            .NotEmpty()
            .WithMessage(ProductDefinition.StockRequiredMessage)
            .GreaterThan(0)
            .WithMessage(ProductDefinition.StockPositive);
    }
}

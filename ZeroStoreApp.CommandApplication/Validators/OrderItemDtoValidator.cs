using FluentValidation;
using ZeroStoreApp.CommandApplication.Dtos;

namespace ZeroStoreApp.CommandApplication.Validators;

public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
{
    public OrderItemDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage(OrderDefinition.ShouldHaveProductIdMessage);

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .InclusiveBetween(OrderDefinition.MinQuantityAllowed, OrderDefinition.MaxQuantityAllowed)
            .WithMessage(OrderDefinition.QuantityAllowedMessage);

    }
}

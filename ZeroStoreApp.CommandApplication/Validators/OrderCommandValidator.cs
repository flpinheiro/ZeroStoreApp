using FluentValidation;
using ZeroStoreApp.CommandApplication.Commands;

namespace ZeroStoreApp.CommandApplication.Validators;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand> 
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Items)
                .NotEmpty()
                .WithMessage(OrderDefinition.ShouldHaveItemsMessage)
                .ForEach(item => item.SetValidator(new OrderItemDtoValidator()));
    }
}

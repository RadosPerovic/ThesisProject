using FluentValidation;

namespace ThesisProject.Application.UseCases.Orders.Commands.AddOrderItem;
public class AddOrderItemRequestValidator : AbstractValidator<AddOrderItemRequest>
{
    public AddOrderItemRequestValidator()
    {
        RuleFor(e => e.OrderId)
            .NotEmpty()
            .WithMessage("OrderId must not be empty");

        RuleFor(e => e.ProductId)
            .NotEmpty()
            .WithMessage("OrderId must not be empty");

        RuleFor(e => e.Quantity)
            .Must(e => e > 0)
            .WithMessage("Quantity must not be 0 or less then 0");
    }
}

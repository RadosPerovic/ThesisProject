using FluentValidation;

namespace ThesisProject.Application.UseCases.Orders.Commands.PlaceOrder;
public class PlaceOrderRequestValidator : AbstractValidator<PlaceOrderRequest>
{
    public PlaceOrderRequestValidator()
    {
        RuleFor(e => e.OrderId)
            .NotEmpty()
            .WithMessage("OrderId must not be empty.");
    }
}

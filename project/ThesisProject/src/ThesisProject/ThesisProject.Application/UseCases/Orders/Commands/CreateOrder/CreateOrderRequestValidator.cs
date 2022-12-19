using FluentValidation;

namespace ThesisProject.Application.UseCases.Orders.Commands.CreateOrder;
public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .WithMessage("Name must not be empty");
    }
}

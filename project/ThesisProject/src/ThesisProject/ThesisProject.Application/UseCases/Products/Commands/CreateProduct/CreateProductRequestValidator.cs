using FluentValidation;

namespace ThesisProject.Application.UseCases.Products.Commands.CreateProduct;
public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(e => e.Price)
            .Must(e => e > 0)
            .WithMessage("Price cannot be 0 or less then 0");

        RuleFor(e => e.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty");
    }
}

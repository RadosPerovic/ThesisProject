using FluentValidation;

namespace ThesisProject.Application.UseCases.Products.Commands.UpdateProduct;
public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(e => e.Price)
            .Must(e => e > 0)
            .WithMessage("Price cannot be 0 or less then 0");

        RuleFor(e => e.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty");
    }
}

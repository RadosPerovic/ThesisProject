using FluentValidation;

namespace ThesisProject.Application.UseCases.Products.Commands.AddProductStock;
public class AddProductStockRequestValidator : AbstractValidator<AddProductStockRequest>
{
    public AddProductStockRequestValidator()
    {
        RuleFor(e => e.ProjectId)
            .NotEmpty()
            .WithMessage("Project id must not be empty");

        RuleFor(e => e.Stock)
            .Must(e => e > 0)
            .WithMessage("Stock cannot be 0 or less then 0");
    }
}

using MediatR;

namespace ThesisProject.Application.UseCases.Products.Commands.UpdateProduct;
public class UpdateProductRequest : IRequest<Unit>
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
}

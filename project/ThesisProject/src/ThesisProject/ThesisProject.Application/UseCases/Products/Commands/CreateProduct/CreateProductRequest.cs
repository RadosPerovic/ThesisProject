using MediatR;

namespace ThesisProject.Application.UseCases.Products.Commands.CreateProduct;

public class CreateProductRequest : IRequest<CreateProductResponse>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
}

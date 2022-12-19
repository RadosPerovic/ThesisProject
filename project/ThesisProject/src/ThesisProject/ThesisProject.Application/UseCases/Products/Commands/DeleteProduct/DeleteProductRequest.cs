using MediatR;

namespace ThesisProject.Application.UseCases.Products.Commands.DeleteProduct;
public class DeleteProductRequest : IRequest<Unit>
{
    public Guid ProductId { get; set; }
}

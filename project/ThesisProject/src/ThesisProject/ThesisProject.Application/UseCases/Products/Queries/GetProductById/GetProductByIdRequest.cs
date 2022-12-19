using MediatR;

namespace ThesisProject.Application.UseCases.Products.Queries.GetProductById;
public class GetProductByIdRequest : IRequest<ProductDto>
{
    public Guid ProductId { get; set; }
}

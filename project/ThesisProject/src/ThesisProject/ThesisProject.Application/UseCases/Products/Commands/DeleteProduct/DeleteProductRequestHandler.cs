using MediatR;
using ThesisProject.Application.Exceptions;
using ThesisProject.Domain.Repositories;

namespace ThesisProject.Application.UseCases.Products.Commands.DeleteProduct;
public class DeleteProductRequestHandler : IRequestHandler<DeleteProductRequest, Unit>
{
    private readonly IProductRepository _productRepository;
    public DeleteProductRequestHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Unit> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(request.ProductId);

        if (product is null)
        {
            throw new ApplicationError($"Product with id {request.ProductId} not found.");
        }

        await _productRepository.Delete(request.ProductId);

        return Unit.Value;
    }
}

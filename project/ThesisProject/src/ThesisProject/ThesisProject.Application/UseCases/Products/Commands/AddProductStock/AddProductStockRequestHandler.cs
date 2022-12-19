using MediatR;
using ThesisProject.Application.Exceptions;
using ThesisProject.Domain.Repositories;

namespace ThesisProject.Application.UseCases.Products.Commands.AddProductStock;
public class AddProductStockRequestHandler : IRequestHandler<AddProductStockRequest, Unit>
{
    private readonly IProductRepository _productRepository;

    public AddProductStockRequestHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Unit> Handle(AddProductStockRequest request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(request.ProjectId);

        if (product is null)
        {
            throw new ApplicationError($"Product with id {request.ProjectId} not found.");
        }

        product.AddStock(request.Stock);

        await _productRepository.Update(product);

        return Unit.Value;
    }
}

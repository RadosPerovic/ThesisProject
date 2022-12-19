using MediatR;
using ThesisProject.Application.Exceptions;
using ThesisProject.Domain.Repositories;

namespace ThesisProject.Application.UseCases.Products.Commands.UpdateProduct;
public class UpdateProductRequestHandler : IRequestHandler<UpdateProductRequest, Unit>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductRequestHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Unit> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(request.ProductId);

        if (product is null)
        {
            throw new ApplicationError($"Product with id {request.ProductId} not found.");
        }

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;

        await _productRepository.Update(product);

        return Unit.Value;
    }
}

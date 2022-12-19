using MediatR;
using ThesisProject.Application.Services;
using ThesisProject.Domain.Entities;
using ThesisProject.Domain.Repositories;

namespace ThesisProject.Application.UseCases.Products.Commands.CreateProduct;
public class CreateProductRequestHandler : IRequestHandler<CreateProductRequest, CreateProductResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IIdentityGenerator _identityGenerator;

    public CreateProductRequestHandler(IProductRepository productRepository, IIdentityGenerator identityGenerator)
    {
        _productRepository = productRepository;
        _identityGenerator = identityGenerator;
    }

    public async Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var productId = _identityGenerator.GenerateGuidId();

        var product = new Product(
            productId,
            request.Name,
            request.Price)
        {
            Description = request.Description
        };

        await _productRepository.Add(product);

        return new CreateProductResponse
        {
            CreatedId = productId,
        };
    }
}

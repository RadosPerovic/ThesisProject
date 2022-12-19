using MediatR;

namespace ThesisProject.Application.UseCases.Products.Queries.GetProductById;
public class GetProducyByIdRequestHandler : IRequestHandler<GetProductByIdRequest, ProductDto>
{
    private readonly IGetProductByIdQuery _getProductByIdQuery;

    public GetProducyByIdRequestHandler(IGetProductByIdQuery getProductByIdQuery)
    {
        _getProductByIdQuery = getProductByIdQuery;
    }

    public async Task<ProductDto> Handle(GetProductByIdRequest request, CancellationToken cancellatonToken)
    {
        var product = await _getProductByIdQuery.Execute(request.ProductId);

        return product;
    }
}

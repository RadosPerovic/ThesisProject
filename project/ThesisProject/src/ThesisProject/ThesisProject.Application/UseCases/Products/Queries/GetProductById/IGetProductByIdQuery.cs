namespace ThesisProject.Application.UseCases.Products.Queries.GetProductById;
public interface IGetProductByIdQuery
{
    Task<ProductDto> Execute(Guid productId);
}

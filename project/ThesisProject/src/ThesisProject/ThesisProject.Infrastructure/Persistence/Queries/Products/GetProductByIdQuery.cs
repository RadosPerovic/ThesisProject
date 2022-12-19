using Microsoft.EntityFrameworkCore;
using ThesisProject.Application.UseCases.Products.Queries.GetProductById;

namespace ThesisProject.Infrastructure.Persistence.Queries.Products;
public class GetProductByIdQuery : IGetProductByIdQuery
{
    private readonly DatabaseContext _dbContext;

    public GetProductByIdQuery(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductDto> Execute(Guid productId)
    {
        var productDto = await _dbContext.Products
            .Where(e => e.Id == productId)
            .Select(e => new ProductDto
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Price = e.Price,
                Stock = e.Stock,
            })
            .SingleOrDefaultAsync();

        return productDto;
    }
}

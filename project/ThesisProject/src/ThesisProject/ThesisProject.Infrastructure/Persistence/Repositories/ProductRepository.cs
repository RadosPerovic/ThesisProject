using Microsoft.EntityFrameworkCore;
using ThesisProject.Domain.Entities;
using ThesisProject.Domain.Repositories;
using ThesisProject.Infrastructure.Persistence.Models;

namespace ThesisProject.Infrastructure.Persistence.Repositories;
public class ProductRepository : IProductRepository
{
    private readonly DatabaseContext _dbContext;

    public ProductRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Product product)
    {
        var dbmodel = GetProductDbmodel(product);

        await _dbContext.AddAsync(dbmodel);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid productId)
    {
        var dbModel = await _dbContext.Products
            .Where(e => e.Id == productId)
            .SingleAsync();

        _dbContext.Remove(dbModel);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Product> GetById(Guid productId)
    {
        var dbModel = await _dbContext.Products
            .Where(e => e.Id == productId)
            .SingleOrDefaultAsync();

        if (dbModel is null)
        {
            return null;
        }

        var product = GetProduct(dbModel);

        return product;
    }
    public async Task Update(Product product)
    {
        var dbModel = await _dbContext.Products
            .Where(e => e.Id == product.Id)
            .SingleAsync();

        dbModel.Name = product.Name;
        dbModel.Price = product.Price;
        dbModel.Description = product.Description;
        dbModel.Stock = product.Stock;

        await _dbContext.SaveChangesAsync();
    }

    private ProductModel GetProductDbmodel(Product product)
    {
        return new ProductModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
        };
    }

    private Product GetProduct(ProductModel dbModel)
    {
        var product = new Product(
            dbModel.Id,
            dbModel.Name,
            dbModel.Price)
        {
            Description = dbModel.Description
        };

        if (dbModel.Stock > 0)
        {
            product.AddStock(dbModel.Stock);
        }

        return product;
    }
}

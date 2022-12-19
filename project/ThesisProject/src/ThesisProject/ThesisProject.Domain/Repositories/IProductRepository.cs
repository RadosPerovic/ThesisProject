using ThesisProject.Domain.Entities;

namespace ThesisProject.Domain.Repositories;
public interface IProductRepository
{
    Task Add(Product product);
    Task<Product> GetById(Guid productId);
    Task Update(Product product);
    Task Delete(Guid productId);
}

using ThesisProject.Domain.Entities;

namespace ThesisProject.Domain.Repositories;
public interface IOrderRepository
{
    Task Add(Order order);
    Task<Order?> GetById(Guid orderId);
    Task Update(Order order);
    Task Delete(Guid orderId);
}

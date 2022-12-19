namespace ThesisProject.Application.UseCases.Orders.Queries.GetOrderById;
public interface IGetOrderByIdQuery
{
    Task<OrderDto?> Execute(Guid orderId);
}

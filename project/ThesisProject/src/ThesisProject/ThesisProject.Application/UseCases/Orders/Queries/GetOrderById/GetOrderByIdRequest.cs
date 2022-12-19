using MediatR;

namespace ThesisProject.Application.UseCases.Orders.Queries.GetOrderById;
public class GetOrderByIdRequest : IRequest<OrderDto?>
{
    public Guid OrderId { get; set; }
}
